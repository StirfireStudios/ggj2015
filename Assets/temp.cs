using UnityEngine;
using System.Collections;

public class temp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		healthbar = GameObject.Find ("HealthBar");
		healthbar.SendMessage ("SetName", "Joe Bloggs", SendMessageOptions.RequireReceiver);
		healthbar.SendMessage ("SetHealth", health, SendMessageOptions.RequireReceiver);
		healthbar2 = GameObject.Find ("Health Bar");
		healthbar2.SendMessage ("SetName", "Globs", SendMessageOptions.RequireReceiver);
		healthbar2.SendMessage ("SetHealth", health2, SendMessageOptions.RequireReceiver);

	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.time - lastTime) > 0.2f) {
			health += 0.01f;
			if (health > 1.0f)
				health = 0.1f;

			healthbar.SendMessage ("SetHealth", health, SendMessageOptions.RequireReceiver);
			lastTime = Time.time;
		}
		if ((Time.time - lastTime2) > 0.4f) {
			health2 -= 0.01f;
			if (health2 < 0.1f)
				health2 = 1.0f;
			
			healthbar2.SendMessage ("SetHealth", health2, SendMessageOptions.RequireReceiver);
			lastTime2 = Time.time;
		}
	}

	private GameObject healthbar;
	private GameObject healthbar2;
	private float lastTime;
	private float lastTime2;
	private float health = 0.1f;
	private float health2 = 1.0f;
}
