using UnityEngine;
using System.Collections;
using System;

public class fish : MonoBehaviour {

	private Rigidbody rigidFishBody;
	public float speed = 100.0f;

	private int fishRotation = 45;
	private bool fishRotationRight = true;

	// Use this for initialization
	void Start () {
		rigidFishBody = GetComponent<Rigidbody>();
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rigidFishBody.AddForce(movement*speed*Time.deltaTime);
	}
		
	// Update is called once per frame
	void Update () {

		rotateFish();
	}

	void rotateFish() {
		transform.Rotate(Vector3.down * Time.deltaTime * fishRotation);

		if (fishRotationRight && transform.rotation.eulerAngles.y < 330 && transform.rotation.eulerAngles.y > 300) {
			fishRotation *= -1;
			fishRotationRight = !fishRotationRight;
		}

		if (!fishRotationRight && transform.rotation.eulerAngles.y > 30 && transform.rotation.eulerAngles.y < 60) {
			fishRotation *= -1;
			fishRotationRight = !fishRotationRight;
		}
	}
}
