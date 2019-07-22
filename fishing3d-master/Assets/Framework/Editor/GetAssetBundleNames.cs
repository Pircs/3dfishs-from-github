using UnityEngine;
using System.Collections;
using UnityEditor;

public class GetAssetBundleNames : Editor {

    [MenuItem("Assets/Get AssetBundle names")]
    static void GetNames()
    {
        var names = AssetDatabase.GetAllAssetBundleNames();
        foreach (var name in names)
            Debug.Log("AssetBundle: " + name);
    }
}
