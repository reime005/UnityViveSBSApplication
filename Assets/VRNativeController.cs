using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRNativeController : MonoBehaviour {

    // Use this for initialization
    void Start() {
       Debug.Log(UnityEngine.Input.GetJoystickNames()[0]);

    }
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(Input.GetAxis("Axis2D.SecondaryThumbstick")) > 0.0F)
                Debug.Log(" is moved");
            
        


    }
}
