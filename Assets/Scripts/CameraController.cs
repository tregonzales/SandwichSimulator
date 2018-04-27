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

	Vector3 offset;
	Coroutine switchRoutine;
	GameInputManager gameInputManager;

	void Start () {
		gameInputManager = GameInputManager.instance;
	    angleX = -45;
        angleY = 0;
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

		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
			angleX += Input.GetAxis("Vertical") * Time.deltaTime * verticalSpeed * -1;
			angleY += Input.GetAxis("Horizontal") * Time.deltaTime * verticalSpeed * -1;
		}

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
	}

	public IEnumerator lerpSwitch(float duration) {
		float startTime = 0;
		float currentTime = 0;
		float endTime = duration;
		Vector3 startPos = transform.position;

		while (true) {
			currentTime += Time.deltaTime;
			float normalizedTime = currentTime / endTime;
			float easedTime = EasingFunction.EaseOutCubic (0, 1, normalizedTime);

			transform.position = Vector3.Lerp(startPos, offset, easedTime); //changed to Lerp instead of Slerp
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (target.position - transform.position, new Vector3 (0, 1, 0)), easedTime);

			yield return null;
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
		return target.position + targetToCamera;
	}
}
