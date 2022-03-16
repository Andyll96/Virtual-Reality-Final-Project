using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Travel : MonoBehaviour {

	private Vector3 newPosition;
	private Vector2 orientation;
	private bool leftStickInUse;
	public GameObject leftController;

	void Start()
	{
		transform.LookAt(new Vector3(0,0,0),new Vector3(0,1,0));
		leftStickInUse = false;
	}

	// Update is called once per frame
	void Update () {
		GetVector ();
		GetDirection();
		
		//Debug.Log("WORLD FORWARD: " + leftController.transform.forward);
		

		//teleportation w/o change in telport orientation 
		if (leftController.GetComponent<PlayerInput>().TeleportButton() && leftStickInUse == false)
		{
			Debug.Log("STICK NOT USED");
			transform.position = newPosition;
		}
		//used when user wants to change teleport rotation
		else if (leftController.GetComponent<PlayerInput>().TeleportButton() && leftStickInUse == true)
		{
			Debug.Log("STICK USED");
			transform.position = newPosition;

			transform.LookAt(getLookAt(orientation) + newPosition);
		}

	}

	/// <summary>
	/// Gets the hit point of the user's teleport laser
	/// </summary>
	private void GetVector () {
		newPosition = leftController.GetComponent<PlayerInput> ().laserHitPoint;
		//Debug.Log("newPosition: " + newPosition);
	}
	
	private Vector3 getLookAt(Vector2 controller){

		//forward gets the direction in world space
		Vector3 forward = leftController.transform.forward;
		Vector2 newLookAtPosition = new Vector2();


		if (forward.x >= 0 && forward.z >= 0)
		{
			
		}
		
		return newLookAtPosition;
	}

	/// <summary>
	/// Gets Controller Direction Vector
	/// </summary>
	private void GetDirection(){
		if (leftController.GetComponent<PlayerInput>().leftStick != Vector2.zero)
		{
			leftStickInUse = true;
			orientation = leftController.GetComponent<PlayerInput>().leftStick;

		}
		else
		{
			leftStickInUse = false;
		}
	}
}