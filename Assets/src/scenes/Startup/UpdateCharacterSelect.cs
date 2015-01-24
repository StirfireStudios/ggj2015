using UnityEngine;
using System.Collections;
using InControl;

public class UpdateCharacterSelect : MonoBehaviour
{
	public GameObject CharacterSelectPrefab;
	
	public void Start ()
	{
		InputManager.OnDeviceAttached += new System.Action<InputDevice>(ShowPanel);
		InputManager.OnDeviceDetached += new System.Action<InputDevice>(HidePanel);
		foreach (InputDevice device in InputManager.Devices)
		{
			ShowPanel (device);
		}


	}
	
	public void ShowPanel(InputDevice device)
	{
		GameObject panel = GetPanelFor (device, true);
		if (characterSelectors.ContainsKey (device))
		{
			panel = (GameObject)GameObject.Instantiate(CharacterSelectPrefab);
		} else
		{
			panel = characterSelectors[device];
		}
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
}
