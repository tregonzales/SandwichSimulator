using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMultiJoint : MonoBehaviour {

	GameInputManager gameInputManager;
	//the corner points 
	private GameObject RB;
	private GameObject LB;
	private GameObject RT;
	private GameObject LT;

	//body of the item
	private Rigidbody body;

	//button ui that represents corners
	buttonController buttons;
	public Transform buttonsTrans;

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

	//bools to check if a corner is grabbing
	public bool RBgrabbing;
	public bool LBgrabbing;
	public bool RTgrabbing;
	public bool LTgrabbing;

	//angular velocity to apply to body
	public float MaxAngularVelocity;

	//vector 3 fling force to apply to bread once user lets go so they don't just spin in place
	Vector3 flingForce;

	// Use this for initialization
	void Start () {
		RB = gameObject.transform.GetChild(1).gameObject;
		LB = gameObject.transform.GetChild(2).gameObject;
		LT = gameObject.transform.GetChild(3).gameObject;
		RT = gameObject.transform.GetChild(4).gameObject;

		body = gameObject.transform.GetComponent<Rigidbody>();
		body.maxAngularVelocity = MaxAngularVelocity;

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

		buttons = buttonsTrans.GetComponent<buttonController>();
		gameInputManager = GameInputManager.instance;
	}
	
	// Update is called once per frame
	void Update () {

		//consistently update the ui for the buttons to tell user what is possible
		buttons.updatePositions(RB.transform.position, LB.transform.position, LT.transform.position, RT.transform.position);
		buttons.colorCanGrab(RBobj.grabbableBody, LBobj.grabbableBody, LTobj.grabbableBody, RTobj.grabbableBody);
		buttons.colorGrabbing(RBgrabbing, LBgrabbing, LTgrabbing, RTgrabbing);

		//send a button message depending if the user is trying to grab or release a corner or not
		if (gameInputManager.getButton("RB") || Input.GetKey(KeyCode.W)) {
			updateJointAndVelocity(true, ref RBgrabbing, ref RBobj, ref RBjoint);
		}
		else {
			updateJointAndVelocity(false, ref RBgrabbing, ref RBobj, ref RBjoint);
		}

		if (gameInputManager.getButton("LB") || Input.GetKey(KeyCode.Q)) {
			updateJointAndVelocity(true, ref LBgrabbing, ref LBobj, ref LBjoint);
		}
		else {
			updateJointAndVelocity(false, ref LBgrabbing, ref LBobj, ref LBjoint);
		}

		
		if (gameInputManager.getButton("LT") || Input.GetKey(KeyCode.A)) {
			updateJointAndVelocity(true, ref LTgrabbing, ref LTobj, ref LTjoint, -1);
		}
		else {
			updateJointAndVelocity(false, ref LTgrabbing, ref LTobj, ref LTjoint, -1);
		}
		
		if (gameInputManager.getButton("RT") || Input.GetKey(KeyCode.S)) {
			updateJointAndVelocity(true, ref RTgrabbing, ref RTobj, ref RTjoint, -1);
		}
		else {
			updateJointAndVelocity(false, ref RTgrabbing, ref RTobj, ref RTjoint, -1);
		}
		
	}

	//update the corner's joint and velocity
	private void updateJointAndVelocity(bool pressed, ref bool grab, ref playerChildren obj, ref ConfigurableJoint joint, int YaxisFix = 1) {

		bool sideGrabL = false;
		bool sideGrabR = false;
		//unlocking the grab if the user lets go of the button
		if (!pressed && grab) {
				grab = false;
				joint.connectedBody = null;
				joint.xMotion = ConfigurableJointMotion.Free;
				joint.yMotion = ConfigurableJointMotion.Free;
				joint.zMotion = ConfigurableJointMotion.Free;
				
		}
		//creating a new attached body when the user initially grabs a corner to a surface or item
		else if (!grab && obj.canGrab && pressed) {
				grab = true;
				joint.connectedBody = obj.grabbableBody;
				joint.xMotion = ConfigurableJointMotion.Locked;
				joint.yMotion = ConfigurableJointMotion.Locked;
				joint.zMotion = ConfigurableJointMotion.Locked;
		}
		//checking if the user is grabbing with the left or right side of the item
		else if (grab) {
			//first check top, bottom, and side grabs
			if (LTgrabbing && LBgrabbing) {
				//angular
				sideGrabL = (checkGrab(LTjoint) && checkGrab(LBjoint)) || LTjoint.connectedBody == LBjoint.connectedBody;
			}
			if (RTgrabbing && RBgrabbing) {
				//angular
				sideGrabR = (checkGrab(RTjoint) && checkGrab(RBjoint)) || RTjoint.connectedBody == RBjoint.connectedBody;
			}
	
			//apply the correct oriented angular velocity
			applyMovement(sideGrabL, sideGrabR, YaxisFix);
		}
	}

	public bool checkGrab(ConfigurableJoint curJoint) {
		if (curJoint.connectedBody.gameObject.CompareTag("item")){
			return false;
		}
		else {
			return true;
		}
	}

	public void applyMovement(bool sideGrabL, bool sideGrabR, int YaxisFix = 1) {
		
		float xForce = 0;
		float yForce = 0;
		xForce = Input.GetAxis("Horizontal");
		yForce = Input.GetAxis("Vertical");
		
		//for angular y axis fix always -1 for side grab
		Vector3 movement;

		if (sideGrabL) {
			//angular style
			YaxisFix = -1;
			movement = new Vector3(yForce, 0.0f, xForce*YaxisFix);
		}
		else if (sideGrabR) {
			//angular style
			YaxisFix = -1;
			movement = new Vector3(yForce, 0.0f, xForce*YaxisFix);
		}
		else {
			//angular style, yaxis fix always 1 bc depends on rotating around simple axis, not direction of force
			YaxisFix = 1;
			movement = new Vector3(yForce, xForce*YaxisFix, 0.0f);
		}

		//just a direction in world space for velocity
		Vector3 worldVelocity = transform.TransformDirection(movement).normalized;	
		
		body.angularVelocity += (worldVelocity*5);
	}
}