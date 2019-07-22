using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;

public class SeasonConfigManager 
{

	private SeasonConfigManager() { }
	private static SeasonConfigManager mInstance;

    private Dictionary<int, FishSeason> mSeasonMap = new Dictionary<int, FishSeason>();
	public static SeasonConfigManager GetInstance()
	{
		if (mInstance == null)
		{
			mInstance = new SeasonConfigManager();
		}
		return mInstance;
	}

    public void Initialize()
    {
        LoadAllSeasons();
    }

    public bool Save(string filepath, FishPath path)
    {
        
        return true;
    }

    public FishSeason Load(string filepath)
    {
        return null;
    }

    private void LoadAllSeasons()
    {
        for (int i = 0; i < 5; i++)
        {
            TextAsset ta = ResourcesManager.GetInstance().LoadLocalAsset("SeasonConfigs/season_" + i.ToString()) as TextAsset;
            if (ta != null)
            {
                string jsonStr = ta.text;
                if (jsonStr != null && jsonStr.Length > 0)
                {
                    FishSeason season = JsonUtility.FromJson<FishSeason>(jsonStr);
                    if (season != null)
                    {
                        mSeasonMap.Add(i, season);
                    }
                }
            }
        }
    }

    public FishSeason GetSeason(int id)
    {
        FishSeason season;
        mSeasonMap.TryGetValue(id, out season);
        return season;
    }
}
