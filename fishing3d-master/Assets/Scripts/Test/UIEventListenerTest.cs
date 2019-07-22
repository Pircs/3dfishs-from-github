using UnityEngine;
using System.Collections;

public class UIEventListenerTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UIEventListener eventlistener = GetComponent<UIEventListener>();
        eventlistener.onClick = OnClickSprite;
        eventlistener.onPress = OnPressedSprite;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClickSprite(GameObject obj)
    {
        print(obj.name);
    }

    void OnPressedSprite(GameObject obj,bool isPressed)
    {
        print(isPressed);
    }
}
