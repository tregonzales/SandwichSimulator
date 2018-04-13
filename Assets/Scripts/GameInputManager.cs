using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameInputManager : MonoBehaviour {

	public bool Windows;
	public bool WebGL;
	public bool Mac;
	// Use this for initialization
	void Start () {
		//mac driver here:
		//https://github.com/360Controller/360Controller
		Windows = false;
		Mac = false;
		WebGL = false;
		if (RuntimePlatform.OSXEditor == Application.platform) {
			Mac = true;
		}
		else if (RuntimePlatform.WindowsEditor == Application.platform) {
			Windows = true;
		}
		else {
			WebGL = true;
		}
	}

	public bool getButton(string button) {
		if (Windows) {
			return getButtonHelperWindows(button);
		}
		else if (Mac) {
			return getButtonHelperMac(button);
		}
		else {
			return getButtonHelperWeb(button);
		}
	}

	public float getStick(string stick) {
			if (Windows) {
				return getStickHelperWindows(stick);
			}
			else if (Mac) {
				return getStickHelperMac(stick);
			}	
			else {
				return getStickHelperWeb(stick);
			}
		}

	public bool getButtonHelperWeb(string button) {
		switch (button){
			case "RB":
				return Input.GetKey("joystick button 5");
				
			case "LB":
				return Input.GetKey("joystick button 4");
				
			case "RT":
				return Input.GetAxis("RTweb") > 0;
				
			case "LT":
				return Input.GetAxis("LTweb") > 0;
				
			case "DpadLeft":
				return Input.GetKeyDown("joystick button 14");

			case "DpadTop":
				return Input.GetKeyDown("joystick button 12");
			
			case "DpadRight":
				return Input.GetKeyDown("joystick button 15");
			
			case "DpadBottom":
				return Input.GetKeyDown("joystick button 13");
			
			case "back":
				return Input.GetKeyDown("joystick button 6");
			
			case "middle":
				return Input.GetKeyDown("joystick button 16");
			
			case "start":
				return Input.GetKeyDown("joystick button 7");
			
			case "X":
				return Input.GetKeyDown("joystick button 2");
			
			case "Y":
				return Input.GetKeyDown("joystick button 3");
			
			case "A":
				return Input.GetKeyDown("joystick button 0");
			
			case "B":
				return Input.GetKeyDown("joystick button 1");
			
			case "LeftStick":
				return Input.GetKeyDown("joystick button 8");

			case "RightStick":
				return Input.GetKeyDown("joystick button 9");
			
			default:
				return false;
		}
	}
	//web button test:
	/*
	a: 0
	b: 1
	x: 2
	y: 3
	rb: 5
	lb: 4
	rt: no button, axis 9
	lt: no button, axis 9
	left click: 8
	right click: 9
	back: 6
	middle: none
	start: 7
	top: 12
	left: 14
	bottom: 13
	right: 15

	 */

	public float getStickHelperWeb(string stick) {
		if (stick == "RightStickX") {
			return Input.GetAxis("RightStickWebX");
		}
		else if (stick == "RightStickY") {
			return Input.GetAxis("RightStickWebY");
		}
		else {
			return 0;
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
			return Input.GetAxis("RightStickMacX");
		}
		else if (stick == "RightStickY") {
			return Input.GetAxis("RightStickMacY");
		}
		else {
			return 0;
		}
	}

	public bool getButtonHelperWindows(string button) {
		switch (button){
			case "RB":
				return Input.GetKey("joystick button 5");
				
			case "LB":
				return Input.GetKey("joystick button 4");
				
			case "RT":
				return Input.GetAxis("RTwindows") > 0;
				
			case "LT":
				return Input.GetAxis("LTwindows") > 0;
				
			case "DpadLeft":
				return Input.GetAxis("DpadWindowsX") < 0;

			case "DpadTop":
				return Input.GetAxis("DpadWindowsY") > 0;
			
			case "DpadRight":
				return Input.GetAxis("DpadWindowsX") > 0;
			
			case "DpadBottom":
				return Input.GetAxis("DpadWindowsY") < 0;
			
			case "back":
				return Input.GetKeyDown("joystick button 6");
			
			case "middle":
				//no mapping
				return false;
			
			case "start":
				return Input.GetKeyDown("joystick button 7");
			
			case "X":
				return Input.GetKeyDown("joystick button 2");
			
			case "Y":
				return Input.GetKeyDown("joystick button 3");
			
			case "A":
				return Input.GetKeyDown("joystick button 0");
			
			case "B":
				return Input.GetKeyDown("joystick button 1");
			
			case "LeftStick":
				return Input.GetKeyDown("joystick button 8");

			case "RightStick":
				return Input.GetKeyDown("joystick button 9");
			
			default:
				return false;
		}
	}

	public float getStickHelperWindows(string stick) {
		if (stick == "RightStickX") {
			return Input.GetAxis("RightStickWindowsX");
		}
		else if (stick == "RightStickY") {
			return Input.GetAxis("RightStickWindowsY");
		}
		else {
			return 0;
		}
	}
}
