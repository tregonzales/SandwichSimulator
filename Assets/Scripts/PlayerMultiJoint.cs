using System.Collections;
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
			if (!RBgrabbing && RBobj.canGrab) {
				RBgrabbing = true;
				RBjoint.connectedBody = RBobj.grabbableBody;
				RBjoint.xMotion = ConfigurableJointMotion.Locked;
				RBjoint.yMotion = ConfigurableJointMotion.Locked;
				RBjoint.zMotion = ConfigurableJointMotion.Locked;
			}
			if (RBgrabbing) {
				forcePointApply(RBforcePoint.transform.position);
			}
		}
		else {
			if (RBgrabbing) {
				RBgrabbing = false;
				RBjoint.connectedBody = null;
				RBjoint.xMotion = ConfigurableJointMotion.Free;
				RBjoint.yMotion = ConfigurableJointMotion.Free;
				RBjoint.zMotion = ConfigurableJointMotion.Free;
			}
		}

		if (XCI.GetButton(XboxButton.LeftBumper)) {
			if (!LBgrabbing && LBobj.canGrab) {
				LBgrabbing = true;
				LBjoint.connectedBody = LBobj.grabbableBody;
				LBjoint.xMotion = ConfigurableJointMotion.Locked;
				LBjoint.yMotion = ConfigurableJointMotion.Locked;
				LBjoint.zMotion = ConfigurableJointMotion.Locked;
			}
			if (LBgrabbing) {
				forcePointApply(LBforcePoint.transform.position);
			}
		}
		else {
			if (LBgrabbing) {
				LBgrabbing = false;
				LBjoint.connectedBody = null;
				LBjoint.xMotion = ConfigurableJointMotion.Free;
				LBjoint.yMotion = ConfigurableJointMotion.Free;
				LBjoint.zMotion = ConfigurableJointMotion.Free;
			}
		}

		
		if (XCI.GetAxis(XboxAxis.LeftTrigger) != 0) {
			if (!LTgrabbing && LTobj.canGrab) {
				LTgrabbing = true;
				LTjoint.connectedBody = LTobj.grabbableBody;
				LTjoint.xMotion = ConfigurableJointMotion.Locked;
				LTjoint.yMotion = ConfigurableJointMotion.Locked;
				LTjoint.zMotion = ConfigurableJointMotion.Locked;
			}
			if (LTgrabbing) {
				forcePointApply(LTforcePoint.transform.position, -1);
			}
		}
		else {
			if (LTgrabbing) {
				LTgrabbing = false;
				LTjoint.connectedBody = null;
				LTjoint.xMotion = ConfigurableJointMotion.Free;
				LTjoint.yMotion = ConfigurableJointMotion.Free;
				LTjoint.zMotion = ConfigurableJointMotion.Free;
			}
		}
		
		if (XCI.GetAxis(XboxAxis.RightTrigger) != 0) {
			if (!RTgrabbing && RTobj.canGrab) {
				RTgrabbing = true;
				RTjoint.connectedBody = RTobj.grabbableBody;
				RTjoint.xMotion = ConfigurableJointMotion.Locked;
				RTjoint.yMotion = ConfigurableJointMotion.Locked;
				RTjoint.zMotion = ConfigurableJointMotion.Locked;
			}
			if (RTgrabbing) {
				forcePointApply(RTforcePoint.transform.position, -1);
			}
		}
		else {
			if (RTgrabbing) {
				RTgrabbing = false;
				RTjoint.connectedBody = null;
				RTjoint.xMotion = ConfigurableJointMotion.Free;
				RTjoint.yMotion = ConfigurableJointMotion.Free;
				RTjoint.zMotion = ConfigurableJointMotion.Free;
			}
		}
		
	}

	//uses one force point 
	public void forcePointApply(Vector3 position, int YaxisFix = 1) {
		
		float xForce = XCI.GetAxis(XboxAxis.LeftStickX);
		float yForce = XCI.GetAxis(XboxAxis.LeftStickY);
		Vector3 movement = new Vector3(xForce, yForce*YaxisFix, 0.0f);
		Vector3 worldForce = transform.TransformDirection(movement);
		body.AddForceAtPosition(worldForce*force, position);
	}
}