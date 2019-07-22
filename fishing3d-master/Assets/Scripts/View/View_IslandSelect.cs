using UnityEngine;
using System.Collections;

public class View_IslandSelect : ViewBase {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}

    public void OnClickedIsland()
    {
        FishManager.GetInstance().Initialize();
        FishData.GetInstance().GameState = GameState.MainLoop;
        gameObject.SetActive(false);

        UIManager.GetInstance().Show("Debug");
    }
}
