using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GGJ {

  /// The animation states for characters
  public enum MobState {
    Static,
    Move,
    Attack,
    Jump,
    Dead,
    None
  }

  /// Common behaviours and data for all mobile types
  public class Mob : MonoBehaviour {

    /// Static collection of all know character instances
    public static List<Mob> All = new List<Mob>();

    /// Are states currently locked for some reason?
    public bool allow_state_change;

    /// State external and internal
    protected MobState actual_state = MobState.Static;
    protected MobState _state = MobState.None;
    public MobState state {
        get { return actual_state; }
        set {
            if (allow_state_change) {
                if (!((value == MobState.Static) && (_state == MobState.Static))) {
                    actual_state = value;
                }
            }
        }
    }

    // Public setter so we can just use SendMessage / BroadcastMessage
    public void SetState(MobState newstate) {
      state = newstate;
    }

    void Start () {
      Mob.All.Add(this);
      allow_state_change = true;
    }

    void Update () {
      if (_updated()) {
        _update();
        N.Meta._(gameObject).cmp<Animator>().Play(_stateId(state));
      }
    }

    /// Check if the state is updated or not
    protected bool _updated() {
      if (state == MobState.None) { return false; }
      return (state != MobState.None);
    }

    /// Mark the state as updated
    protected void _update() {
      _state = actual_state;
      actual_state = MobState.None;
    }

    /// Return the state id for the given state code
    protected string _stateId(MobState state) {
      switch (state) {
        case MobState.Static:
        case MobState.None:
        return "Static";
        case MobState.Move:
        return "Walk";
        case MobState.Attack:
        return "Attack";
        case MobState.Dead:
        return "Death";
      }
      return "Static";
    }
  }
}
