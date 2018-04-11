using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputManager : MonoBehaviour {

	public bool Windows;
	public bool controller;
	// Use this for initialization
	void Start () {
		Windows = false;
		controller = Input.GetJoystickNames().Length > 0;
	}

	public bool getButton(string button) {
		if (Windows) {
			return false;
		}
		else {
			return getButtonHelperMac(button);
		}
	}

	public float getStick(string stick) {
			if (Windows) {
				return 0;
			}
			else {
				return getStickHelperMac(stick);
			}	
		}
	public bool getButtonHelperMac(string button) {
		switch (button){
			case "RB":
				return Input.GetKey("joystick button 14");
				
			case "LB":
				return Input.GetKey("joystick button 13");
				
			case "RT":
				return Input.GetAxis("RTmac") > 0;
				
			case "LT":
				return Input.GetAxis("LTmac") > 0;
				
			case "DpadLeft":
				return Input.GetKeyDown("joystick button 7");

			case "DpadTop":
				return Input.GetKeyDown("joystick button 5");
			
			case "DpadRight":
				return Input.GetKeyDown("joystick button 8");
			
			case "DpadBottom":
				return Input.GetKeyDown("joystick button 6");
			
			case "back":
				return Input.GetKeyDown("joystick button 10");
			
			case "middle":
				return Input.GetKeyDown("joystick button 15");
			
			case "start":
				return Input.GetKeyDown("joystick button 9");
			
			case "X":
				return Input.GetKeyDown("joystick button 18");
			
			case "Y":
				return Input.GetKeyDown("joystick button 19");
			
			case "A":
				return Input.GetKeyDown("joystick button 16");
			
			case "B":
				return Input.GetKeyDown("joystick button 17");
			
			case "LeftStick":
				return Input.GetKeyDown("joystick button 11");

			case "RightStick":
				return Input.GetKeyDown("joystick button 12");
			
			default:
				return false;
		}
	}

	public float getStickHelperMac(string stick) {
		if (stick == "RightStickX") {
			Debug.Log("x " + Input.GetAxisRaw("RightStickMacX"));
			return Input.GetAxis("RightStickMacX");
		}
		else if (stick == "RightStickY") {
			Debug.Log("y " + Input.GetAxisRaw("RightStickMacY"));
			return Input.GetAxis("RightStickMacY");
		}
		else {
			return 0;
		}
	}
}
