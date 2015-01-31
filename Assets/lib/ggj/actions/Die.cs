using UnityEngine;
using System;
using N;

namespace GGJ.Actions {

    [RequireComponent (typeof (Rigidbody))]
    public class Die : MonoBehaviour {

        /** Amount of time to be idle and dead */
        public float idle_death = 4f;

        /** Is this component currently active? */
        public bool _active;

        /** How long has animation been idle for? */
        private float _idle;
        private float _idle_death;

        /** Idle animation threshold */
        private float _threshold = 0.5f;

        /** Death plane **/
        private float _deathY = -20.0f;

        public void Start() {
            _idle = 0f;
            _idle_death = 0f;
        }

        public void Update() {
            var anim = N.Meta._(this).cmp<Animator>();
            if (_active && (!anim.IsInTransition(0)))
            {
                _idle += Time.deltaTime;
                if (_idle > this._threshold)
                {
                    _idle_death += Time.deltaTime;
                    if (_idle_death > idle_death)
                    {
                        _active = false;
                        this.active = false;
                    }
                }
            }
            else if (_active && (this.transform.localPosition.y < _deathY))
            {
                this.apply();
            }
        }

        /** Trigger this effect on the target */
        public void apply() {
            Mob mob = N.Meta._(this).cmp<Monster>(true);
            if (mob == null) {
                mob = N.Meta._(this).cmp<Character>(true);
            }
            mob.ForceState(MobState.Dead);
            _active = true;
            _idle = 0f;
            mob.alive = false;
        }
    }
}
