using N;
using UnityEngine;
using System.Collections;

namespace GGJ {

  /// This type should be applied to the 'ship part' boxes in the world.
  public class Box : MonoBehaviour {
    void Start () {
    }
    void Update () {
    }

    /// When this box comes in contact with a player, pick up the box
    void OnCollisionEnter(Collision collision) {
      var character = N.Meta._(collision.gameObject).cmp<Character>(true);
      if (character != null) {
        character.box = this.gameObject;
        this.gameObject.SetActive(false);
      }
    }
  }
}
