using UnityEngine;
using System;
using N;

namespace GGJ.Actions {

    [RequireComponent (typeof (Rigidbody))]
    public class Attack : MonoBehaviour {

        /** How much damage does this attack do? */
        public float damage = 10f;

        /** Is this component currently active? */
        public bool _active;

        /** How long has animation been idle for? */
        private float _idle;

        /** Idle animation threshold */
        private float _threshold = 0.5f;

        public void Start() {
            _idle = 0f;
        }

        public void Update() {
            var anim = N.Meta._(this).cmp<Animator>();
            if (_active && (!anim.IsInTransition(0))) {
                _idle += Time.deltaTime;
                if (_idle > this._threshold) {
                    _active = false;
                    var monster = N.Meta._(this).cmp<Monster>();
                    monster.FinishedState();
                }
            }
        }

        /** Trigger this effect on the target */
        public void apply(Character target) {
            var monster = N.Meta._(this).cmp<Monster>();
            if (monster.alive) {
                if (monster.RequestState(MobState.Attack, true)) {
                    _active = true;
                    _idle = 0f;
                    target.damage(damage);
                }
            }
        }
    }
}
