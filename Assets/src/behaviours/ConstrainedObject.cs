using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GGJ
{
    public class ConstrainedObject : MonoBehaviour
    {
        enum Axis {
            X,
            Y,
            Z
        }

        enum ConstraintType {
            Min,
            Max
        }

        Vector3 _min_scale = new Vector3(-1, -1, -1);
        Vector3 _min = Vector3.zero;

        Vector3 _max_scale = new Vector3(-1, -1, -1);
        Vector3 _max = Vector3.zero;

        Dictionary<ConstraintType, Dictionary<Axis, string>> Constraints = new Dictionary<ConstraintType, Dictionary<Axis, string>>() {
            {
                ConstraintType.Max,
                new Dictionary<Axis, string>() {
                     {Axis.Z,  "BGPlane" },
                }
            },
            {
                ConstraintType.Min,
                new Dictionary<Axis, string>() {
                     {Axis.Z,  "FGPlane" },
                }
            }
        };
        GameObject boundsCollection = null;

        private void ApplyConstraint(ConstraintType t, Axis a, string s)
        {
            var tr = boundsCollection.transform.Find(s);
            if (tr)
            {
                //var go = tr.gameObject;
                switch (a)
                {
                    case Axis.X:
                        switch (t)
	                    {
                            case ConstraintType.Min:
                                _min_scale.x = 1.0f;
                                _min.x = tr.position.x;
                                break;
                            case ConstraintType.Max:
                                _max_scale.x = 1.0f;
                                _max.x = tr.position.x;
                                break;
                            default:
                                break;
	                    }
                        break;
                    case Axis.Y:
                        switch (t)
                        {
                            case ConstraintType.Min:
                                _min_scale.y = 1.0f;
                                _min.y = tr.position.y;
                                break;
                            case ConstraintType.Max:
                                _max_scale.y = 1.0f;
                                _max.y = tr.position.y;
                                break;
                            default:
                                break;
                        }
                        break;
                    case Axis.Z:
                        switch (t)
                        {
                            case ConstraintType.Min:
                                _min_scale.z = 1.0f;
                                _min.z = tr.position.z;
                                break;
                            case ConstraintType.Max:
                                _max_scale.z = 1.0f;
                                _max.z = tr.position.z;
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void ApplyAllConstraints(Dictionary<ConstraintType, Dictionary<Axis, string>> constraints)
        {
            foreach (KeyValuePair<ConstraintType, Dictionary<Axis, string>> dic in constraints)
            {
                foreach (KeyValuePair<Axis, string> kvp in dic.Value)
	            {
                    this.ApplyConstraint(dic.Key, kvp.Key, kvp.Value);
	            }
            }

            // update scales (well, they're more like flags)
            Debug.Log("min:" + _min.ToString() + " " + _min_scale.ToString());
            Debug.Log("max:" + _max.ToString() + " " + _max_scale.ToString());
        }


        public void Start() {

            GameObject levelinfo = gameObject.findClosestParentWithTag("LevelInfo");
            if (levelinfo != null) {
                // Query the level info for AABB info.
                boundsCollection = levelinfo.getFirstChildWithTag("BoundsCollection");

                if (boundsCollection != null) {
                    ApplyAllConstraints(this.Constraints);
                }
            }
        }

        public void LateUpdate() {
            //Vector3 maxclamped = Vector3.Min(transform.position, _max);
            Vector3 pos = transform.position;

            if (_min_scale.x > 0.0f)
            {
                pos.x = Mathf.Max(_min.x, pos.x);
            }
            if (_max_scale.x > 0.0f)
            {
                pos.x = Mathf.Min(_max.x, pos.x);
            }
            if (_min_scale.y > 0.0f)
            {
                pos.y = Mathf.Max(_min.y, pos.y);
            }
            if (_max_scale.y > 0.0f)
            {
                pos.y = Mathf.Min(_max.y, pos.y);
            }
            if (_min_scale.z > 0.0f)
            {
                pos.z = Mathf.Max(_min.z, pos.z);
            }
            if (_max_scale.z > 0.0f)
            {
                pos.z = Mathf.Min(_max.z, pos.z);
            }

            transform.position = pos;
        }
    }
}
