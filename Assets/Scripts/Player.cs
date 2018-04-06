using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class Player : MonoBehaviour {

	public GameObject RBforcePoint;
	public GameObject LBforcePoint;
	public GameObject RTforcePoint;
	public GameObject LTforcePoint;
	private Rigidbody body;
	private ConfigurableJoint joint;
	//force to apply on body
	public float force;
	//have we locked the joint
	private bool lockedOrientation;
	//current hold on object defined by button
	private string curHold;

	// Use this for initialization
	void Start () {
		body = gameObject.transform.GetComponent<Rigidbody>();
		joint = gameObject.transform.GetComponent<ConfigurableJoint>();
		lockedOrientation = false;
		curHold = "none";
	}
	
	// Update is called once per frame
	void Update () {

		if ((XCI.GetButton(XboxButton.RightBumper) && XCI.GetButton(XboxButton.LeftBumper))
				&& (XCI.GetAxis(XboxAxis.LeftTrigger) != 0 || XCI.GetAxis(XboxAxis.RightTrigger)!=0)) {
			lockOrientation(Vector3.zero, 4);
			curHold = "2bt";
		}
		else if ((XCI.GetAxis(XboxAxis.LeftTrigger) != 0 && XCI.GetAxis(XboxAxis.RightTrigger) != 0)
				&& (XCI.GetButton(XboxButton.RightBumper) || XCI.GetButton(XboxButton.LeftBumper))) {
			lockOrientation(Vector3.zero, 4);
			curHold = "2tb";
		}
		//top lock z
		else if (XCI.GetButton(XboxButton.RightBumper) && XCI.GetButton(XboxButton.LeftBumper)) {
			forceTwoPointApply(RBforcePoint.transform.position, LBforcePoint.transform.position, true, "top", 1);
		}
		//right
		else if (XCI.GetButton(XboxButton.RightBumper) && XCI.GetAxis(XboxAxis.RightTrigger) != 0) {
			forceTwoPointApply(RBforcePoint.transform.position, RTforcePoint.transform.position, false, "right", 3, 1);
		}
		//cross top right to bottom left so nothing happens
		else if (XCI.GetButton(XboxButton.RightBumper) && XCI.GetAxis(XboxAxis.LeftTrigger) != 0) {
			lockOrientation(Vector3.zero, 4);
			curHold = "crl";
		}
		//left
		else if (XCI.GetButton(XboxButton.LeftBumper) && XCI.GetAxis(XboxAxis.LeftTrigger) != 0) {
			forceTwoPointApply(LBforcePoint.transform.position, LTforcePoint.transform.position, false, "left", 3, 1, -1);
		}
		//bottom 
		else if (XCI.GetAxis(XboxAxis.RightTrigger) != 0 && XCI.GetAxis(XboxAxis.LeftTrigger) != 0) {
			forceTwoPointApply(RTforcePoint.transform.position, LTforcePoint.transform.position, true, "bottom", 1, 1, -1);
		}
		//cross top left to bottom right so nothing happens
		else if (XCI.GetAxis(XboxAxis.RightTrigger) != 0 && XCI.GetButton(XboxButton.LeftBumper)) {
			lockOrientation(Vector3.zero, 4);
			curHold = "clr";
		}
		else if (XCI.GetButton(XboxButton.RightBumper)) {
			forceOnePointApply(RBforcePoint.transform.position, "tr", 0);
		}
		else if (XCI.GetButton(XboxButton.LeftBumper)) {
			forceOnePointApply(LBforcePoint.transform.position, "tl", 0);
		}
		else if (XCI.GetAxis(XboxAxis.LeftTrigger) != 0) {
			forceOnePointApply(LTforcePoint.transform.position, "bl", 0, -1);
		}
		else if (XCI.GetAxis(XboxAxis.RightTrigger) != 0) {
			forceOnePointApply(RTforcePoint.transform.position, "br", 0, -1);
		}
		else {
			lockedOrientation = false;
			curHold = "none";
			joint.anchor = Vector3.zero;
			joint.xMotion = ConfigurableJointMotion.Free;
			joint.yMotion = ConfigurableJointMotion.Free;
			joint.zMotion = ConfigurableJointMotion.Free;
			joint.angularXMotion = ConfigurableJointMotion.Free;
			joint.angularYMotion = ConfigurableJointMotion.Free;
			joint.angularZMotion = ConfigurableJointMotion.Free;
		}
	}
	//new anchor point and rotation axis depending on corners held (1:x, 2:y, 3:z)
	public void lockOrientation(Vector3 anchor, int rotationAxis) {
		lockedOrientation = true;
		joint.anchor = anchor;
		joint.xMotion = ConfigurableJointMotion.Locked;
		joint.yMotion = ConfigurableJointMotion.Locked;
		joint.zMotion = ConfigurableJointMotion.Locked;

		switch (rotationAxis) {
			case 0:
				joint.angularXMotion = ConfigurableJointMotion.Free;
				joint.angularYMotion = ConfigurableJointMotion.Free;
				joint.angularZMotion = ConfigurableJointMotion.Free;
				break;
			case 1:
				joint.axis = Vector3.right;
				joint.angularXMotion = ConfigurableJointMotion.Free;
				joint.angularYMotion = ConfigurableJointMotion.Locked;
				joint.angularZMotion = ConfigurableJointMotion.Locked;
				break;
			case 2:
				joint.axis = Vector3.up;
				joint.angularYMotion = ConfigurableJointMotion.Free;
				joint.angularXMotion = ConfigurableJointMotion.Locked;
				joint.angularZMotion = ConfigurableJointMotion.Locked;
				break;
			case 3:
				joint.axis = Vector3.right;
				joint.angularZMotion = ConfigurableJointMotion.Free;
				joint.angularXMotion = ConfigurableJointMotion.Locked;
				joint.angularYMotion = ConfigurableJointMotion.Locked;
				break;
			case 4:
				// joint.angularXMotion = ConfigurableJointMotion.Locked;
				// joint.angularYMotion = ConfigurableJointMotion.Locked;
				// joint.angularZMotion = ConfigurableJointMotion.Locked;
				break;
		}

	}

	//uses one force point 
	public void forceOnePointApply(Vector3 position, string hold, int rotationAxis = 0, int YaxisFix = 1) {
		if (!lockedOrientation || !(hold.Equals(curHold))) {
			lockOrientation(transform.InverseTransformPoint(position)*-1, rotationAxis);
		}
		
		float xForce = XCI.GetAxis(XboxAxis.LeftStickX);
		float yForce = XCI.GetAxis(XboxAxis.LeftStickY);
		Vector3 movement = new Vector3(xForce, yForce*YaxisFix, 0.0f);
		Vector3 worldForce = transform.TransformDirection(movement);
		body.AddForceAtPosition(worldForce*force, position);
	}

	//using midpoint of two force points with 0 or 1 direction to signify vertical flipping vs horizontal flipping
	//axis fixes for directional problems and rotational lock for the joint
	public void forceTwoPointApply(Vector3 position1, Vector3 position2, bool vertical, string hold, int rotationAxis = 0, int XaxisFix = 1, int YaxisFix = 1) {
		Vector3 position = (position1+position2)/2;
		if (!lockedOrientation || !(hold.Equals(curHold))) {
			lockOrientation(transform.InverseTransformPoint(position)*-1, rotationAxis);
		}
		
		float xForce = XCI.GetAxis(XboxAxis.LeftStickX);
		float yForce = XCI.GetAxis(XboxAxis.LeftStickY);
		Vector3 movement = vertical ? new Vector3(xForce*XaxisFix, yForce*YaxisFix, 0.0f) :  new Vector3(yForce*XaxisFix, xForce*YaxisFix, 0.0f);
		Vector3 worldForce = transform.TransformDirection(movement);
		
		body.AddForceAtPosition(worldForce*force, position);
	}
}