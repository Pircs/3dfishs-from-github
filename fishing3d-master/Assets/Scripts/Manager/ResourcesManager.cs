using UnityEngine;
using System.Collections;
using System;

public class ResourcesManager
{
    private static ResourcesManager mInstance = null;

    private ResourcesManager()
    { }

    public static ResourcesManager GetInstance()
    {
        if (mInstance == null)
            mInstance = new ResourcesManager();

        return mInstance;
    }

    public void Initialize()
    {
        
    }

    public UnityEngine.Object LoadLocalAsset(string name)
    {
        return Resources.Load(name);
    }
}
