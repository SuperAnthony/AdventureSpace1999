using UnityEngine;
using System.Collections;

public class PlayAnimation : MonoBehaviour {

	public string animationName;

	// Use this for initialization
	void Start () {
		// Change animation
		Debug.Log ("Attempt to play animation");
		Animator animator = this.GetComponent<Animator> ();
		if(animator != null) {
			Debug.Log("Animation Found");
			animator.Play (animationName);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
