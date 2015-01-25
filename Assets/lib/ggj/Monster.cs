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

            // Cull dead targets
            if ((self.target) && (!self.target.alive)) {
                self.target = null;
                N.Console.log("Monster has no target");
                var controller = N.Meta._(self).cmp<GGJ15Character>();
                controller.DesiredSpeedFactor = 0f;
            }

            // Filter to characters who are alive
            characters = characters.FindAll(_alive);

            // Find new target
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
                self.target = target;
            }

            // Move towards target
            if (self.target != null) {
                var controller = N.Meta._(self).cmp<GGJ15Character>();
                var heading = self.target.gameObject.transform.position - self.transform.position;
                controller.DesiredHeading = heading;
                controller.DesiredHeading.Normalize();
                controller.DesiredSpeedFactor = self.move_speed;

                // If close, attack
                var dist = Vector3.Distance(self.target.transform.position, self.transform.position);
                if (dist < self.bounding_attack_distance) {
                    var attack = N.Meta._(self).cmp<Attack>(true);
                    if (attack != null) {
                        attack.apply(self.target);
                    }
                }
            }

            return null;
        }

        // Find characters who are alive
        private static bool _alive(Character c)
        {
            return c.alive;
        }
    }

    /** Marker for collecting and looking after Monsters */
    public class Monster : Mob {

        /** Currently visible? */
        public bool visible;

        /** Bounding attack distance to hit players */
        public float bounding_attack_distance = 4f;

        /** How fast should this monster move */
        public float move_speed = 0.5f;

        /** The game object this monster is currently hunting */
        public Character target;

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
        /*void OnCollisionEnter(Collision collision) {
            if (this.alive) {
                var character = N.Meta._(collision.gameObject).cmp<Character>();
                if ((character != null) && (character.alive)) {
                    if (character != null) {
                        var attack = N.Meta._(this).cmp<Attack>(true);
                        if (attack != null) {
                            N.Console.log("Triggered attack");
                            attack.apply(character);
                        }
                    }
                }
            }
        }*/
    }
}
