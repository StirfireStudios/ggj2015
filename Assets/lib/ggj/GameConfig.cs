using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InControl;

namespace GGJ
{

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

		Dictionary<InputDevice, Data.CharacterInfo.Type> DeviceCharMapping = new Dictionary<InputDevice, Data.CharacterInfo.Type>();
		
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
        }

    }
}
