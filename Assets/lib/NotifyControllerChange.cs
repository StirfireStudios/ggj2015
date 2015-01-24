using UnityEngine;
using System.Collections;
using InControl;

public class NotifyControllerChange : MonoBehaviour {

	// Update is called once per frame
	public void Update () {
		if (connectedDevices != InputManager.Devices.Count) {
			connectedDevices = InputManager.Devices.Count;
			this.SendMessage("OnNumControllersChanged", connectedDevices, SendMessageOptions.DontRequireReceiver);
		}
	}
	
	private int connectedDevices = 0;
}
