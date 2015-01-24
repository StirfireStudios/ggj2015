using UnityEngine;
using System.Collections;
using InControl;

public class NotifyControllerChange : MonoBehaviour {

	public void Start () {
		InputManager.OnDeviceAttached += new System.Action<InputDevice>(SendDeviceListChange);
		InputManager.OnDeviceDetached += new System.Action<InputDevice>(SendDeviceListChange);
		this.SendMessage("OnNumControllersChanged", InputManager.Devices.Count, SendMessageOptions.DontRequireReceiver);
	}

	public void SendDeviceListChange(InputDevice device) {
		this.SendMessage("OnNumControllersChanged", InputManager.Devices.Count, SendMessageOptions.DontRequireReceiver);
	}

/*	public void SendDeviceListChange1(InputDevice device) {
		this.SendMessage("OnNumControllersChanged", InputManager.Devices.Count, SendMessageOptions.DontRequireReceiver);
	}*/
}
