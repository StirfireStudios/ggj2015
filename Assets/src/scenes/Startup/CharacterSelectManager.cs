using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.UI;

public class CharacterSelectManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		portrait = transform.FindChild ("Portrait").gameObject.GetComponent<Image> ();
		name = transform.FindChild ("Name").gameObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetDevice(InputDevice device) {
		this.device = device;
	}

	private InputDevice device;
	private Image portrait;
	private Text name;
}
