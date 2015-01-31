using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GGJ
{

    public class BoxDropZone : MonoBehaviour
    {

        GameConfig cfg = GameConfig.Instance;
        void OnTriggerEnter(Collider other) {

            var character = N.Meta._(other.gameObject).cmp<Character>(true);
            var box = N.Meta._(other.gameObject).cmp<Box>(true);

            if (character != null)
            {
                bool CharacterHasBox = character.box != null;
                Debug.Log(
                    String.Format(
                        "{0} has box: {1}",
                        other.gameObject.name,
                        CharacterHasBox.ToString()
                    )
                );

                if (CharacterHasBox)
                {
                    N.Meta._(character.box).cmp<Box>().deliver(character.gameObject);
                    cfg.BoxesReturned.Add(character.box);
                    Debug.Log(
                        String.Format(
                            "Box being delivered! Boxes delivered: {0}",
                            cfg.BoxesReturned.Count()
                        )
                    );
                    character.box = null;
                }
            }
/*            else if (box != null)
            {
                cfg.BoxesReturned.Add(other.gameObject);

                Debug.Log(
                    String.Format(
                        "Box being delivered! Boxes delivered: {0}",
                        cfg.BoxesReturned.Count()
                    )
                );
            }


            /*
            var box = N.Meta._(other.gameObject).cmp<Box>(true);
            if (box != null) {
                
            }
             * */

            //GameObject other_go = other.gameObject;
            //other_go.SendMessage("SetBoxDeliverable", true);

        }

        void OnTriggerExit(Collider other)
        {
            //GameObject other_go = other.gameObject;
            //other_go.SendMessage("SetBoxDeliverable", false);
            var box = N.Meta._(other.gameObject).cmp<Box>(true);
            if (box != null)
            {
                cfg.BoxesReturned.Remove(other.gameObject);

                Debug.Log(
                    String.Format(
                        "Box being removed! Boxes delivered: {0}",
                        cfg.BoxesReturned.Count()
                    )
                );
            }
        
        }
    }
}