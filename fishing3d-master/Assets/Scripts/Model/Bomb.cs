using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
    public Transform uiRoot;
    //private UISpriteAnimation mBombSprite;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BombAction()
    {
        //if (mBombSprite == null)
        //{
        //    mBombSprite = transform.FindChild("Animation").GetComponent<UISpriteAnimation>();
        //    mBombSprite.loop = false;
        //    mBombSprite.Callback = new UISpriteAnimation.AnimationCallback(BombCallback);
        //}
        //mBombSprite.Play();
    }

    public void BombCallback()
    {
        Destroy(gameObject);
    }
}
