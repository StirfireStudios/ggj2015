using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;
using GGJ;

public class InputWrangler : MonoBehaviour {

    int NumDevices = 0;

    GGJ.GameConfig gc = GGJ.GameConfig.Instance;

	// Use this for initialization
	void Start () {
        gc.DebugMeh();

        // spawn in what we need!
        foreach (KeyValuePair<InputDevice, GGJ.Data.CharacterInfo.Type> kvp in gc.DeviceCharMapping)
        {
            GGJ.Data.CharacterInfo chtype = GGJ.Data.Characters.Instance.CharacterList[kvp.Value];

            // spawn location
            Vector3 SpawnLoc = Vector3.zero;
            SpawnLoc.x = Camera.main.transform.position.x;


            // Instantiate "empty player" container.
            GameObject go = Resources.Load("EmptyPlayer") as GameObject;
            GameObject sg = Resources.Load(chtype.SpriteResource) as GameObject;
            sg.transform.position = Vector3.zero;

            GameObject player = (GameObject)Instantiate(go, SpawnLoc, Quaternion.identity);
            GameObject spriterobj = (GameObject)Instantiate(sg, SpawnLoc, Quaternion.identity);
            spriterobj.transform.SetParent(player.transform);
            player.name = chtype.Name;
            player.SendMessage("SetControllingDevice", kvp.Key);

            //ControllingDevice


            // Instantiate a SpaceGuy.
            
            //sg.transform.SetParent(go.transform);

            
            //Instantiate(go)
            


        }

        // tell the camera to start watching players
        Camera.main.SendMessage("StartWatchingPlayers");

	}
	
	// Update is called once per frame
	void Update () {
	    NumDevices = InputManager.Devices.Count;

//Debug.Log(string.Format("{0} devices.", NumDevices));
	}
}
