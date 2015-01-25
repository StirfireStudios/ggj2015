using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class GGJ15Camera : MonoBehaviour {

    private float _horizsafearea = 0.45f;
    public float HorizontalSafeArea = 0.9f;

    [Flags]
    private enum Scroll {
        None =      0,
        XNegative = 1 << 0,
        XPositive = 1 << 1,
        YNegative = 1 << 2,
        YPositive = 1 << 3
    }
    Scroll NecessaryScroll = Scroll.None;
    
    Vector2 ScreenSize = Vector2.zero;
    Vector2 ScreenSizeInvFactors = Vector2.zero;
    Vector2[] ScreenSpacePositions;
    GameObject[] Players;
    Dictionary<int, GameObject> PlayersByIdx = new Dictionary<int, GameObject>();

    Vector2 ScreenCenter = Vector2.zero;

    // Centroid position, in raw screen-space pixels.
    Vector3 CentroidCenter = Vector3.zero;
    float CentroidInvFactor = 0.0f;

    bool bAlive = false;

	void Start () {

        ScreenSize.Set(Screen.width, Screen.height);
        ScreenSizeInvFactors.Set(
            1f / ScreenSize.x,
            1f / ScreenSize.y
        );
        ScreenCenter = ScreenSize * 0.5f;      

	}

    void StartWatchingPlayers()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < Players.Count(); i++)
        {
            PlayersByIdx.Add(i, Players[i]);
        }
        ScreenSpacePositions = new Vector2[Players.Count()];

        CentroidInvFactor = ((float)Players.Count()) / 1.0f;

        bAlive = true;
    }

    void StopWatchingPlayers() {
    }

	void LateUpdate () {

        if (!bAlive)
            return;

        // Only care about active players.
        KeyValuePair<int, GameObject>[] ActivePlayers = PlayersByIdx.Where(kvp => kvp.Value.activeInHierarchy).ToArray();

        CentroidCenter = Vector2.zero;
        NecessaryScroll = Scroll.None;

        foreach (KeyValuePair<int, GameObject> kvp in ActivePlayers)
        {
            // Debug.Log(string.Format("{0}\t{1}", kvp.Value.name, gameObject.camera.WorldToScreenPoint(kvp.Value.transform.position)));
            Vector3 sspoint = gameObject.camera.WorldToScreenPoint(kvp.Value.transform.position);
            
            // Accumulate the centroid (which I guess we'll have be raw for now?)
            CentroidCenter += sspoint;

            // Normalize all SS positions into the -1.0f to 1.0f range.
            Vector2 sspointvec2 = new Vector2(
                (sspoint.x * ScreenSizeInvFactors.x) - 0.5f,
                (sspoint.y * ScreenSizeInvFactors.y) - 0.5f
            );
            sspointvec2 = (sspointvec2) * 2f;
            ScreenSpacePositions[kvp.Key] = sspointvec2;
            
            // Are we near a screen edge?
            if (Mathf.Abs(_horizsafearea) < Mathf.Abs(sspointvec2.x))
            {
                if (sspointvec2.x > 0.0f) {
                    NecessaryScroll |= Scroll.XPositive;
                }
                else {
                    NecessaryScroll |= Scroll.XNegative;
                }
            }
        }

        // Okay, now...
        if (NecessaryScroll != Scroll.None) {
            
            // Calculate centroid position.
            CentroidCenter *= CentroidInvFactor;

            // Convert centroid position into -1.0f to 1.0f notation.
            Vector3 CentroidCenterN = new Vector3(
                (CentroidCenter.x * ScreenSizeInvFactors.x) - 0.5f,                
                0.0f,
                0.0f//(CentroidCenter.y * ScreenSizeInvFactors.y) - 0.5f
            );
            CentroidCenterN = (CentroidCenterN) * 2f;

            Debug.DrawLine(
                transform.position,
                transform.position + (4.0f * CentroidCenterN),
                Color.cyan
            );

            Vector3 dir = CentroidCenterN.normalized;
            transform.Translate(dir * Time.deltaTime * 4.0f);

            Debug.Log(string.Format("[{2}] centroid: {0}\tcam: {1}", CentroidCenterN, ScreenCenter, Enum.GetName(typeof(Scroll), NecessaryScroll)));
        }
	}

    void OnValidate() {
        _horizsafearea = 0.5f * HorizontalSafeArea;
    }

    private void UpdateScreenSize(float f)
    {
        camera.orthographicSize = f;
        ScreenSize.Set(Screen.width, Screen.height);
        ScreenSizeInvFactors.Set(
            1f / ScreenSize.x,
            1f / ScreenSize.y
        );
        ScreenCenter = ScreenSize * 0.5f;
    }
}
