using UnityEngine;
using System.Collections;

public class JJLabel : MonoBehaviour {

    UILabel textComponent;
	// Use this for initialization
	void Start ()
    {
        textComponent = this.GetComponent<UILabel>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public string Text
    {
        get 
        {
            return textComponent.text;
        }
        set
        {
            textComponent.text = value;
        }
    }

    public int FontSize
    {
        set { textComponent.fontSize = value; }
    }

    public int FontStyle
    {
        set 
        {
            textComponent.fontStyle = (UnityEngine.FontStyle)value;
        }
    }

    public Color FontColor
    {
        get
        {
            return textComponent.color;
        }
        set 
        {
            textComponent.color = value;
        }
    }
}
