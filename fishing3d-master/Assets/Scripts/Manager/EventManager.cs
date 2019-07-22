using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;

public class FishEvent
{
    public int et;  //eventtype
    public int t;   //time
    public int begint;  //begintime
    public int endt;  //endtime

    public FishEvent()
    { }
    public FishEvent(int eventtime, int time)
    {
        et = eventtime;
        t = time;
    }
}

public class EventManager
{
	private static EventManager mInstance = null;
    private EventManager()
    { }

    private List<FishEvent> mEventList = new List<FishEvent>();

    private FishEvent mEvent = null;

    private float mTimerTeam = 0;

    private float mTimerSingleFish = 0;
    bool testseason = true;

    public static EventManager GetInstance()
    {
        if (mInstance == null)
            mInstance = new EventManager();

        return mInstance;
    }

    public void Initialize()
    {
        mEventList = new List<FishEvent>();
        //mEventList.Add(new FishEvent(0, 60));
        //mEventList.Add(new FishEvent(1, 60));
        //string jsonStr = JsonMapper.ToJson(mEventList);
        //string filepath = Application.dataPath + "/Resources/event.bytes";
        //JsonUtil.Save(jsonStr, filepath);

        LoadEventConfig();
        CaculateBeginAndEndTime();
    }

    public void Update(float dt)
    {
        if (FishData.GetInstance().GameState == GameState.MainLoop)
        {
            mEvent = GetEvent(TimeManager.GetInstance().Gametime);
            if (mEvent == null) return;
            switch (mEvent.et)
            {
                case 0:
                    testseason = true;
                    mTimerTeam += dt;
                    mTimerSingleFish += dt;
                    if (mTimerTeam > 5f)
                    {
                        mTimerTeam = 0;
                        TestTeam();
                    }
                    if (mTimerSingleFish > 0.3f)
                    {
                        mTimerSingleFish = 0;
                        TestOneFish();
                    }
                    break;
                case 1:
                    if (testseason)
                    {
                        TestFishSeason();
                        testseason = false;
                    }
                    
                    break;
                case 2:
                    break;
                default:
                    break;
            }
        }
    }

    public void LoadEventConfig()
    {
        TextAsset textAsset = ResourcesManager.GetInstance().LoadLocalAsset("event") as TextAsset;
        if (textAsset != null)
        {
            mEventList = JsonMapper.ToObject<List<FishEvent>>(textAsset.text);
        }
    }

    public void CaculateBeginAndEndTime()
    {
        if (mEventList != null)
        {
            int t = 0;
            foreach (FishEvent eventdata in mEventList)
            {
                eventdata.begint = t;
                eventdata.endt = t + eventdata.t;
                t = eventdata.endt;
            }
        }
    }

    public FishEvent GetEvent(float t)
    {
        foreach (FishEvent eventdata in mEventList)
        {
            if (t < eventdata.endt)
                return eventdata;
        }
        return null;
    }

    public void TestTeam()
    {
        float hBound = 120;
        float yBottom = -70;
        float yUp = 70;
        int[] temp = new int[2] { -1, 1 };
        int flag = temp[Random.Range(0, 2)];
        float speed = Random.Range(40, 70);
        int pathid = Random.Range(0, 5);
        Vector3 headPosition = new Vector3(hBound * flag, Random.Range(yBottom + 20, yUp - 20), Random.Range(96, 96 + 20));
        //Vector3 bornEulerAngles = new Vector3(Random.Range(-20, 20), Random.Range(80, 100) * flag, 0);
        Vector3 bornEulerAngles = new Vector3(0, -90 * flag, 0);
        int randomCnt = Random.Range(5,11);
        for (int i = 0; i < randomCnt; i++)
        {
            Vector3 offset = new Vector3(i * 15 * flag, 0, 0);
            //offset = Quaternion.Euler(bornEulerAngles) * offset;
            FishManager.GetInstance().CreateFish(0, headPosition + offset, bornEulerAngles, pathid, speed, i * 15.0f / speed);
        }
    }

    public void TestOneFish()
    {
        float hBound = 120;
        float yBottom = -70;
        float yUp = 70;
        int[] temp = new int[2] { -1, 1 };
        int flag = temp[Random.Range(0, 2)];
        float speed = Random.Range(40, 70);
        int pathid = Random.Range(0, 5);
        int[] temp1 = new int[10] { 0, 0, 0, 0, 0, 2, 0, 0, 0, 1 };
        int fishid = temp1[Random.Range(0, 10)];
        if (fishid == 1 )
            speed = 30;
        if (fishid == 2)
        {
            speed = 20;
            pathid = 0;
        }
        Vector3 headPosition = new Vector3(hBound * flag, Random.Range(yBottom + 20, yUp - 20), Random.Range(96, 96 + 20));
        Vector3 bornEulerAngles = new Vector3(Random.Range(-20, 20), -Random.Range(80, 100) * flag, 0);
        FishManager.GetInstance().CreateFish(fishid, headPosition, bornEulerAngles, pathid, speed, 0);
    }

    public void TestFishSeason()
    {
        FishSeason season = SeasonConfigManager.GetInstance().GetSeason(Random.Range(0,3));
        if (season == null)
            return;
        foreach (OneWave wave in season.waves)
        {
            foreach (WaveFish fish in wave.fishes)
            {
                FishManager.GetInstance().CreateFish(fish.fkid,fish.p, wave.ea, wave.pathid, wave.speed, -fish.p.x / 20.0f);
            }
        }
    }
}
