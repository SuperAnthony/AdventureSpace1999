using UnityEngine;
using System.Collections;

public class ControlAnimation : MonoBehaviour {

	AIPath aiPath;
	Animator anim;

	// Use this for initialization
	void Start () {
	
		aiPath = this.GetComponentInParent<AIPath> ();
		anim = this.GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
	
		float speed = Mathf.Abs(aiPath.CurrentVelocity.x) + Mathf.Abs(aiPath.CurrentVelocity.y) + Mathf.Abs(aiPath.CurrentVelocity.z);

		Debug.Log ("Speed: " + speed);

		anim.SetFloat ("walkSpeed", speed);

	}
}
