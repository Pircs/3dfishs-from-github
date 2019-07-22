using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UICmd
{
    public string key = string.Empty;
    public Object value;
}

public class UICommandSystem
{
	private static UICommandSystem mInstance = null;
    private UICommandSystem()
    { }

    private Hashtable mCmdTable= null;

    private Queue<UICmd> mCmdQueue = null;

    public static UICommandSystem GetInstance()
    {
        if (mInstance == null)
            mInstance = new UICommandSystem();

        return mInstance;
    }

    public void Initialize()
    {
        mCmdTable = new Hashtable();
        mCmdQueue = new Queue<UICmd>();
        RegisterCommand();
    }

    public void Update(float dt)
    {
        while (mCmdQueue.Count != 0)
        {
            UICmd cmd = mCmdQueue.Dequeue();

            if (cmd == null)
                continue;

            List<UIAsset> uiAsset = mCmdTable[cmd.key] as List<UIAsset>;

            for (int i = 0; i < uiAsset.Count; i++)
            {
                UIAsset asset = uiAsset[i];

                if (asset == null)
                    continue;
                if (asset.view == null || asset.viewObject == null)
                    continue;
                asset.view.HandleCommand(cmd);
            }
        }
        mCmdQueue.Clear();
    }

    public void RegisterCommand()
    {
        int length = UICmdDefine.commands.GetLength(0);
        for (int i = 0; i < length; i++)
        {
            string key = UICmdDefine.commands[i, 0];
            string view = UICmdDefine.commands[i, 1];
            if (mCmdTable.ContainsKey(key) == false)
            {
                mCmdTable.Add(UICmdDefine.commands[i, 0], new List<UIAsset>());
            }

            UIAsset asset = UIManager.GetInstance().GetUIAsset(view);
            if (asset == null)
            {
                Debug.LogError("[UICmdSystem] UICmd:" + key + ",Panel:" + view + ",not ui");
                return;
            }

            List<UIAsset> list = mCmdTable[key] as List<UIAsset>;
            if (list.Contains(asset))
            {
                Debug.LogError("[UICmdSystem] UICmd:" + key + ",Panel:" + view + ",has register");
                return;
            }
            list.Add(asset);
        }
    }

    public void AddCmd(UICmd cmd)
    {
        if (cmd == null)
            return;
        if (mCmdTable.ContainsKey(cmd.key) == false)
            return;
        mCmdQueue.Enqueue(cmd);
    }
}
