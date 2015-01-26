using UnityEngine;
using System.Collections;
using N;

public class DamageBlip : MonoBehaviour {

	public bool is_active = false;
	public float duration = 5.0f;
	private float life;

	void Start () {
		is_active = false;
		N.Meta._(this).opacity = 0;
	}

	void Update () {
		if (is_active) {
			life += Time.deltaTime;
			var relative = 1.0f - life / duration;
			N.Meta._(this).opacity = relative;
			if (life > duration) {
				is_active = false;
			}
		}
	}

	/// Activate the effect
	public void activate() {
		life = 0f;
		is_active = true;
	}
}
