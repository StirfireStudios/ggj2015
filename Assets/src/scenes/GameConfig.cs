using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InControl;

namespace GGJ
{
    public enum CharEnum {
        MaraQuinn,
        FourFiveSix,
        RickJames,
        Yamamoto
    };

    public class GameConfig
    {
        static GameConfig me;

        public static GameConfig Instance
        {
            get
            {
                if (me == null)
                    me = new GameConfig();

                return me;
            }
        }

        Dictionary<InputDevice, string> DeviceCharMapping = new Dictionary<InputDevice, string>();

        public void EnqueueMapping(InputDevice i, string c)
        {
            if (DeviceCharMapping.ContainsKey(i))
                DeviceCharMapping[i] = c;
            else 
                DeviceCharMapping.Add(i, c);
        }


        public void DebugMeh() {
            Debug.Log(String.Format("number of players: #{0}", DeviceCharMapping.Count));
        }

    }
}
