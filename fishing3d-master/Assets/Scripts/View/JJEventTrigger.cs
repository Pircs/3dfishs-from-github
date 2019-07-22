using UnityEngine;
using System.Collections;
using LuaInterface;

public class JJEventTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddPressCallback(LuaFunction luafunc, object param)
    {
        UIEventTrigger trigger = this.gameObject.GetComponent<UIEventTrigger>();
        if (trigger)
        {
            EventDelegate.Add(trigger.onPress,
                delegate()
                {
                    luafunc.Call(param);
                }
            );
        }
        else
        {
            Debug.Log("no trigger!");
        }
    }

    public void AddReleaseCallback(LuaFunction luafunc, object param)
    {
        UIEventTrigger trigger = this.gameObject.GetComponent<UIEventTrigger>();
        if (trigger)
        {
            EventDelegate.Add(trigger.onRelease,
                delegate()
                {
                    luafunc.Call(param);
                }
            );
        }
        else
        {
            Debug.Log("no trigger!");
        }
    }
}
