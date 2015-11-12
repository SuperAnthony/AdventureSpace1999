using UnityEngine;
using System.Collections;

public class DestroyIfRequiredExists : MonoBehaviour {

	void Awake() {
	
		// Check to see if the required objects exists, if so the destroy this one
		GameObject go = GameObject.FindGameObjectWithTag ("Required Scene Objects");
		if (go != null) {
			// Destroy
			Destroy(this.gameObject);
		}
	}
}
