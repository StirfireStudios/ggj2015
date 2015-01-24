using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GGJ;
using N.Tests;

namespace GGJ {

	/// Marker to collecting and looking after Characters
	public class Character : Mob {

		/// Static collection of all know character instances
		new public static List<Character> All = new List<Character>();

		/// Box instance associated with this character
		public GameObject box = null;
		private GameObject _box = null;

		/// Pending damage to apply to this character
		private float _pendingDamage = 0;

		public void damage(float damage) {
			N.Console.log("Takes damage");
			this._pendingDamage = damage;
		}

		public void Start() {
			Character.All.Add(this);
		}

		void Update () {
			if (_updated()) {
				_update();
				N.Console.log(_stateId(_state));
				N.Meta._(gameObject).cmp<Animator>().Play(_stateId(_state));
			}
			if (this._pendingDamage != 0) {
			}
		}

		/// Check if the state is updated or not
		new protected bool _updated() {
			if (state == MobState.None) { return false; }
			return (state != MobState.None) || (box != _box);
		}

		/// Mark the state as updated
		new protected void _update() {
			N.Console.log("Updating!");
			_state = state;
			_box = box;
      state = MobState.None;
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
