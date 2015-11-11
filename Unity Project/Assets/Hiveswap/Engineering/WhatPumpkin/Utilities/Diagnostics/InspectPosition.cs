using UnityEngine;
using System.Collections;

public class InspectPosition : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		print (this.name + ": " + this.transform.position);
		print (this.transform.position.y);
	}
}
