// December 24 2014

using UnityEngine;
using System.Collections;

namespace WhatPumpkin {

	public class DestroyOnAwake : MonoBehaviour {

		// Use this for initialization
		void Awake () {
			// Destroy this game object on awake
			Destroy (this.gameObject);
		}
	}
}
