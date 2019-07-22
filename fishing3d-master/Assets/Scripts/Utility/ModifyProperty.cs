using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class ModifyProperty : MonoBehaviour {
	public Vector3 relativeOffset;
	public int count;
	[SerializeField]
	private List<Transform> objArray;

	public Vector3 ringCenter;
	public float ringRadius;

	public enum ModifyType
	{
		Array = 0,
		Ring = 1
	}

	public ModifyType modifyType = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void applyModify()
	{
		if(count <= 1) return;
		if(objArray == null)
		{
			objArray = new List<Transform>();
		}

		if(modifyType == ModifyType.Array)
			applyArrayModify();
		else
			applyRingModify();
	}

	public void applyArrayModify()
	{
		if(count <= 1) return;
		if(objArray == null)
		{
			objArray = new List<Transform>();
		}
		for(int i = objArray.Count - 1; i > -1; i --)
		{
			if(objArray[i] == null)
				objArray.RemoveAt(i);
		}
		if(objArray.Count == 0)
		{
			objArray.Add(this.transform);
		}
		Vector3 rootPosition = this.transform.localPosition;
		for(int i = 1; i < objArray.Count; i ++)
		{
			objArray[i].parent = this.transform.parent;
			objArray[i].localPosition = rootPosition + new Vector3(relativeOffset.x * i,relativeOffset.y * i,relativeOffset.z * i);
			objArray[i].localScale = this.transform.localScale;
		}
		for(int i = objArray.Count; i < count ; i ++)
		{
			GameObject obj = (GameObject)GameObject.Instantiate((Object)gameObject);
			obj.transform.parent = this.transform.parent;
			obj.transform.localPosition = rootPosition + new Vector3(relativeOffset.x * i,relativeOffset.y * i,relativeOffset.z * i);
			obj.transform.localScale = this.transform.localScale;
			objArray.Add(obj.transform);
			ModifyProperty temp = obj.GetComponent<ModifyProperty>();
			if(temp)
				DestroyImmediate(temp);
		}
		if(objArray.Count > count)
		{
			for(int i = objArray.Count - 1; i > count-1; i --)
			{
				GameObject.DestroyImmediate(objArray[i].gameObject);
				objArray.RemoveAt(i);
			}
		}
	}

	public void applyRingModify()
	{
		for(int i = objArray.Count - 1; i > -1; i --)
		{
			if(objArray[i])
				GameObject.DestroyImmediate(objArray[i].gameObject);
		}
		objArray.Clear();

		float step = 2 * Mathf.PI / count;
		for(int i = 0; i < count; i ++)
		{
			GameObject obj = (GameObject)GameObject.Instantiate((Object)gameObject);
			obj.transform.parent = this.transform.parent;
            obj.transform.localPosition = new Vector3(this.transform.localPosition.x, ringRadius * Mathf.Sin(i * step) + ringCenter.y, ringRadius * Mathf.Cos(i * step) + ringCenter.x);
			obj.transform.localScale = this.transform.localScale;
            obj.transform.eulerAngles = transform.eulerAngles;
			objArray.Add(obj.transform);
			ModifyProperty temp = obj.GetComponent<ModifyProperty>();
			if(temp)
				DestroyImmediate(temp);
		}
	}
}
