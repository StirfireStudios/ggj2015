using UnityEngine;
using System;
using N;

namespace GGJ.Actions {

    [RequireComponent (typeof (Rigidbody))]
    public class Shoot : MonoBehaviour {

        /** How much damage do we do with this? */
        public float damage = 10f;

        /** How fast does the gun shoot */
        public float bullet_speed = 100f;

        /** Where do we shoot from */
        public Vector2 bullet_spawn_offsets = new Vector2(5, 5);

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
            if (character.alive) {
                if (character.box == null) {
                    if (character.RequestState(MobState.Attack, true)) {
                        _active = true;
                        _idle = 0f;
                        this._shoot(character);
                    }
                }
            }
        }

        /** Shoot a bullet from this user in the right direction, etc */
        private void _shoot(Mob mob) {
            var fx = mob.flipped ? -bullet_speed : bullet_speed;
            var ox = mob.flipped ? -bullet_spawn_offsets[0] : bullet_spawn_offsets[0];
            var force = new Vector3(fx, 5, 0);
            var instance = UnityEngine.Object.Instantiate(_bulletFactory) as GameObject;
            var bullet = N.Meta._(instance).cmp<Bullet>().damage = damage;
            var pos = this.gameObject.transform.position;
            pos.x += ox;
            pos.y += 5;
            instance.transform.position = pos;
            var rb = N.Meta._(instance).cmp<Rigidbody>();
            rb.AddForce(force);
        }
    }
}
