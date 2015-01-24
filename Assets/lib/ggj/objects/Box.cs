using N;
using UnityEngine;
using System.Collections;
using System;

namespace GGJ {

    /** This type should be applied to the 'ship part' boxes in the world. */
    public class Box : MonoBehaviour {

        /** The threshold of all the things */
        public float threshold = 3f;

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
                N.Console.log(down);
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
            t.y += 12f;
            this.gameObject.transform.position = t;
            N.Console.log(this);
            var rb = N.Meta._(this).cmp<Rigidbody>();
            if (rb != null) {
                rb.AddForce(new Vector3(0, 1000, 0));
                N.Console.log("New object up");
            }
        }
    }
}
