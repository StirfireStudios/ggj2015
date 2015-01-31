using UnityEngine;
using System.Collections;
using InControl;

public class UpdateCharacterSelect : MonoBehaviour
{
	public GameObject CharacterSelectPrefab;

    public void Awake()
    {
        DeviceAttachHandler = new System.Action<InputDevice>(ShowPanel);
        DeviceDetachHandler = new System.Action<InputDevice>(HidePanel);
    }

	public void Start ()
	{
        InputManager.OnDeviceAttached += DeviceAttachHandler;
        InputManager.OnDeviceDetached += DeviceDetachHandler;
		foreach (InputDevice device in InputManager.Devices)
		{
            if (device.Name == "Keyboard/Mouse")
            {
                keyboard = device;
            }
            else
            {
                ShowPanel(device);
            } 
		}
	}

    public void Update()
    {
        if (keyboardOn && keyboard != null)
        {
            if (keyboard.Action2)
            {
                HidePanel(keyboard);                
                keyboardOn = false;
                SendMessageUpwards("OnKeyboardDisabled");
            }
        }
        else
        {
            if (keyboard.Action1)
            {
                ShowPanel(keyboard);
                keyboardOn = true;
                SendMessageUpwards("OnKeyboardEnabled");
            }
        }
    }

    public void OnDestroy()
    {
        InputManager.OnDeviceAttached -= DeviceAttachHandler;
        InputManager.OnDeviceDetached -= DeviceDetachHandler;
    }

	public void ShowPanel(InputDevice device)
	{
		GameObject panel = GetPanelFor (device, true);
		panel.SetActive(true);
	}
	
	public void HidePanel(InputDevice device)
	{
		GameObject panel = GetPanelFor (device);
		if (panel != null)
		{
			panel.SetActive(false);
		}
	}

	private GameObject GetPanelFor(InputDevice device)
	{
		return GetPanelFor (device, false);
	}

	private GameObject GetPanelFor(InputDevice device, bool create)
	{
		if (characterSelectors.ContainsKey (device))
		{
			return characterSelectors[device];
		} else
		{
			if (create)
			{
				GameObject newPanel = (GameObject)GameObject.Instantiate (CharacterSelectPrefab);
				newPanel.transform.SetParent(this.transform, false);
				newPanel.SendMessage("SetDevice", device);
				newPanel.name = device.Name + " Selector";
				characterSelectors[device] = newPanel;
				return newPanel;
			} else 
			{
				return null;
			}
		}
	}

	private System.Collections.Generic.Dictionary<InputDevice, GameObject> characterSelectors = new System.Collections.Generic.Dictionary<InputDevice, GameObject>();
    private bool keyboardOn = false;
    private InputDevice keyboard;
    private System.Action<InputDevice> DeviceAttachHandler, DeviceDetachHandler;
}
