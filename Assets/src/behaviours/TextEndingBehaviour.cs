using UnityEngine;
using System.Collections;

public class TextEndingBehaviour : MonoBehaviour {

    // time to wait to reload startup screen.
    public float restartDelay = 5.0f;

	// Use this for initialization
	public void Start () {
        text = this.GetComponent<UnityEngine.UI.Text>();
        if (text == null)
        {
            Debug.Log("Warning: No Text object attached to " + this.name);
        }
        else
        {
            text.enabled = false;
        }
	}
	
	// Update is called once per frame
	public void Update () {
        if (text.enabled && textEnableTime < 0.0f)
        {
            textEnableTime = Time.time;
        }
        else if (text.enabled && textEnableTime > 0.0f)
        {
            if (Time.time - textEnableTime > restartDelay)
            {
                Application.LoadLevel("Startup");
            }
        }
	}

    public void Trigger()
    {
        text.enabled = true;
    }

    UnityEngine.UI.Text text;
    float textEnableTime = -1.0f;
}
