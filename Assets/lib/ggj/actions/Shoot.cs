using UnityEngine;
using System;
using N;

namespace GGJ.Actions {

    [RequireComponent (typeof (Rigidbody))]
    public class Shoot : MonoBehaviour {

        /** Is this component currently active? */
        private bool _active;

        /** How long has animation been idle for? */
        private float _idle;

        /** Idle animation threshold */
        private float _threshold = 0.5f;

        /** Disable shooting explicitly */
        public void Stop() {
            _active = false;
        }
        
        public void Start() {
            this._idle = 0f;
        }

        public void Update() {
            var anim = N.Meta._(this).cmp<Animator>();
            if (_active && !anim.IsInTransition(0)) {
                _idle += Time.deltaTime;
                if (_idle > this._threshold) {
                    N.Console.log("Probably finished animation");
                    _active = false;
                    var character = N.Meta._(this).cmp<Character>();
                    character.allow_state_change = true;
                }
            }
        }

        /** Trigger this effect on the target */
        public void apply() {
            var character = N.Meta._(this).cmp<Character>();
            if ((character.box == null) && (character.allow_state_change)) {
                character.state = MobState.Attack;
                character.allow_state_change = false;
                _active = true;
                _idle = 0f;
            }
        }
    }
}
