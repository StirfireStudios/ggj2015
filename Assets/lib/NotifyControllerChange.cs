using UnityEngine;
using System.Collections;
using InControl;

public class NotifyControllerChange : MonoBehaviour {

	// Update is called once per frame
	public void Update () {
		if (connectedDevices != InputManager.Devices.Count) {
			this.SendMessage("OnNumControllersChanged", InputManager.Devices.Count, SendMessageOptions.DontRequireReceiver);
		}
	}
	
	private int connectedDevices = 0;
}
