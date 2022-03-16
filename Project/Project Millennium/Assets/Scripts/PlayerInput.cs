using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerInput : MonoBehaviour {

	public SteamVR_Input_Sources handType; 
	public SteamVR_Behaviour_Pose controllerPose;

	public SteamVR_Action_Boolean recordAction;
	public SteamVR_Action_Boolean toggleCamAction;

	public SteamVR_Action_Boolean playPauseAction;
	public SteamVR_Action_Boolean playbackAction;

	public SteamVR_Action_Boolean laserAction;
	public SteamVR_Action_Boolean teleportAction;

	public SteamVR_Action_Vector2 orientAction;
	public SteamVR_Action_Vector2 zoomAction;

	public GameObject gimbal;
	public bool cameraOn;

	[HideInInspector]
	public Vector2 leftStick;
	[HideInInspector]
	public Vector2 rightStick;

	[HideInInspector]
	public Vector3 ControllerPosition;
	[HideInInspector]
	public Quaternion ControllerRotation;

	[HideInInspector]
	public float RightTiltAngle;
	[HideInInspector]
	public float LeftTiltAngle;

	private LineRenderer laser;
	[HideInInspector]
	public Vector3 laserHitPoint;
	public GameObject arrowPrefab;

	[HideInInspector]
	public bool record;

	/// <summary>
	/// Used for Initialization
	/// </summary>
	void Start () {
		cameraOn = false;
		record = false;
		gimbal.SetActive (cameraOn);
		arrowPrefab.SetActive(false);

		laser = GetComponent<LineRenderer>();
		laser.enabled = false;
		laser.useWorldSpace = true;
	}

	/// <summary>
	/// Update is called once every frame
	/// </summary>
	void Update () {
		SetControllerPosition ();
		SetControllerRotation ();
		ToggleCamera ();
		ToggleRecord();
		LaserButton();
		TeleportButton();
		Orient ();
		Zoom();

		if (playPauseButton())
		{
			Debug.Log("PlayPause Tapped");
		} else if (playbackButton())
		{
			Debug.Log("Playback Held");
		}
		//playPauseButton();
		//playbackButton();
		
		//TODO: Move this to the Travel 
		if (LaserButton())
		{
			laser.enabled = true;
			arrowPrefab.SetActive(true);

			RaycastHit hit;
			if(Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, Mathf.Infinity)){
				laser.enabled = true;
				Debug.DrawLine(transform.position, hit.point);
				laserHitPoint = hit.point;
				laser.SetPosition(0, transform.position);
				laser.SetPosition (1, laserHitPoint);
				arrowPrefab.transform.position = laserHitPoint;
				//Debug.Log("Hit point(hit): " + hit.point);
			}else
			{
				laser.enabled = false;
				arrowPrefab.SetActive(false);
			}		
		} else
		{
			laser.enabled = false;
			arrowPrefab.SetActive(false);
		}
		
	}

	/// <summary>
	/// Sets the Controller Position
	/// </summary>
	private void SetControllerPosition () {
		ControllerPosition = controllerPose.transform.position;
		//Debug.Log (handType + ": " + ControllerPosition);
	}

	private void SetControllerRotation () {
		ControllerRotation = controllerPose.transform.rotation;
	}

	/// <summary>
	/// Checks if Camera Toggle Button has been clicked and Turns on and off the camera
	/// </summary>
	private void ToggleCamera () {
		if (ToggleCamButton ()) {
			cameraOn = !cameraOn;
			gimbal.SetActive (cameraOn);
			Debug.Log (cameraOn);
		}
	}

	private void ToggleRecord(){
		if (RecordButton())
		{
			record = !record;
		}
	}

	/// <summary>
	/// Gets controller input to turn on or off film camera
	/// </summary>
	/// <returns>bool</returns>
	public bool ToggleCamButton () {
		//Debug.Log (toggleCamAction.GetState (handType));
		return toggleCamAction.GetStateDown (handType);
	}

	public bool RecordButton(){
		return recordAction.GetStateDown(handType);
	}

	public bool LaserButton(){
		//Debug.Log("laser held down");
		return laserAction.GetState(handType);
	}


	public bool TeleportButton(){
		return teleportAction.GetStateDown(handType);
	}

	public void Orient () {
		leftStick = orientAction.GetAxis (handType);
		//Debug.Log("Left Stick: " + leftStick);
	}

	public void Zoom(){
		rightStick = zoomAction.GetAxis(handType);
		//Debug.Log("Right Stick: " + rightStick);
	}

	public bool playPauseButton(){
		return playPauseAction.GetStateDown(handType);
	}

	public bool playbackButton(){
		return playbackAction.GetState(handType);
	}
}