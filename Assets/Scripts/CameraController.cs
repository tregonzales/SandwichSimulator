// <copyright file="CameraController.cs" company="DIS Copenhagen">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Benno Lueders</author>
// <date>05/10/2017</date>

using UnityEngine;
using System.Collections;


public class CameraController : MonoBehaviour {

	//determines how the camera should move based on the current state of the game (switching items or following one item)
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

	Vector3 offset;
	Coroutine switchRoutine;
	GameInputManager gameInputManager;

	void Start () {
		gameInputManager = GameInputManager.instance;
	    angleX = -45;
        angleY = 180;
	}

	//when switching from one item to another
	public void SwitchTarget(Transform newTarget){
		target = newTarget; //change targets to the new item
		state = CamState.Switch; //change state of camera to switching
		if (switchRoutine != null) { //if the camera was in the middle of switching items, disrupt it
			StopCoroutine (switchRoutine);
		}
		switchRoutine = StartCoroutine(lerpSwitch(1f)); //begin coroutine to switch to the new item, and store it to switchRoutine variable to keep track of whether the camera is currently running a coroutine or not
	}

	void Update () {

		//using input from the player on xbox controller, determine where to move the camera
		angleX += gameInputManager.getStick ("RightStickY") * Time.deltaTime * verticalSpeed * -1;
		angleY += gameInputManager.getStick ("RightStickX") * Time.deltaTime * verticalSpeed * -1;

		//using input from keyboard, determine where to move the camera
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
			angleX += Input.GetAxis("Vertical") * Time.deltaTime * verticalSpeed * -1;
			angleY += Input.GetAxis("Horizontal") * Time.deltaTime * verticalSpeed * -1;
		}
			
		angleX = Mathf.Clamp (angleX, minVerticalAngle, maxVerticalAngle);
		angleY %= 360;

		//calculate rotation of camera
		Quaternion xRotation = Quaternion.AngleAxis (angleX, new Vector3 (1, 0, 0));
		Quaternion yRotation = Quaternion.AngleAxis (angleY, new Vector3 (0, 1, 0));
		offset = new Vector3 (0, 0, 1);
		offset = xRotation * offset;
		offset = yRotation * offset;
		offset *= distance;

		//offset here becomes the result of offset plus the target position and target to camera
		//otherwise becomes the hit point if it has to avoid an obstacle
		offset = AddObstacleAvoidance (offset);
			
		switch (state) {
		case CamState.Follow:
			//if camera state is following, then set the new position and new rotation
			transform.position = offset;
			transform.rotation = Quaternion.LookRotation (target.position - transform.position, new Vector3 (0, 1, 0));
			break;
		case CamState.Switch:
			//if camera state is switch, do nothing here because the SwitchTarget method will be called instead
			break;
		}
	}
		
	//use lerp to move from one item to another for a smooth linear transition
	public IEnumerator lerpSwitch(float duration) {
		float currentTime = 0;
		float endTime = duration;
		Vector3 startPos = transform.position;

		while (true) {
			currentTime += Time.deltaTime;
			float normalizedTime = currentTime / endTime;

			//ease the amount of time with an easing function with normalized time
			float easedTime = EasingFunction.EaseOutCubic (0, 1, normalizedTime);

			//lerp to the new position and new rotation from the old position/rotation
			transform.position = Vector3.Lerp(startPos, offset, easedTime);
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (target.position - transform.position, new Vector3 (0, 1, 0)), easedTime);

			yield return null;

			//once the set duration time is up, end loop
			if (currentTime > endTime) {
				break;
			}
		}
		state = CamState.Follow;
	}


	Vector3 AddObstacleAvoidance(Vector3 targetToCamera){
		RaycastHit hit;
		if (Physics.Raycast (target.position, targetToCamera, out hit, distance, obstacleLayerMask)) {
			// if we hit an object between camera and target position the camera at (in front of) the hit object.
			return hit.point;
		}
		//if we don't hit an object, then the new position will be returned based off target position and target to camera
		return target.position + targetToCamera;
	}
}
