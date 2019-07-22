using System;
using UnityEngine;
using System.Collections;

public class AssetBundleTest : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {
        print(Application.streamingAssetsPath);
        string assetBundleURL = "file:///" + Application.streamingAssetsPath + "/fish.unity3d";

        using (WWW www = new WWW(assetBundleURL))
        {
            yield return www;

            if (www.error != null)
                throw new Exception("WWW download had an error:" + www.error);
            AssetBundle bundle = www.assetBundle;

            string[] assetnames = bundle.GetAllAssetNames();
            Texture2D obj = bundle.LoadAsset<Texture2D>("test/xiaochouyu_d");
            //Instantiate(obj);
            Instantiate(bundle.mainAsset);

            // Unload the AssetBundles compressed contents to conserve memory
            bundle.Unload(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
