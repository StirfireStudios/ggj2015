using UnityEngine;
using System.Collections;
using InControl;

public class InputWrangler : MonoBehaviour {

    int NumDevices = 0;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    NumDevices = InputManager.Devices.Count;

        Debug.Log(string.Format("{0} devices.", NumDevices));
	}
}
