using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GGJ;
using N.Tests;

namespace GGJ {

    /// Marker for collecting and looking after Characters
    public class Monster : Mob {
        void Start () {
            SetState(MobState.Static);
        }
    }
}
