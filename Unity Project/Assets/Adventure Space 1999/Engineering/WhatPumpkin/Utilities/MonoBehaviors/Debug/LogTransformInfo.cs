// May 18, 2015

using UnityEngine;
using System.Collections;

namespace WhatPumpkin {

	public class LogTransformInfo : MonoBehaviour {



		/// <summary>
		/// Log the position info?
		/// </summary>

		[SerializeField] bool _logPosition;

		/// <summary>
		/// Log the rotation info?
		/// </summary>

		[SerializeField] bool _logEulerRotation;

		/// <summary>
		/// Log the scale info?
		/// </summary>

		[SerializeField] bool _logScale;

		
		// Update is called once per frame
		void Update () {


			//Debug.Log (this.name + " Rotation: " + this.transform.eulerAngles);
		
			if(_logPosition) 
				Debug.Log (this.name + " Position: " + this.transform.position);

			if(_logEulerRotation)
				Debug.Log (this.name + " Rotation: " + this.transform.eulerAngles);

			if(_logScale)
				Debug.Log (this.name + " Scale: " + this.transform.localScale);

		}
	}
}
