using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITest : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Button closeBtn = transform.FindChild("Btn_Close").GetComponent<Button>();
        closeBtn.onClick.AddListener(this.OnClickBtnClose);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClickBtnClose()
    {
        print("close clicked!");
    }
}
