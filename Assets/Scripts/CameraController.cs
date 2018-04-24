// <copyright file="CameraController.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>05/10/2017</date>

using UnityEngine;
using System.Collections;


public class CameraController : MonoBehaviour {

	public enum CamState
	{
		Follow, Switch
	}

	private CamState state = CamState.Follow;

    public Transform target;

	public LayerMask obstacleLayerMask;

	public float distance;
    public float minVerticalAngle;
	public float maxVerticalAngle;

    public float verticalSpeed = 150;
    public float horizontalSpeed = 300;

    private float angleX;
    private float angleY;
	GameInputManager gameInputManager;

	Vector3 offset;
	Coroutine switchRoutine;

	void Start () {
	    angleX = -45;
        angleY = 0;
		gameInputManager = GameObject.Find("GameInputManager").GetComponent<GameInputManager>();
	}

	public void SwitchTarget(Transform newTarget){
		target = newTarget;
		state = CamState.Switch;
		if (switchRoutine != null) {
			StopCoroutine (switchRoutine);
		}
		switchRoutine = StartCoroutine(lerpSwitch(1f));
	}

	void Update () {
		angleX += gameInputManager.getStick ("RightStickY") * Time.deltaTime * verticalSpeed * -1;
		angleY += gameInputManager.getStick ("RightStickX") * Time.deltaTime * verticalSpeed * -1;

		angleX = Mathf.Clamp (angleX, minVerticalAngle, maxVerticalAngle);
		angleY %= 360;

		Quaternion xRotation = Quaternion.AngleAxis (angleX, new Vector3 (1, 0, 0));
		Quaternion yRotation = Quaternion.AngleAxis (angleY, new Vector3 (0, 1, 0));
		offset = new Vector3 (0, 0, 1);
		offset = xRotation * offset;
		offset = yRotation * offset;
		offset *= distance;

		offset = AddObstacleAvoidance (offset);
			
		switch (state) {
		case CamState.Follow:
			transform.position = offset;
			transform.rotation = Quaternion.LookRotation (target.position - transform.position, new Vector3 (0, 1, 0));
			break;
		case CamState.Switch:
			break;
		}
			

////			transform.position = offset;
////					transform.position = Vector3.MoveTowards(transform.position, target.position + offset,Time.deltaTime);
//
//			//smooths the movement, has some momentum after
//					transform.position = Vector3.Lerp(transform.position, offset, Time.deltaTime); //changed to Lerp instead of Slerp
//
////			transform.rotation = Quaternion.LookRotation (target.position - transform.position, new Vector3 (0, 1, 0));
//			//		transform.rotation = Quaternion.Slerp(transform.rotation, target.position - transform.position, Time.deltaTime);
//
//
//		//changed to Lerp instead of Slerp
//					transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (target.position - transform.position, new Vector3 (0, 1, 0)), Time.deltaTime);
	}

	public IEnumerator lerpSwitch(float duration) {
		float startTime = 0;
		float currentTime = 0;
		float endTime = duration;
		Vector3 startPos = transform.position;
//		float tt = 0f;
		while (true) {
			currentTime += Time.deltaTime;
			float normalizedTime = currentTime / endTime;
			float easedTime = EasingFunction.EaseOutCubic (0, 1, normalizedTime);
			transform.position = Vector3.Lerp(startPos, offset, easedTime); //changed to Lerp instead of Slerp
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (target.position - transform.position, new Vector3 (0, 1, 0)), easedTime);
//			tt += Time.deltaTime;
			yield return null;
			if (currentTime > endTime) {
				break;
			}
//			Debug.Log ("transform " + (transform.position - target.position).magnitude + 0.04f);
//
//			Debug.Log("offset " + (offset - target.position).magnitude);

//			if ((((transform.position - target.position).magnitude) <= (offset - target.position).magnitude + 0.04) && (Quaternion.Dot(transform.rotation, Quaternion.LookRotation (target.position - transform.position, new Vector3 (0, 1, 0))) > 0.99f)) {
////			if ((Mathf.Approximately((transform.position - target.position).magnitude, (offset - target.position).magnitude))) {
//				break;
//			}


//			if(Quaternion.Dot(transform.rotation, Quaternion.LookRotation (target.position - transform.position, new Vector3 (0, 1, 0))) > 0.9999f) {
//				// Now we're within 1 degree of the target rotation.
//				// Put your "arrived" code here.
//				Debug.Log ("broken");
//				break;
//			}
		}
		state = CamState.Follow;
//		Debug.Log ("no longer switching " + isSwitching);
	}

	public void updateToSwitchObjects() {
		//i don't think this works but i thought maybe if we do the smooth chanigng thing only when switching objects it would work better
		//but instead it just doesn't seem to be calling either this or the normal update method
		//so as of rn we don't use this method
		//keeping in case we decide to somewhere

//		Debug.Log ("updating to swtich objects");
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
