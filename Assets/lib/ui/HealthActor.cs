using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthActor : MonoBehaviour {
	// Name to display for this character
	public string characterName;

	// Health for character
	public float health;

	public void Awake() {
		nameObject = transform.Find ("CharacterName").gameObject.GetComponent<Text> ();
		if (nameObject == null)
			Debug.Log ("OH NO. COULDNT FIND NAME");
		
		healthBarObject = transform.Find ("Bar").gameObject.GetComponent<Image> ();
		if (healthBarObject == null) {
			Debug.Log ("OH NO. COULDNT FIND HEALTH");
		} else {
			healthBarInitial = healthBarObject.rectTransform.sizeDelta;
		}
		
		SetName(characterName);
		SetHealth(health);
	}

	// Use this for initialization
	public void Start () {
	}
	
	// Update is called once per frame
	public void Update () {
	
	}


	public void SetName(string value) {
		nameObject.text = value;
	}

	private void SetHealth(float value) {
		if (value < 0.0f) {
			Debug.Log ("Warning: Health value " + value + " Below 0.0f");
			value = 0.0f;
		} else if (value > 1.0f) {
			Debug.Log ("Warning: Health value " + value + " Above 1.0f");
			value = 1.0f;
		}
	
		healthBarObject.rectTransform.sizeDelta = new Vector2 (healthBarInitial.x * value, healthBarInitial.y);
	}

	private Text nameObject;
	private Image healthBarObject;
	private Vector2 healthBarInitial;
}
