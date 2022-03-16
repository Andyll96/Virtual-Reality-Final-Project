using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimbal : MonoBehaviour {

	public GameObject RightController;
	public GameObject LeftController;

	private Vector3 LeftPosition;
	private Vector3 RightPosition;

	// Use this for initialization
	void Start () {
		transform.position = GetMidPoint ();
	}

	// Update is called once per frame
	void Update () {
		SetControllerPositions ();

		SetCamera ();
	}

	public Vector3 GetMidPoint () {
		return Vector3.Lerp (RightPosition, LeftPosition, 0.5f);
	}

	private void SetControllerPositions () {
		LeftPosition = LeftController.GetComponent<PlayerInput> ().ControllerPosition;
		RightPosition = RightController.GetComponent<PlayerInput> ().ControllerPosition;

		//Debug.Log ("Right Controller: " + RightControllerPosition);
		//Debug.Log ("Left Controller: " + LeftControllerPosition);
	}

	private void SetCamera () {
		transform.position = GetMidPoint ();
	}
}