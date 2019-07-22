using UnityEngine;
using System.Collections;

public class FishData
{

	private static FishData mInstance = null;
    private FishData()
    { }

    private GameState mGameState = GameState.Login;

    public GameState GameState
    {
        get { return mGameState; }
        set { mGameState = value; }
    }

    public static FishData GetInstance()
    {
        if (mInstance == null)
            mInstance = new FishData();

        return mInstance;
    }

    public void Initialize()
    {
 
    }
	
	// Update is called once per frame
	void Update (float dt)
    {
	
	}
}
