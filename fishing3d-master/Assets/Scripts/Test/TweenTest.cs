using UnityEngine;
using System.Collections;

public class TweenTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //TweenScale tweenScale = this.GetComponent<TweenScale>();
        //tweenScale.AddOnFinished(this.ScaleFinished);
        transform.localScale = Vector3.zero;
        LeanTween.delayedCall(2, () => { 
            LeanTween.moveLocalY(gameObject, 100, 0.2f); 
            LeanTween.scale(gameObject,new Vector3(1f,1f,1f),0.4f);
            LeanTween.moveLocalY(gameObject, 0, 0.2f).setDelay(0.2f);
            LeanTween.moveLocalY(gameObject, 50, 0.15f).setDelay(0.4f);
            LeanTween.moveLocalY(gameObject, 0, 0.15f).setDelay(0.55f).setOnComplete(coinFlyToPlayer);
           
            
        });
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void coinFlyToPlayer()
    {
        LTDescrImpl a = LeanTween.moveLocal(gameObject, new Vector3(-640, -360, 0), .5f).setDelay(0.2f) as LTDescrImpl;
        a.setOnComplete(removeCoin);
        a.setOnCompleteParam(12);
    }

    void removeCoin(object obj)
    {
        print((int)obj);
    }

    void ScaleFinished()
    {
        TweenPosition tweenPosition = gameObject.AddComponent<TweenPosition>();
        tweenPosition.duration = 0.5f;
        tweenPosition.AddOnFinished(this.MoveUpFinished);
        tweenPosition.from = Vector3.zero;
        tweenPosition.to = new Vector3(0, 200, 0);
        tweenPosition.method = UITweener.Method.EaseInOut;
    }

    void MoveUpFinished()
    {
        TweenPosition tweenPosition = gameObject.AddComponent<TweenPosition>();
        tweenPosition.duration = 0.5f;
        //.AddOnFinished(this.MoveUpFinished);
        tweenPosition.from = new Vector3(0, 200, 0);
        tweenPosition.to = Vector3.zero;
        tweenPosition.method = UITweener.Method.EaseInOut;
    }

}
