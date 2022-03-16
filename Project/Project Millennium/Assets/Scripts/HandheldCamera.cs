using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RockVR.Video;

public class HandheldCamera : MonoBehaviour {

	public GameObject Gimbal;
	public Camera headset;

	public GameObject RightController;
	public GameObject LeftController;

	private Vector3 LeftPosition;
	private Vector3 RightPosition;

	private Quaternion LeftRotation;
	private Quaternion RightRotation;

	private Vector3 MidPoint;

	private Vector2 zoom;

	public Camera filmCamera;

	private bool record;
	public VideoCaptureCtrl ctrl;
	public Material viewFinder;

	// Use this for initialization
	void Start () {
		record = false;
	}

	// Update is called once per frame
	void Update () {
		GetControllerRotation ();
		GetControllerPosition ();
		GetControllerMidPoint ();
		//Debug.Log ("L: " + LeftPosition + " R: " + RightPosition + " MidPoint: " + MidPoint);
		SetCamera ();
		Zoom();
		Record();
	}

	private void Record(){
		record = RightController.GetComponent<PlayerInput>().record;
		if(record){
			Debug.Log("RECORD");
			ctrl.StartCapture();
			viewFinder.SetColor("_Color", Color.red);
		}else
		{
			Debug.Log("NOT RECORDING");
			ctrl.StopCapture();
			viewFinder.SetColor("_Color", Color.white);

		}
	}

	private void SetCamera () {
		//averages the rotation values of both controllers and assigns it to the camera rotation
		//Debug.Log ("Left: " + LeftRotation.eulerAngles);
		//Debug.Log ("Right: " + RightRotation.eulerAngles);

		//Sets Tilt and Pan Rotation
		Quaternion AvgControllerRotation = Quaternion.Lerp (LeftRotation, RightRotation, 0.5f);
		transform.localRotation = AvgControllerRotation;

		
		//Vector3 test = transform.localRotation.eulerAngles;

		//transform.localRotation = Quaternion.Euler (test.x + 90, test.y, test.z);

		XZTilt ();
	}

	private void Zoom(){
		zoom = RightController.GetComponent<PlayerInput>().rightStick;
		if (zoom.y > 0.5)
		{
			Debug.Log("ZOOM IN: Decrease angle " + filmCamera.fieldOfView);
			if (filmCamera.fieldOfView < 3)
				filmCamera.fieldOfView = 3;

			else if(filmCamera.fieldOfView > 100)
				filmCamera.fieldOfView = 100;
			else 
				filmCamera.fieldOfView -= 0.5f;
		}
		else if (zoom.y < -0.5)
		{
			Debug.Log("ZOOM OUT: Increase angle " + filmCamera.fieldOfView);
			if (filmCamera.fieldOfView < 3)
				filmCamera.fieldOfView = 3;

			else if(filmCamera.fieldOfView > 100)
				filmCamera.fieldOfView = 100;
			else 
				filmCamera.fieldOfView += 1;
		}
	}

	private void GetControllerPosition () {
		LeftPosition = LeftController.GetComponent<PlayerInput> ().ControllerPosition;
		RightPosition = RightController.GetComponent<PlayerInput> ().ControllerPosition;
	}

	/// <summary>
	/// Gets the Orientation of Left and Right Controller
	/// </summary>
	private void GetControllerRotation () {
		LeftRotation = LeftController.GetComponent<PlayerInput> ().ControllerRotation;
		RightRotation = RightController.GetComponent<PlayerInput> ().ControllerRotation;
	}

	/// <summary>
	/// Gets MidPoint using function defined in Gimbal Class
	/// </summary>
	private void GetControllerMidPoint () {
		MidPoint = Gimbal.GetComponent<Gimbal> ().GetMidPoint ();
		//Debug.Log ("MidPoint: " + MidPoint);
	}

	private void XZTilt () {

		Debug.DrawLine (RightPosition, MidPoint, Color.red);
		Debug.DrawLine (LeftPosition, MidPoint, Color.green);

		float RX = RightPosition.x - MidPoint.x;
		float RZ = RightPosition.z - MidPoint.z;

		float RTiltAngle = Mathf.Atan2 (RX, RZ) * Mathf.Rad2Deg;

		float LX = LeftPosition.x - MidPoint.x;
		float LZ = LeftPosition.z - MidPoint.z;

		float LTiltAngle = Mathf.Atan2 (LX, LZ) * Mathf.Rad2Deg;

		//Debug.Log ("L: " + LeftPosition + " R: " + RightPosition + " MidPoint: " + MidPoint + "LTiltAngle: " + LTiltAngle + " RTiltAngle: " + RTiltAngle);
	}
}