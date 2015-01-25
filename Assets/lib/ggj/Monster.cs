using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GGJ;
using GGJ.Actions;
using N;

namespace GGJ {

    /** Behaviour types */
    public interface IMonsterBehaviour {

        /**
         * Do something
         *
         * If that something results in a new behaviour (eg. we're low on hp),
         * then return the new behaviour to apply instead
         */
        IMonsterBehaviour Update(Monster self, List<Character> characters);
    }

    /** Don't do anything, upgrade state if we're seen */
    public class IdleWaitBehaviour : IMonsterBehaviour {
        public IMonsterBehaviour Update(Monster self, List<Character> characters) {
            if (self.visible) {
                N.Console.log("Monster wakes");
                return new RunAndKillBehaviour();
            }
            return null;
        }
    }

    /** Basic run and kill closest target behaviour */
    public class RunAndKillBehaviour : IMonsterBehaviour {
        public IMonsterBehaviour Update(Monster self, List<Character> characters) {
            if ((characters.Count > 0) && (self.target == null)) {
                var min = Vector3.Distance(characters[0].transform.position, self.transform.position);
                var target = characters[0];
                for (var i = 1; i < characters.Count; ++i) {
                    var dist = Vector3.Distance(characters[i].transform.position, self.transform.position);
                    if (dist < min) {
                        min = dist;
                        target = characters[i];
                    }
                }
                N.Console.log("Monster seeks " + target);
                self.target = target.gameObject;

                // Run towards target if further than attack distance

                // If closer than attack distance, ATTTAAAAACCKK RARRRRRARARRrrrr~
            }
            return null;
        }
    }

    /** Marker for collecting and looking after Monsters */
    public class Monster : Mob {

        /** Currently visible? */
        public bool visible;

        /** The game object this monster is currently hunting */
        public GameObject target;

        /** The behaviour we're currently using */
        private IMonsterBehaviour _brain;

        /** Poll interval for render state */
        public float poll_interval = 1f;
        private float _idle = 0f;

        public void Start () {
            SetState(MobState.Static);
            _brain = new IdleWaitBehaviour();
            target = null;
        }

        public void Update() {

            // Update visibility detection
            _idle += Time.deltaTime;
            if (_idle > poll_interval) {
                _idle = 0;
                var render = N.Meta._(this).cmp<SpriteRenderer>();
                this.visible = render.isVisible;
            }

            // Update brain state
            var next = _brain.Update(this, Character.All);
            if (next != null) {
                _brain = next;
            }

            // Push states
            _updateAnimationState();
        }

        /** When this monster comes in contact with a player, damage that player and render an attack animation */
        void OnCollisionEnter(Collision collision) {
            N.Console.log("Collision with life state of:" + this.alive);
            if (this.alive) {
                N.Console.log(collision.gameObject);
                var character = N.Meta._(collision.gameObject).cmp<Character>();
                N.Console.log("Character: " + character);
                if (character != null) {
                    var attack = N.Meta._(this).cmp<Attack>(true);
                    if (attack != null) {
                        N.Console.log("Triggered attack");
                        attack.apply(character);
                    }
                }
            }
        }
    }
}
