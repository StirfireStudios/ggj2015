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

    /// State external and internal
		public MobState state = MobState.Static;
		protected MobState _state = MobState.None;

    void Start () {
			Mob.All.Add(this);
    }

    void Update () {
      if (_updated()) {
        _update();
        N.Meta._(gameObject).cmp<Animator>().Play(_stateId(state));
      }
    }

    /// Check if the state is updated or not
    protected bool _updated() {
      return state != _state;
    }

    /// Mark the state as updated
    protected void _update() {
      _state = state;
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
