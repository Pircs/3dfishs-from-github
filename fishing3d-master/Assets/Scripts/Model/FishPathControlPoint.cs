using UnityEngine;
using System.Collections;
using System;

public class FishPathControlPoint:ScriptableObject
{
	public float mTime;
	public float mSpeedScale;
	public Vector2 mRotationChange;	//x,y
	public bool highLight;
	public Color color;

	public FishPathControlPoint(float time ,float speedscale,Vector2 rchange,bool highlight,Color color)
	{
		mTime = time;
		mSpeedScale = speedscale;
		mRotationChange = rchange;
		highLight = highlight;
		this.color = color;
	}
	
	public FishPathControlPoint()
	{
		mTime = 1;
		mSpeedScale = 1;
		mRotationChange = Vector2.zero;
		highLight = false;
		this.color = Color.red;
	}
}
