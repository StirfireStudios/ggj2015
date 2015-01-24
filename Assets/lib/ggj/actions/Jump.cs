using UnityEngine;
using System;
using N;

namespace GGJ.Actions {

    [RequireComponent (typeof (Rigidbody))]
    public class Jump : MonoBehaviour {

        /** Is this component currently active? */
        private bool _active;

        /** The magnitude of the jump effect on this target */
        public float magnitude;

        /** Idle timeout to mark as 'not jumping anymore' */
        public float idle_timeout;
        private float threshold = 0.3f;

        /** The 'up' vector to apply */
        public Vector3 up;
        private Vector3 _up;
        private Rigidbody _rb;

        /** Determines if currently in the air */
        public bool airbourne;

        /** Idly time */
        private float _idle;

        /** Setup; not jumping */
        public void Start() {
            airbourne = false;
            _up = up.normalized * magnitude;
            _rb = N.Meta._(this).cmp<Rigidbody>();
        }

        /** Check if still jumping for idle motion */
        public void Update() {
            if (Math.Abs(_rb.velocity.y) < threshold) {
                _idle += Time.deltaTime;
            }
            else if (!airbourne) {
                _idle = 0;
                airbourne = true;
                _active = true;
            }
            if (this._active) {
                if (_idle > idle_timeout) {
                    airbourne = false;
                    var character = N.Meta._(this).cmp<Character>();
                    character.FinishedState();
                    this._active = false;
                }
            }
        }

        /** Trigger this effect on the target */
        public void apply() {
            if (!airbourne) {
                var character = N.Meta._(this).cmp<Character>();
                if (character.SetState(MobState.Jump, true)) {
                    _rb.AddForce(this._up);
                    _idle = 0;
                    _active = true;

                    // Stop anyone who's busy shooting
                    var shoot = N.Meta._(this).cmp<Shoot>();
                    shoot.Stop();
                }
            }
        }
    }
}
