using UnityEngine;
using GGJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GGJ
{
    public static class GameObjectExtensions
    {
        public static GameObject findClosestParentWithTag(this GameObject me, string tagToFind)
        {

            var parent = me.transform.parent;
            while (parent != null) {
                if (parent.tag == tagToFind) {
                    return parent.gameObject as GameObject;
                }
                parent = parent.transform.parent;
            }
            return null;
        }

        private static List<GameObject> getAllChildrenWithTagImpl(GameObject go, string tag) {

            List<GameObject> kids = new List<GameObject>();

            int count = go.transform.childCount;
            for (int i = 0; i < count; i++)
            {
                GameObject child = go.transform.GetChild(i).gameObject;
                if (child.CompareTag(tag))
                    kids.Add(child);

                List<GameObject> grandkids = getAllChildrenWithTagImpl(go, tag);
                kids.Concat(grandkids);
            }

            return kids;        
        }

        public static List<GameObject> getAllChildrenWithTag(this GameObject me, string tagToFind)
        {
            return getAllChildrenWithTagImpl(me, tagToFind);       
        }

        private static GameObject getFirstChildWithTagImpl(GameObject go, string tag) {
            GameObject obj = null;

            int count = go.transform.childCount;
            for (int i = 0; i < count; i++)
            {
                GameObject child = go.transform.GetChild(i).gameObject;
                if (child.CompareTag(tag))
                {
                    obj = child;
                    break;
                }

                GameObject descendant = getFirstChildWithTagImpl(child, tag);
                if (descendant != null)
                {
                    obj = descendant;
                    break;
                }
            }

            return obj;
        }

        public static GameObject getFirstChildWithTag(this GameObject me, string tagToFind)
        {
            return getFirstChildWithTagImpl(me, tagToFind);
        }
    }
}
