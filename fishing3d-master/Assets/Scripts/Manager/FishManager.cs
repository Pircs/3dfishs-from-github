using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FishManager
{
    private static FishManager mInstance = null;
    private FishManager()
    { }

    private float timer = 0;

    private Table_Fish mFishTable;

    private Transform mFishRoot;

    private Dictionary<int, List<GameObject>> mUnActiveFishMap = new Dictionary<int, List<GameObject>>();

    public static FishManager GetInstance()
    {
        if (mInstance == null)
            mInstance = new FishManager();

        return mInstance;
    }

    public void Initialize()
    {
        mFishTable = GameTableManager.GetInstance().GetTable("table_fish") as Table_Fish;
        GameObject fish = new GameObject();
        fish.transform.localPosition = Vector3.zero;
        fish.transform.localScale = Vector3.one;
        fish.transform.localEulerAngles = Vector3.zero;
        fish.name = "FishRoot";
        fish.tag = "FishRoot";
        mFishRoot = fish.transform;
        fish.layer = 8;

        InitializeFishCache();
    }

    public void Update(float dt)
    {
    }

    public void CreateFish()
    {
        float hBound = 120;
        float yBottom = -70;
        float yUp = 70;
        int[] temp = new int[2] { -1, 1 };
        int flag = temp[Random.Range(0, 2)];

        Record_Fish record = mFishTable.GetRecord(Random.Range(0, 2)) as Record_Fish;
        GameObject fish = GameObject.Instantiate(ResourcesManager.GetInstance().LoadLocalAsset("FishPrefabs/" + record.prefabName) as GameObject);
        fish.transform.parent = mFishRoot;
        fish.layer = 8;
        fish.transform.localPosition = new Vector3(hBound * flag, Random.Range(yBottom + 20, yUp - 20), Random.Range(96,96+20));
        fish.transform.eulerAngles = new Vector3(0, -90*flag, 0);
        Fish fishcom = fish.AddComponent<Fish>();
        fishcom.Speed = 30.0f * Random.Range(1,2);
        fishcom.FishPathData = PathConfigManager.GetInstance().GetPath(Random.Range(0,5));
        fishcom.FishPathData.renderPath = false;
    }

    public void CreateFish(int fishid, Vector3 position, Vector3 eulerangle, int pathid, float speed, float unactiveTime)
    {
        Record_Fish record = mFishTable.GetRecord(fishid) as Record_Fish;
        GameObject fish = GetUnActiveFishFromCache(fishid);
        if (fish == null)
        {
            return;
        }
        fish.transform.parent = mFishRoot;
        fish.layer = 8;
        fish.transform.localPosition = position;
        fish.transform.eulerAngles = eulerangle;
        fish.SetActive(true);
        Fish fishcom = fish.AddComponent<Fish>();
        fishcom.FishKindId = fishid;
        fishcom.Speed = speed;
        fishcom.FishPathData = PathConfigManager.GetInstance().GetPath(pathid);
        fishcom.UnActiveTime = unactiveTime;
        fishcom.FishRecord = record;
    }

    public GameObject GetUnActiveFishFromCache(int fishid)
    {
        List<GameObject> fishList = null;
        mUnActiveFishMap.TryGetValue(fishid, out fishList);
        if (fishList == null)
        {
            fishList = new List<GameObject>();
            mUnActiveFishMap[fishid] = fishList;
        }
        if (fishList.Count == 0)
        {
            Record_Fish record = mFishTable.GetRecord(fishid) as Record_Fish;
            if (record == null)
                return null;
            GameObject fish = GameObject.Instantiate(ResourcesManager.GetInstance().LoadLocalAsset("FishPrefabs/" + record.prefabName) as GameObject);
            //fishList.Add(fish);
            //SetUnActive(fish);
            return fish;
        }
        else
        {
            GameObject fish = fishList[fishList.Count - 1];
            fishList.RemoveAt(fishList.Count - 1);
            return fish;
        }
    }

    private void InitializeFishCache()
    {
        int idcnt = mFishTable.recordsList.Count;
        for (int i = 0; i < idcnt; i++)
        {
            Record_Fish record = mFishTable.recordsList[i] as Record_Fish;
            List<GameObject> fishList = new List<GameObject>();
            mUnActiveFishMap[record.id] = fishList;
            GameObject original = ResourcesManager.GetInstance().LoadLocalAsset("FishPrefabs/" + record.prefabName) as GameObject;
            if (original != null)
            {
                for (int j = 0; j < 50; j++)
                {
                    GameObject fish = GameObject.Instantiate(original);
                    fishList.Add(fish);
                    SetUnActive(fish);
                }
            }
        }
    }

    public void SetUnActive(GameObject fish)
    {
        if (fish != null)
        {
            fish.transform.localPosition = new Vector3(0, 0, -1000000);
            fish.SetActive(false);
        }
    }

    public void RecycleFish(Fish fish)
    {
        if (fish != null)
        {
            fish.transform.localPosition = new Vector3(0, 0, -1000000);
            mUnActiveFishMap[fish.FishKindId].Add(fish.gameObject);
            GameObject.Destroy(fish);
            fish.gameObject.SetActive(false);
        }
    }
}
