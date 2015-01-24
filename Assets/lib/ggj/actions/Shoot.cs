using UnityEngine;
using System;
using N;

namespace GGJ.Actions {

    [RequireComponent (typeof (Rigidbody))]
    public class Shoot : MonoBehaviour {

        /** Is this component currently active? */
        public bool _active;

        /** How long has animation been idle for? */
        private float _idle;

        /** Idle animation threshold */
        private float _threshold = 0.5f;

        /** Bullet factory */
        private GameObject _bulletFactory = null;

        /** Disable shooting explicitly */
        public void Stop() {
            _active = false;
            _idle = 0;
        }

        public void Start() {
            _idle = 0f;
            _bulletFactory = Resources.Load("objects/Bullet", typeof(GameObject)) as GameObject;
        }

        public void Update() {
            var anim = N.Meta._(this).cmp<Animator>();
            if (_active && (!anim.IsInTransition(0))) {
                _idle += Time.deltaTime;
                if (_idle > this._threshold) {
                    _active = false;
                    var character = N.Meta._(this).cmp<Character>();
                    character.FinishedState();
                }
            }
        }

        /** Trigger this effect on the target */
        public void apply() {
            var character = N.Meta._(this).cmp<Character>();
            if (character.box == null) {
                if (character.RequestState(MobState.Attack, true)) {
                    _active = true;
                    _idle = 0f;
                    this._shoot();
                }
            }
        }

        /** Shoot a bullet from this user in the right direction, etc */
        private void _shoot() {
            var force = new Vector3(100, 5, 0);
            var instance = UnityEngine.Object.Instantiate(_bulletFactory) as GameObject;
            var pos = this.gameObject.transform.position;
            pos.x += 5;
            pos.y += 5;
            instance.transform.position = pos;
            var rb = N.Meta._(instance).cmp<Rigidbody>();
            rb.AddForce(force);
        }
    }
}
