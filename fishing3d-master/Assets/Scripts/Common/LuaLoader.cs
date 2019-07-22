using UnityEngine;
using System.Collections;
using System.IO;
using LuaInterface;
using System.Text;

namespace LuaFramework {
    /// <summary>
    /// 集成自LuaFileUtils，重写里面的ReadFile，
    /// </summary>
    public class LuaLoader : LuaFileUtils {
        private ResourceManager m_resMgr;

        ResourceManager resMgr {
            get { 
                if (m_resMgr == null)
                    m_resMgr = AppFacade.Instance.GetManager<ResourceManager>(ManagerName.Resource);
                return m_resMgr;
            }
        }

        // Use this for initialization
        public LuaLoader() {
            instance = this;
            beZip = AppConst.LuaBundleMode;
        }

        /// <summary>
        /// 添加打入Lua代码的AssetBundle
        /// </summary>
        /// <param name="bundle"></param>
        public void AddBundle(string bundleName) {
            string url = Util.DataPath + bundleName.ToLower();
            if (File.Exists(url)) {
                AssetBundle bundle = AssetBundle.LoadFromFile(url);
                if (bundle != null)
                {
                    bundleName = bundleName.Replace("lua/", "").Replace(".unity3d", "");
                    base.AddSearchBundle(bundleName.ToLower(), bundle);
                }
            }
        }

        /// <summary>
        /// 当LuaVM加载Lua文件的时候，这里就会被调用，
        /// 用户可以自定义加载行为，只要返回byte[]即可。
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        //public override byte[] ReadFile(string fileName) {
        //    return base.ReadFile(fileName);     
        //}

        public override byte[] ReadFile(string fileName)
        {
#if !UNITY_EDITOR
        byte[] buffer = ReadDownLoadFile(fileName);

        if (buffer == null)
        {
            buffer = ReadResourceFile(fileName);
        }        
        
        if (buffer == null)
        {
            buffer = base.ReadFile(fileName);
        }        
#else
            byte[] buffer = base.ReadFile(fileName);

            if (buffer == null)
            {
                buffer = ReadResourceFile(fileName);
            }

            if (buffer == null)
            {
                buffer = ReadDownLoadFile(fileName);
            }
#endif

            return buffer;
        }

        public override string FindFileError(string fileName)
        {
            if (Path.IsPathRooted(fileName))
            {
                return fileName;
            }

            StringBuilder sb = StringBuilderCache.Acquire();

            if (Path.GetExtension(fileName) == ".lua")
            {
                fileName = fileName.Substring(0, fileName.Length - 4);
            }

            for (int i = 0; i < searchPaths.Count; i++)
            {
                sb.AppendFormat("\n\tno file '{0}'", searchPaths[i]);
            }

            sb.AppendFormat("\n\tno file ./Resources/?.lua");
            sb = sb.Replace("?", fileName);
            return sb.ToString();
        }

        byte[] ReadResourceFile(string fileName)
        {
            if (!fileName.EndsWith(".lua"))
            {
                fileName += ".lua";
            }

            byte[] buffer = null;
            string path = "Lua/" + fileName;
            TextAsset text = Resources.Load(path, typeof(TextAsset)) as TextAsset;

            if (text != null)
            {
                buffer = text.bytes;
                Resources.UnloadAsset(text);
            }

            return buffer;
        }

        byte[] ReadDownLoadFile(string fileName)
        {
            if (!fileName.EndsWith(".lua"))
            {
                fileName += ".lua";
            }

            string path = fileName;

            if (!Path.IsPathRooted(fileName))
            {
                path = string.Format("{0}/{1}", LuaConst.luaResDir, fileName);
            }

            if (File.Exists(path))
            {
#if !UNITY_WEBPLAYER
                return File.ReadAllBytes(path);
#else
            throw new LuaException("can't run in web platform, please switch to other platform");
#endif
            }

            return null;
        }
    }
}