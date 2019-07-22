using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
    private float mAngle = 0;
    private Vector3 mDirection = Vector3.right;
    public float Angle
    {
        get { return mAngle; }
        set 
        {
            mAngle = value;
            transform.eulerAngles = new Vector3(0, 0, mAngle);
            mDirection = Quaternion.Euler(new Vector3(0, 0, mAngle)) * Vector3.right;
        }
    }

    private float mSpeed = 700;
    public float Speed
    {
        get { return mSpeed; }
        set { mSpeed = value; }
    }

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.localPosition += Time.deltaTime * mSpeed * mDirection;
        if (transform.localPosition.x < -640 || transform.localPosition.x > 640 || transform.localPosition.y < -360 || transform.localPosition.y > 360)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 worldpos1 = UICamera.currentCamera.transform.TransformPoint(transform.localPosition);
        Vector3 pos = UICamera.currentCamera.WorldToScreenPoint(worldpos1);
        Ray ray = Camera.main.ScreenPointToRay(pos);
        int layerMask = 1 << 8;
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 10000, layerMask))
        {
            Bomb bomb = (Instantiate(Resources.Load("BombPrefabs/Bomb_01")) as GameObject).GetComponent<Bomb>();
            bomb.transform.parent = UIManager.GetInstance().WeaponRoot;
            bomb.transform.localPosition = transform.localPosition;
            bomb.transform.localScale = Vector3.one * 0.5f;
            bomb.BombAction();
            Destroy(gameObject);

            Animation fishanim = hitInfo.transform.GetComponent<Animation>();
            fishanim.Stop();
            Fish fishcom = hitInfo.transform.GetComponent<Fish>();
            fishanim.Play(fishcom.FishRecord.deadAnimationName);

        }
	}
}
