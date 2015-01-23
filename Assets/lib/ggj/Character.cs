using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GGJ;

namespace GGJ {

	/// The animation states for characters
	public enum CharacterState {
		Static,
		Move,
		Attack,
		Jump,
		Dead,
		None
	}

	/// Marker to collecting and looking after Characters
	public class Character : MonoBehaviour {

		/// Static collection of all know character instances
		public static List<Character> All = new List<Character>();

		/// Box instance associated with this character
		public GameObject box = null;
		private GameObject _box = null;

		/// The actual animation state for this character, and the internal one
		public CharacterState state = CharacterState.Static;
		private CharacterState _state = CharacterState.None;

		void Start () {
			Character.All.Add(this);
			var self = N.Meta._(this);
			if (!self.has_component<Mob>()) {
				self.add_component<Mob>();
			}
		}

		void Update () {
			if ((state != _state) || (_box != box)) {
				_state = state;
				_box = box;
				N.Meta._(gameObject).cmp<Animator>().Play(_stateId(state));
			}
		}

		/// Return the state id for the given state code
		private string _stateId(CharacterState state) {
			var prefix = box != null ? "Box-" : "Gun-";
			switch (state) {
				case CharacterState.Static:
				case CharacterState.None:
					return prefix + "Static";
				case CharacterState.Move:
					return prefix + "Walk";
				case CharacterState.Attack:
					return "Gun-Attack";
				case CharacterState.Dead:
					return "Death";
			}
			return prefix + "Static";
		}
	}
}
