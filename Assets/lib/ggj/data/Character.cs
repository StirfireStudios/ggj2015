using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GGJ;
using N.Tests;

namespace GGJ {

  /// Marker for collecting and looking after Characters
  public class Character : Mob {

    /// Static collection of all know character instances
    public static List<Character> All = new List<Character>();

    /// Box instance associated with this character
    public GameObject box = null;
    private GameObject _box = null;

    public void damage(float damage) {
      // TODO this or something
      N.Console.log("Takes damage");
      N.Meta._(this).cmp<DamageBlip>().activate();
    }

    public void Start() {
        Character.All.Add(this);
    }

    void OnDestroy() {
        Character.All.Remove(this);
    }

    void Update () {
        var old = _state.state;
        var next = _state.next();
        if (next != MobState.None) {
            N.Meta._(gameObject).cmp<Animator>().Play(_stateId(next));
        }
        else if (_box != box) {
            _state.reset();
            _state.request(old);
            N.Meta._(gameObject).cmp<Animator>().Play(_stateId(_state.next()));
        }
    }

    /// Return the state id for the given state code
    new protected string _stateId(MobState state) {
        var postfix = box != null ? "_Box" : "_Gun";
        switch (state) {
            case MobState.Static:
            case MobState.None:
                return "Idle" + postfix;
            case MobState.Move:
                return "Walk" + postfix;
            case MobState.Jump:
                return "Jump" + postfix;
            case MobState.Attack:
                return "GunShotStanding";
            case MobState.Dead:
                return "Death_Gun";
        }
        return "Idle";
    }
  }
}
