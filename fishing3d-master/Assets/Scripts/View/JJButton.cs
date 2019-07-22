using UnityEngine;
using System.Collections;
using LuaInterface;

public class JJButton : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddClickCallback(LuaFunction luafunc,object param=null)
    {
        UIButton btn = this.gameObject.GetComponent<UIButton>();
        EventDelegate.Add(btn.onClick ,
                delegate()
                {
                    luafunc.Call(param);
                }
        );
    }
}
