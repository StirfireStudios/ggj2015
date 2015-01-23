using N;
using UnityEngine;
using GGJ;

namespace GGJ.Scenes.Characters {

	/// Main script for the 'Characters' test scene.
	public class UiManager : MonoBehaviour {

		public void Start() {
		}

		public void Update() {
		}

		/// Set characters to hold a box
		public void StateBox() {
			for (var i = 0; i < Character.All.Count; ++i) {
				Character.All[i].box = this.gameObject;
			}
		}

		/// Set characters to not hold a box
		public void StateNoBox() {
			for (var i = 0; i < Character.All.Count; ++i) {
				Character.All[i].box = null;
			}
		}

		/// Display the static animation for all characters
		public void AnimateStatic() {
			for (var i = 0; i < Character.All.Count; ++i) {
				Character.All[i].state = MobState.Static;
			}
		}

		/// Display the static animation for all characters
		public void AnimateMove() {
			for (var i = 0; i < Character.All.Count; ++i) {
				Character.All[i].state = MobState.Move;
			}
		}

		/// Display the static animation for all characters
		public void AnimateAttack() {
			for (var i = 0; i < Character.All.Count; ++i) {
				Character.All[i].state = MobState.Attack;
			}
		}

		/// Display the static animation for all characters
		public void AnimateJump() {
			for (var i = 0; i < Character.All.Count; ++i) {
				Character.All[i].state = MobState.Jump;
			}
		}

		/// Display the static animation for all characters
		public void AnimateDead() {
			for (var i = 0; i < Character.All.Count; ++i) {
				Character.All[i].state = MobState.Dead;
			}
		}
	}
}
