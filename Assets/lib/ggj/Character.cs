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

		public void Start() {
			Character.All.Add(this);
		}

		void Update () {
			if (_updated()) {
				_update();
				N.Console.log("Updated state");
				N.Meta._(gameObject).cmp<Animator>().Play(_stateId(state));
			}
		}

		/// Check if the state is updated or not
		new protected bool _updated() {
			return (state != _state) || (box != _box);
		}

		/// Mark the state as updated
		new protected void _update() {
			_state = state;
			_box = box;
		}

		/// Return the state id for the given state code
		new protected string _stateId(MobState state) {
			var prefix = box != null ? "Box-" : "Gun-";
			switch (state) {
				case MobState.Static:
				case MobState.None:
					return prefix + "Static";
				case MobState.Move:
					return prefix + "Walk";
				case MobState.Jump:
					return prefix + "Jump";
				case MobState.Attack:
					return "Gun-Attack";
				case MobState.Dead:
					return "Death";
			}
			return prefix + "Static";
		}
	}
}
