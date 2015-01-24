using UnityEngine;
using System.Collections;
using InControl;

public class InputWrangler : MonoBehaviour {

    int NumDevices = 0;

    GGJ.GameConfig gc = GGJ.GameConfig.Instance;

	// Use this for initialization
	void Start () {
        gc.DebugMeh();
	}
	
	// Update is called once per frame
	void Update () {
	    NumDevices = InputManager.Devices.Count;

//Debug.Log(string.Format("{0} devices.", NumDevices));
	}
}
