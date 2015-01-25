using N;
using UnityEngine;
using System.Collections;
using System;

namespace GGJ {

    /** Try to periodically spawn monsters from a monster spawner if it's visible */
    public class MonsterSpawner : MonoBehaviour {

        /** The threshold of all the things */
        public float threshold = 3f;

        /** Vertical offset for throws */
        public float offset_up = 12f;

        /** Force to apply to self on throw */
        public float force_up = 1000f;

        void Start () {
        }

        void Update () {
        }

        /** When this box comes in contact with a player, pick up the box */
        void OnCollisionEnter(Collision collision) {
            var character = N.Meta._(collision.gameObject).cmp<Character>(true);
            if (character != null) {
                character.box = this.gameObject;
                this.gameObject.SetActive(false);
            }
            else {
                var rb = N.Meta._(this).cmp<Rigidbody>();
                var down = Math.Abs(rb.velocity.y);
                if (down > threshold) {
                    N.Meta._(this).destroy();
                    N.Console.log("Lost a crate!");
                }
            }
        }

        /** Character has chosen to get rid of this box, throw it into the air */
        public void dispose(GameObject root) {
            this.gameObject.SetActive(true);
            var t = root.transform.position;
            t.y += offset_up;
            this.gameObject.transform.position = t;
            var rb = N.Meta._(this).cmp<Rigidbody>();
            if (rb != null) {
                rb.AddForce(new Vector3(0, force_up, 0));
            }
        }
    }
}
