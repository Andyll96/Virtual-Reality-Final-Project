using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewFinder : MonoBehaviour {

	public GameObject PlayerCamera;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.LookAt (PlayerCamera.transform, Vector3.up);
		transform.Rotate (new Vector3 (90, 0, 0));
	}

}