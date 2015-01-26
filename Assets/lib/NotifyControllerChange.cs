using UnityEngine;
using System.Collections;
using InControl;

public class NotifyControllerChange : MonoBehaviour
{

    public void Awake()
    {
        handleEvent = new System.Action<InputDevice>(SendDeviceListChange);
    }

	public void Start ()
	{
        InputManager.OnDeviceAttached += handleEvent;
        InputManager.OnDeviceDetached += handleEvent;
		this.SendMessage("OnNumControllersChanged", InputManager.Devices.Count, SendMessageOptions.DontRequireReceiver);
	}

	public void SendDeviceListChange(InputDevice device)
	{
        if (this != null)
        {
            this.SendMessage("OnNumControllersChanged", InputManager.Devices.Count, SendMessageOptions.DontRequireReceiver);
        }
	}

    public void onDestroy()
    {
        InputManager.OnDeviceAttached -= handleEvent;
        InputManager.OnDeviceDetached -= handleEvent;
    }

    private System.Action<InputDevice> handleEvent;
}
