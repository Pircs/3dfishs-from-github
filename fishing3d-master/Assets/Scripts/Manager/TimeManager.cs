using UnityEngine;
using System.Collections;

public class TimeManager
{
    private static TimeManager mInstance = null;

    private float mGameTime = 0;

    public float Gametime
    {
        get { return mGameTime; }
        set { mGameTime = value; }
    }

    private TimeManager()
    { }

    public static TimeManager GetInstance()
    {
        if (mInstance == null)
            mInstance = new TimeManager();

        return mInstance;
    }

    public void Initialize()
    {
        mGameTime = 0;
    }

    public void Update(float dt)
    {
        mGameTime += dt;
        if (mGameTime > 20)
        {
            mGameTime = 0;
        }
    }
}
