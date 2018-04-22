// <copyright file="CameraController.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>05/10/2017</date>

using UnityEngine;
using System.Collections;


public class CameraController : MonoBehaviour {


    public Transform target;

	public bool isSwitching = false;
	public LayerMask obstacleLayerMask;

	public float distance;
    public float minVerticalAngle;
	public float maxVerticalAngle;

    public float verticalSpeed = 150;
    public float horizontalSpeed = 300;

    private float angleX;
    private float angleY;
	GameInputManager gameInputManager;

	void Start () {
	    angleX = -45;
        angleY = 0;
		gameInputManager = GameObject.Find("GameInputManager").GetComponent<GameInputManager>();
	}
	
	void Update () {
//		if (!isSwitching) {
			Debug.Log ("normal movement");
			angleX += gameInputManager.getStick ("RightStickY") * Time.deltaTime * verticalSpeed * -1;
			angleY += gameInputManager.getStick ("RightStickX") * Time.deltaTime * verticalSpeed * -1;

			angleX = Mathf.Clamp (angleX, minVerticalAngle, maxVerticalAngle);
			angleY %= 360;

			Quaternion xRotation = Quaternion.AngleAxis (angleX, new Vector3 (1, 0, 0));
			Quaternion yRotation = Quaternion.AngleAxis (angleY, new Vector3 (0, 1, 0));
			Vector3 offset = new Vector3 (0, 0, 1);
			offset = xRotation * offset;
			offset = yRotation * offset;
			offset *= distance;

			offset = AddObstacleAvoidance (offset);

//			transform.position = offset;
//					transform.position = Vector3.MoveTowards(transform.position, target.position + offset,Time.deltaTime);

			//smooths the movement, has some momentum after
					transform.position = Vector3.Slerp(transform.position, offset, Time.deltaTime); //changed to Lerp instead of Slerp

//			transform.rotation = Quaternion.LookRotation (target.position - transform.position, new Vector3 (0, 1, 0));
			//		transform.rotation = Quaternion.Slerp(transform.rotation, target.position - transform.position, Time.deltaTime);


		//changed to Lerp instead of Slerp
					transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (target.position - transform.position, new Vector3 (0, 1, 0)), Time.deltaTime);
//		} else {
//			Debug.Log ("else");
//		}
	}

	public void updateToSwitchObjects() {
		//i don't think this works but i thought maybe if we do the smooth chanigng thing only when switching objects it would work better
		//but instead it just doesn't seem to be calling either this or the normal update method
		//so as of rn we don't use this method
		//keeping in case we decide to somewhere

		Debug.Log ("updating to swtich objects");
		angleX += gameInputManager.getStick("RightStickY") * Time.deltaTime * verticalSpeed * -1;
		angleY += gameInputManager.getStick("RightStickX") * Time.deltaTime * verticalSpeed * -1;

		angleX = Mathf.Clamp(angleX, minVerticalAngle, maxVerticalAngle);
		angleY %= 360;

		Quaternion xRotation = Quaternion.AngleAxis(angleX, new Vector3(1,0,0));
		Quaternion yRotation = Quaternion.AngleAxis(angleY, new Vector3(0,1,0));
		Vector3 offset = new Vector3(0,0,1);
		offset = xRotation * offset;
		offset = yRotation * offset;
		offset *= distance;

		offset = AddObstacleAvoidance (offset);

		//        transform.position = offset;
		//		transform.position = Vector3.MoveTowards(transform.position, target.position + offset,Time.deltaTime);

		//smooths the movement, has some momentum after
		transform.position = Vector3.Slerp(transform.position, offset, Time.deltaTime);

		//        transform.rotation = Quaternion.LookRotation(target.position - transform.position, new Vector3(0,1,0));
		//		transform.rotation = Quaternion.Slerp(transform.rotation, target.position - transform.position, Time.deltaTime);


		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (target.position - transform.position, new Vector3 (0, 1, 0)), Time.deltaTime);

	}

	Vector3 AddObstacleAvoidance(Vector3 targetToCamera){
		RaycastHit hit;
		if (Physics.Raycast (target.position, targetToCamera, out hit, distance, obstacleLayerMask)) {
			// if we hit an object between camera and target position the camera at (in front of) the hit object.
			return hit.point;
		}
		return target.position + targetToCamera;
	}
}
