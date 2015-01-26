using UnityEngine;
using System.Collections;

namespace GGJ {

	/** Behaviour for bullet types */
	public class Bullet : MonoBehaviour {

		/** Damage value for this bullet */
		public float damage;

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

		/** When this bullet comes in contact with a monster, attack it */
		void OnCollisionEnter(Collision collision) {
			var monster = N.Meta._(collision.gameObject).cmp<Monster>(true);
			if (monster != null) {
				N.Console.log("Shot monster");
				monster.damage(damage);
				N.Meta._(this).destroy();
			}
		}
	}
}
