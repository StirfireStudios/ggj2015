using UnityEngine;
using System.Collections;
using N;

/**
 * A class for all moving characters to inherit from.
 */
public class GGJ15Character : MonoBehaviour {

    // @todo put this somewhere else
    public const string SPRITEROBJECT_TAG = "SpriterObject";

    /**
     * A GameObject tagged with SPRITEROBJECT_TAG.
     */
    public GameObject SpriterObject = null;

    /**
     * Speed.
     */
    public float Speed = 4.0f;

    /**
     * Rotation speed factor.
     */
    public float RotationSpeed = 3.0f;

    /**
     * Whether to draw debug lines.
     */
    public bool DrawDebugInfo = false;

    /**
     * The following variables are public, but serve little purpose at edit
     * time - they are, therefore, hidden using HideInInspector.
     */
    #region RuntimePublicVars
    [HideInInspector]
    public Vector3 DesiredHeading = Vector3.zero;
    [HideInInspector]
    public float DesiredSpeedFactor = 0.0f;
    #endregion

    const float _minSpeed = 0.05f;
    const float _invPI = 1.0f / Mathf.PI;

    /**
     * On instantiation.
     */
    public void Start()
    {
        if (SpriterObject == null)
            foreach (Transform child in transform)
            {
                if (child.CompareTag(SPRITEROBJECT_TAG))
                    SpriterObject = child.gameObject;
            }
    }

    /**
     * Every tick.
     */
    public void Update() {

        // Sorry! If you can figure out how to do this with SendMessage, feel free to fix it~
        GGJ.Mob mob = this.gameObject.GetComponentInChildren<GGJ.Character>();
        if (mob == null) {
            mob = this.gameObject.GetComponentInChildren<GGJ.Monster>();
        }

        /**
         * Cache the results of the cross and dot products of the forward
         * vector and the desired heading vector.
         */
        float DotProd = Vector3.Dot(transform.forward, DesiredHeading);
        Vector3 CrossProd = Vector3.Cross(transform.forward, DesiredHeading);

        /**
         * Calculate rotation speed scale.
         */
        float RotationSpeedScale = Mathf.Abs(((DotProd * _invPI) - 0.5f) * 2.0f) * RotationSpeed;

        if (DrawDebugInfo) {
            const float debugDrawLength = 4.0f;
            Debug.DrawLine(
                transform.position,
                transform.position + debugDrawLength * transform.forward.normalized,
                Color.red
            );
            Debug.DrawLine(
                transform.position,
                transform.position + debugDrawLength * DesiredHeading,
                Color.yellow
            );
        }

        /**
         * Only bother with visual and spatial updates if we get a
         * non-negligible speed factor.
         */
        if (DesiredSpeedFactor > _minSpeed)
        {
            // Rotate.
            if (1.0f - DotProd > 0.05f)
            {
                if (CrossProd.y > 0.0f)
                {
                    transform.Rotate(Vector3.up * (Time.deltaTime + 4) * RotationSpeedScale, Space.World);
                }
                else
                {
                    transform.Rotate(Vector3.up * (Time.deltaTime - 4) * RotationSpeedScale, Space.World);
                }
            }

            // Translate.
            if (mob.alive) {
                Vector3 Translation = Vector3.forward * Speed * DesiredSpeedFactor * Time.deltaTime;
                transform.Translate(Translation, Space.Self);

                // Process animation.
                if (SpriterObject != null)
                {
                    mob.SetState(GGJ.MobState.Move);

                    if (Mathf.Abs(DesiredHeading.x) > 0.10f)
                    {
                        // Send a flip message to the billboard, but only if we're
                        // moving substantially in the X axis.
                        bool bFlip =
                            Vector3.Dot(Vector3.right, DesiredHeading) < 0.0f ||
                            Vector3.Dot(Vector3.right, transform.forward) < 0.0f;
                        SpriterObject.SendMessage("FlipBillboard", bFlip);
                        mob.flipped = bFlip;
                    }
                }
            }
        }
        else {

            if (SpriterObject != null)
            {
                mob.SetState(GGJ.MobState.Static);
            }
        }
    }


    /**
     * Validate data entered into the inspector at edit time.
     */
    public void OnValidate() {

        // If a SpriterObject has been set, validate it.
        if (SpriterObject != null) {
            if (!SpriterObject.CompareTag(SPRITEROBJECT_TAG)) {
                Debug.LogWarning(
                    string.Format("Spriter object needs the tag '{0}'.", SPRITEROBJECT_TAG)
                );
            }
        }
    }
}
