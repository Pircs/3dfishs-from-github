using UnityEngine;
using System.Collections;

public class TouchEvent : MonoBehaviour {

    public Camera uiCamera;
    bool isTouched = false;
    public Cannon cannon;
    public Transform uiRoot;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    public void OnPressed()
    {
        isTouched = true;
    }

    public void OnReleased()
    {
        isTouched = false;

        Vector3 touchPos = MathUtil.ScreenPos_to_NGUIPos(Input.mousePosition);
        Vector3 from = touchPos - new Vector3(0, -315);
        float angle = Vector3.Angle(from, Vector3.right);
        cannon.transform.eulerAngles = new Vector3(0, 0, angle);
        cannon.FireAction();
        return;
        Bullet bullet = (Instantiate(Resources.Load("BulletPrefabs/Bullet")) as GameObject).GetComponent<Bullet>();
        bullet.Angle = cannon.transform.eulerAngles.z;
        bullet.transform.parent = UIManager.GetInstance().WeaponRoot;
        bullet.transform.localPosition = cannon.transform.localPosition + from.normalized * 50;
        bullet.transform.localScale = Vector3.one;
    }

    
}
