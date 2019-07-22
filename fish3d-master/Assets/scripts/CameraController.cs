using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject fish;
	Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - fish.transform.position;
	}
		
	// Update is called once per frame
	void Update () {
	
	}

	void LateUpdate () {
		transform.position = fish.transform.position + offset;
	}

}
