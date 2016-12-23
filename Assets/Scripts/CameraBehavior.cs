﻿using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

	private float curVel;
	private float minVel;
	private float maxVel;
	private float velStep;
	private Vector3 prevForward;
	private bool isGazing;

	// Use this for initialization
	void Start () {
		this.minVel = 0.01f;
		this.maxVel = 0.2f;
		this.curVel = this.minVel;
		this.velStep = 0.001f;
		this.prevForward = new Vector3 (0, 0, 0);
		this.isGazing = false;
	}
	
	// Update is called once per frame
	void Update () {
		// Move forward in the direction that the camera is facing
		Transform transform = this.GetComponent<Transform> ();
		Vector3 pos = transform.position;

		if (transform.forward == this.prevForward) {
			this.curVel += this.velStep;
		}
		else {
			// this.curVel -= this.velStep;
			this.curVel = this.minVel;
		}

		this.curVel = Mathf.Min (Mathf.Max (this.curVel, this.minVel), this.maxVel);

		// Steadily back away if I'm gazing
		if (this.isGazing) {
			this.curVel = this.minVel * -1;
		}

		Vector3 mvmt = transform.forward * this.curVel;
		this.prevForward = transform.forward;
		//Quaternion rot = transform.rotation;
		//Debug.Log (rot);
		pos.x += mvmt.x;
		pos.z += mvmt.z;
		transform.position = pos;
	}

	public void SetIsGazing(bool gazing) {
		this.isGazing = gazing;
	}
}
