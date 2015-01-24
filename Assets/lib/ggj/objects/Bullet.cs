using UnityEngine;
using System.Collections;

namespace GGJ {

	/** Behaviour for bullet types */
	public class Bullet : MonoBehaviour {

		/** Maximum lifetime this bullet will live for */
		public float maxLife;

		/** Actual life so far */
		private float _life;

		void Start () {
			_life = 0f;
		}

		void Update () {
			_life += Time.deltaTime;
			if (_life > maxLife) {
				N.Meta._(this).destroy();
			}
		}
	}
}
