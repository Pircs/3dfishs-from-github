using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class FishMenu
{
	[MenuItem("Fish/CreateFishSeason")]
	static void CreateFishSeasonInfo()
	{

	}

	[MenuItem("Fish/Export Season")]
	static void ExportSeason()
	{
        GameObject fishSeasonObj = Selection.activeGameObject;
        if (fishSeasonObj == null)
        {
            Debug.LogError("no seasonobj selected!");
            return;
        }
        FishSeason fishseason = new FishSeason();
        OneWave oneWave = new OneWave();
        fishseason.AddWave(oneWave);
        oneWave.speed = 20;
        oneWave.pathid = 5;
        oneWave.rootea = fishSeasonObj.transform.localEulerAngles;
        int fishcellscnt = fishSeasonObj.transform.childCount;
        for (int i = 0; i < fishcellscnt; i++)
        {
            Transform child = fishSeasonObj.transform.GetChild(i);
            if (child.name == "HeadModel")
            {
                oneWave.ea = child.eulerAngles;
                oneWave.o = fishSeasonObj.transform.position;
                continue;
            }
            WaveFish wavefish = new WaveFish();
            wavefish.fkid = child.GetComponent<FishId>().fishKindId;
            wavefish.p = child.position;
            wavefish.s = child.localScale;
            oneWave.AddWaveFish(wavefish);
        }

        string destpath = EditorUtility.SaveFilePanel("", "Assets/Resources/SeasonConfigs/", "untitled","bytes");
        if (destpath.Length > 0)
        {
            string json = JsonUtility.ToJson(fishseason);
            FileStream fs = new FileStream(destpath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(json);
            sw.Flush();
            fs.Close();
            AssetDatabase.Refresh();
        }
	}

    [MenuItem("Fish/Load Season")]
    public static void LoadSeason()
    {
        string filepath = EditorUtility.OpenFilePanel("Open", Application.dataPath + "/Resources/SeasonConfigs/", "");
        if (filepath.Length > 0)
        {
            string filename = Path.GetFileNameWithoutExtension(filepath);
            TextAsset textasset = Resources.Load("SeasonConfigs/" + filename) as TextAsset;
            FishSeason season = JsonUtility.FromJson<FishSeason>(textasset.text);
            if (season != null)
            {
                foreach (OneWave wave in season.waves)
                {
                    GameObject oneWaveObj = new GameObject();
                    oneWaveObj.name = "FishSeason_Wave";
                    oneWaveObj.transform.localPosition = wave.o;
                    oneWaveObj.transform.eulerAngles = wave.rootea;
                    oneWaveObj.transform.localScale = Vector3.one;

                    GameObject head = GameObject.Instantiate<GameObject>(Resources.Load("FishPrefabs/FishSeason_Wave") as GameObject);
                    head.name = "HeadModel";
                    head.transform.parent = oneWaveObj.transform;
                    head.transform.position = wave.o;
                    head.transform.eulerAngles = wave.ea;
                    head.transform.localScale = Vector3.one;

                    foreach (WaveFish fish in wave.fishes)
                    {
                        GameObject fishobj = GameObject.Instantiate<GameObject>(Resources.Load("FishPrefabs/Fish_" + fish.fkid) as GameObject);
                        fishobj.transform.parent = oneWaveObj.transform;
                        fishobj.transform.position = fish.p;
                        fishobj.transform.eulerAngles = wave.ea;
                        fishobj.transform.localScale = fish.s;
                    }
                }
            }
        }
    }
}
