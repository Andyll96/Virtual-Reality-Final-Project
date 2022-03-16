using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaybackController : MonoBehaviour {

	private Animator anim;
	public GameObject leftController;

	private bool playPausePress;
	private bool play;

	private bool playback;

	private bool rewind;
	private bool forward;

	private Vector3 startOrientation;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		play = false;
		rewind = false;
		forward = false;
	}
	
	// Update is called once per frame
	void Update () {
		playPausePress = leftController.GetComponent<PlayerInput>().playPauseButton();
		playback = leftController.GetComponent<PlayerInput>().playbackButton();

		PlayandPause();
		Playback();
	}

	public void Playback(){

		//Debug.Log("Test" + leftController.GetComponent<PlayerInput>().controllerPose.transform.rotation.eulerAngles);

		if (playback == true && startOrientation == Vector3.zero)
		{
			startOrientation = leftController.GetComponent<PlayerInput>().controllerPose.transform.rotation.eulerAngles;
			Debug.Log("startOrientation: " + startOrientation);
		} else if(playback == false){
			startOrientation = Vector3.zero;
		}

		if (leftController.GetComponent<PlayerInput>().controllerPose.transform.rotation.eulerAngles.z > 180 && leftController.GetComponent<PlayerInput>().controllerPose.transform.rotation.eulerAngles.z < 360)
			forward = true;
 		else
			forward = false;
		
		if (leftController.GetComponent<PlayerInput>().controllerPose.transform.rotation.eulerAngles.z > 0 && leftController.GetComponent<PlayerInput>().controllerPose.transform.rotation.eulerAngles.z < 60)
			rewind = true;
		else
			rewind = false;

		if (playback == true && forward)
		{
			Debug.Log("FAST FORWARD");
			anim.SetFloat("Play", 3.0f);
			anim.speed = 1;
		}else if (playback == true && rewind)
		{
			Debug.Log("REWIND");
			anim.SetFloat("Play", -3.0f);
			anim.speed = 1;
		}
	}

	public void PlayandPause(){
		if(playPausePress){
			play = !play;
			Debug.Log("PLAYPAUSE PRESSED!: " + play);
		}

		if (play == true)
		{
			anim.SetFloat("Play", 1.0f);
			anim.speed = 1;

		}
		else
		{
			anim.SetFloat("Play", 0.0f);
			anim.speed = 0;

		}
	}
}
