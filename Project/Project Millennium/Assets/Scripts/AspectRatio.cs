using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatio : MonoBehaviour {

	public bool SixteenByNine;
	public bool FourByThree;

	public float width;


	// Use this for initialization
	void Start () {
		width = 0.04f;
		SixteenByNine = true;
		FourByThree = false;	
	}
	
	// Update is called once per  frame
	void Update () {
		if (SixteenByNine)
		{
			FourByThree = false;
			transform.localScale = new Vector3( width, transform.localScale.y,(transform.localScale.x / 16) * 9.0f);
		}else if (FourByThree)
		{
			SixteenByNine = false;
			transform.localScale = new Vector3( width, transform.localScale.y,(transform.localScale.x /4) * 3.0f);
		}
	}
}