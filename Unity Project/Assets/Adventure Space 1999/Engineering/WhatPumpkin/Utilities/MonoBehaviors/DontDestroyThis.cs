using UnityEngine;
using System.Collections;

public class DontDestroyThis : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this);
	}

}
