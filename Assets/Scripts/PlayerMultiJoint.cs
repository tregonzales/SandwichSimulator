﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerMultiJoint : MonoBehaviour {

	//the corner points and their respective force points
	private GameObject RB;
	public GameObject RBforcePoint;
	private GameObject LB;
	public GameObject LBforcePoint;
	private GameObject RT;
	public GameObject RTforcePoint;
	private GameObject LT;
	public GameObject LTforcePoint;
	private Rigidbody body;

	//the children colliders
	playerChildren RBobj;
	playerChildren LBobj;
	playerChildren RTobj;
	playerChildren LTobj;

	//the four joints on each corner
	private Component[] joints;
	private ConfigurableJoint RBjoint;
	private ConfigurableJoint LBjoint;
	private ConfigurableJoint RTjoint;
	private ConfigurableJoint LTjoint;

	public bool RBgrabbing;
	public bool LBgrabbing;
	public bool RTgrabbing;
	public bool LTgrabbing;
	
	//force to apply on body
	public float force;

	// Use this for initialization
	void Start () {
		RB = gameObject.transform.GetChild(1).gameObject;
		LB = gameObject.transform.GetChild(2).gameObject;
		LT = gameObject.transform.GetChild(3).gameObject;
		RT = gameObject.transform.GetChild(4).gameObject;

		body = gameObject.transform.GetComponent<Rigidbody>();

		joints = GetComponents(typeof(ConfigurableJoint));
		RBjoint = (ConfigurableJoint)joints[0];
		LBjoint = (ConfigurableJoint)joints[1];
		LTjoint = (ConfigurableJoint)joints[2];
		RTjoint = (ConfigurableJoint)joints[3];

		RBobj = RB.GetComponent<playerChildren>();
		LBobj = LB.GetComponent<playerChildren>();
		RTobj = RT.GetComponent<playerChildren>();
		LTobj = LT.GetComponent<playerChildren>();

		RBgrabbing = false;
		LBgrabbing = false;
		RTgrabbing = false;
		LTgrabbing = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (XCI.GetButton(XboxButton.RightBumper)) {
			updateJointAndForce(true, ref RBgrabbing, ref RBobj, ref RBjoint, ref RBforcePoint);
		}
		else {
			updateJointAndForce(false, ref RBgrabbing, ref RBobj, ref RBjoint, ref RBforcePoint);
		}

		if (XCI.GetButton(XboxButton.LeftBumper)) {
			updateJointAndForce(true, ref LBgrabbing, ref LBobj, ref LBjoint, ref LBforcePoint);
		}
		else {
			updateJointAndForce(false, ref LBgrabbing, ref LBobj, ref LBjoint, ref LBforcePoint);
		}

		
		if (XCI.GetAxis(XboxAxis.LeftTrigger) != 0) {
			updateJointAndForce(true, ref LTgrabbing, ref LTobj, ref LTjoint, ref LTforcePoint, -1);
		}
		else {
			updateJointAndForce(false, ref LTgrabbing, ref LTobj, ref LTjoint, ref LTforcePoint, -1);
		}
		
		if (XCI.GetAxis(XboxAxis.RightTrigger) != 0) {
			updateJointAndForce(true, ref RTgrabbing, ref RTobj, ref RTjoint, ref RTforcePoint, -1);
		}
		else {
			updateJointAndForce(false, ref RTgrabbing, ref RTobj, ref RTjoint, ref RTforcePoint, -1);
		}
		
	}

	private void updateJointAndForce(bool pressed, ref bool grab, ref playerChildren obj, ref ConfigurableJoint joint, ref GameObject forcePoint, int YaxisFix = 1) {

		if (!pressed && grab) {
				grab = false;
				joint.connectedBody = null;
				joint.xMotion = ConfigurableJointMotion.Free;
				joint.yMotion = ConfigurableJointMotion.Free;
				joint.zMotion = ConfigurableJointMotion.Free;
		}
		else if (!grab && obj.canGrab && pressed) {
				grab = true;
				joint.connectedBody = obj.grabbableBody;
				joint.xMotion = ConfigurableJointMotion.Locked;
				joint.yMotion = ConfigurableJointMotion.Locked;
				joint.zMotion = ConfigurableJointMotion.Locked;
			}
		else if (grab) {
			forcePointApply(forcePoint.transform.position, YaxisFix);
		}
	}

	public void forcePointApply(Vector3 position, int YaxisFix = 1) {
		
		float xForce = XCI.GetAxis(XboxAxis.LeftStickX);
		float yForce = XCI.GetAxis(XboxAxis.LeftStickY);
		Vector3 movement;
		if ((LTgrabbing && LBgrabbing) || (RTgrabbing && RBgrabbing)) {
			if (LTgrabbing || LBgrabbing) {
				YaxisFix = -1;
			}
			else {
				YaxisFix = 1;
			}
			movement = new Vector3(0.0f, xForce*YaxisFix, yForce);
		}
		else {
			movement = new Vector3(xForce, yForce*YaxisFix, 0.0f);
		}
		Vector3 worldForce = transform.TransformDirection(movement);
		body.AddForceAtPosition(worldForce*force, position);
	}
}