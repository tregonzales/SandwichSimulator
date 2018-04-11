// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Pivot : MonoBehaviour {

//     private Jeremy_Move move;
// 	// Use this for initialization
// 	void Start () {
//         move = GetComponentInParent<Jeremy_Move> ();
// 	}
	
// 	// Update is called once per frame
// 	void Update () {
		
// 	}

 

//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("Surface"))
//         {
//             if (CompareTag("RT"))
//             {
//                 move.RT_Touching_Obj = true;
//             }

//             if (CompareTag("RB"))
//             {
//                 move.RB_Touching_Obj = true;
//             }

//             if (CompareTag("LT"))
//             {
//                 move.LT_Touching_Obj = true;
//             }

//             if (CompareTag("LB"))
//             {
//                 move.LB_Touching_Obj = true;
//             }

//         }
//     }

//     private void OnTriggerExit(Collider other)
//     {
//         if (other.CompareTag("Surface"))
//         {
//             if (CompareTag("RT"))
//             {
//                 move.RT_Touching_Obj = false;
//             }

//             if (CompareTag("RB"))
//             {
//                 move.RB_Touching_Obj = false;
//             }

//             if (CompareTag("LT"))
//             {
//                 move.LT_Touching_Obj = false;
//             }

//             if (CompareTag("LB"))
//             {
//                 move.LB_Touching_Obj = false;
//             }
//         }
//     }


// }
