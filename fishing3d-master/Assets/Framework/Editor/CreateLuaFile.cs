using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class CreateLuaFile : Editor {

    [MenuItem("Assets/JJ/Create Lua File", false, 6)]
    static void CreateLua()
    {
        GameObject selectedObj = Selection.activeGameObject;
        string dir = Application.dataPath;
        if (selectedObj != null)
        {
            string path = AssetDatabase.GetAssetPath(selectedObj);
            dir = Path.GetDirectoryName(path);
        }
        else
        {
            string[] dirobj = Selection.assetGUIDs;
            dir = AssetDatabase.GUIDToAssetPath(dirobj[0]);
        }
        string luafile = dir + "/NewLuaClass.lua";
        FileStream fs = new FileStream(luafile, FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        sw.WriteLine("local NewLuaClass = class(\"NewLuaClass\")");
        sw.WriteLine("return NewLuaClass");
        sw.Close();
        fs.Close();
        AssetDatabase.Refresh();
    }
}
