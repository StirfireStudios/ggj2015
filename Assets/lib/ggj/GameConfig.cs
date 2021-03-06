﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InControl;

namespace GGJ
{
    // @todo rename this class?
    public class GameConfig
    {
        static GameConfig instance;

		static public GameConfig Instance
		{
			get
			{
				if (instance == null)
					instance = new GameConfig();
				return instance;
			}
		}

		public int NumberOfPlayers 
		{
			get {
				return DeviceCharMapping.Count;
			}
		}

        public List<GameObject> BoxesReturned = new List<GameObject>();

        public Dictionary<InputDevice, Data.CharacterInfo.Type> DeviceCharMapping { get; protected set; }

        private GameConfig()
        {
            DeviceCharMapping = new Dictionary<InputDevice, Data.CharacterInfo.Type>();
        }
		
		public void SetCharacterForDevice(InputDevice i, Data.CharacterInfo.Type c)
        {
			DeviceCharMapping[i] = c;
        }

		public void RemoveCharacterForDevice (InputDevice i) 
		{
			if (DeviceCharMapping.ContainsKey(i))
			{
				DeviceCharMapping.Remove(i);
			}
		}

        public void DebugMeh() {
            Debug.Log(String.Format("number of players: #{0}", DeviceCharMapping.Count));
			foreach (InputDevice device in DeviceCharMapping.Keys)
			{
				Data.CharacterInfo chara = Data.Characters.Instance.CharacterList[DeviceCharMapping[device]];
				Debug.Log ("Char is: " + chara.Name);
			}
        }

    }
}
