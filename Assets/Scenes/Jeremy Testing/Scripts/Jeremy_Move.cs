using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input


public class Jeremy_Move : MonoBehaviour {

    public XboxController controller;

    public bool LB_Touching_Obj = false;
    public bool RB_Touching_Obj = false;
    public bool LT_Touching_Obj = false;
    public bool RT_Touching_Obj = false;

    private Rigidbody rb;

    private bool rotate = false;

    private bool LB_pressed = false;
    private bool RB_pressed = false;
    private bool LT_pressed = false;
    private bool RT_pressed = false;








    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        // (XCI.GetButtonDown(XboxButton.LeftBumper, controller) && LB_Touching_Obj) && (XCI.GetButtonDown(XboxButton.RightBumper, controller) && RB_Touching_Obj)

        updateButtonsDown();
        
        if (LB_pressed && RB_pressed && rotate == false && !LT_pressed && !RT_pressed && LB_Touching_Obj && RB_Touching_Obj)
        {
            // LB is child 2
            // RB is child 3

            rotate = true;
            Debug.Log("hi");


            StartCoroutine(rotationBumpers(XboxButton.RightBumper, XboxButton.RightBumper, Vector3.right, 0, 1));
        }

        else if (LB_pressed && LT_pressed && rotate == false && !RB_pressed && !RT_pressed)
        {
            // LB is child 2
            // RB is child 3

            rotate = true;
            Debug.Log("hi");

            StartCoroutine(rotationMix(XboxButton.LeftBumper, XboxAxis.LeftTrigger, Vector3.forward, 0, 3));

            //StartCoroutine(rotate_RB_LB());

        }

        updateButtonsUp();
    }


    IEnumerator rotate_RB_LB()
    {
        Vector3 rotate_around_point = (gameObject.transform.GetChild(2).position + gameObject.transform.GetChild(3).position) / 2;

        while (true)
        {
            if(XCI.GetButtonUp(XboxButton.LeftBumper, controller) || (XCI.GetButtonUp(XboxButton.RightBumper, controller)))
            {
                break;
            }

            Debug.Log(XCI.GetAxis(XboxAxis.LeftStickY, controller));

            transform.RotateAround(rotate_around_point, Vector3.right, transform.rotation.y + 1 * XCI.GetAxis(XboxAxis.LeftStickY, controller));
            yield return null;
        }
        rotate = false;
    }

    IEnumerator rotationBumpers(XboxButton pivot1, XboxButton pivot2, Vector3 rotationAxis, int childNum1, int childNum2)
    {

        Vector3 rotate_around_point = (gameObject.transform.GetChild(childNum1).position + gameObject.transform.GetChild(childNum2).position) / 2;

        while (true)
        {

            Debug.Log("---------------------------------------------");

            if (XCI.GetButtonUp(pivot1, controller) || (XCI.GetButtonUp(pivot2, controller)) || RT_pressed || LT_pressed || !RB_Touching_Obj || !LB_Touching_Obj)
            {
                break;
            }

            Vector3 first = transform.position - gameObject.transform.GetChild(childNum1).position;
            Vector3 second = transform.position - gameObject.transform.GetChild(childNum2).position;

            Debug.Log(Vector3.Cross(first, second).x * XCI.GetAxis(XboxAxis.LeftStickY, controller) * 8);
            Debug.Log(Vector3.Cross(first, second).y * XCI.GetAxis(XboxAxis.LeftStickY, controller) * 8);
            Debug.Log(Vector3.Cross(first, second).z * XCI.GetAxis(XboxAxis.LeftStickY, controller) * 8);


            // Vector3.Cross(first, second) * XCI.GetAxis(XboxAxis.LeftStickY, controller)
            rb.AddForceAtPosition(transform.up * 8 * XCI.GetAxis(XboxAxis.LeftStickY, controller), rotate_around_point);

            yield return null;
        }
        rotate = false;
    }

    IEnumerator rotationMix(XboxButton pivot1, XboxAxis pivot2, Vector3 rotationAxis, int childNum1, int childNum2)
    {
        Debug.Log("Hello2");
        Vector3 rotate_around_point = (gameObject.transform.GetChild(childNum1).position + gameObject.transform.GetChild(childNum2).position) / 2;

        Debug.Log(CountTrue(new bool[] { RT_pressed, LT_pressed, RB_pressed, LB_pressed }));

        while (true)
        {
            if (XCI.GetButtonUp(pivot1, controller) || (XCI.GetAxis(pivot2, controller)) == 0 || CountTrue(new bool[] { RT_pressed, LT_pressed, RB_pressed, LB_pressed }) != 2
                || !LB_Touching_Obj || !LT_Touching_Obj)
            {
                break;
            }

            //Debug.Log(XCI.GetAxis(XboxAxis.LeftStickY, controller));

            //transform.RotateAround(rotate_around_point, rotationAxis, transform.rotation.x + 1 * XCI.GetAxis(XboxAxis.LeftStickY, controller));



            Vector3 first = transform.position - gameObject.transform.GetChild(childNum1).position;
            Vector3 second = transform.position - gameObject.transform.GetChild(childNum2).position;

            Debug.Log(Vector3.Cross(first, second).z);


            rb.AddForceAtPosition(Vector3.Cross(first, second) * XCI.GetAxis(XboxAxis.LeftStickY, controller) * 8, rotate_around_point);

            yield return null;
        }
        rotate = false;
    }

    private void updateButtonsDown()
    {
        if (XCI.GetButtonDown(XboxButton.LeftBumper, controller))
        {
            LB_pressed = true;
        }

        if (XCI.GetButtonDown(XboxButton.RightBumper, controller))
        {
            RB_pressed = true;
        }

        if (XCI.GetAxis(XboxAxis.RightTrigger, controller) != 0)
        {
            RT_pressed = true;
        }

        if (XCI.GetAxis(XboxAxis.LeftTrigger, controller) != 0)
        {
            LT_pressed = true;
        }

        // Debug.Log(LB_pressed);
        // Debug.Log(RB_pressed);
        // Debug.Log(LT_pressed);
        // Debug.Log(RT_pressed);

        // Debug.Log(XCI.GetAxis(XboxAxis.LeftTrigger, controller));

    }

    private void updateButtonsUp()
    {
        if (XCI.GetButtonUp(XboxButton.LeftBumper, controller))
        {
            LB_pressed = false;
        }

        if (XCI.GetButtonUp(XboxButton.RightBumper, controller))
        {
            RB_pressed = false;
        }

        if (XCI.GetAxis(XboxAxis.RightTrigger, controller) == 0)
        {
            RT_pressed = false;
        }

        if (XCI.GetAxis(XboxAxis.LeftTrigger, controller) == 0)
        {
            LT_pressed = false;
        }
    }

    public static int CountTrue( bool[] args)
    {
        int count = 0;
        for(int i = 0; i < args.Length; i++)
        {
            if (args[i])
            {
                count += 1;
            }
        }
        return count;

    }



}
