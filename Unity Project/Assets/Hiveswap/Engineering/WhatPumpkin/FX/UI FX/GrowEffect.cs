#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - August 21, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin.FX {

	public class GrowEffect : Effect, IPerformParams<string> {

		/// <summary>
		/// The speed of the grow effect.
		/// </summary>

		[SerializeField] float _speed = 3F;

		void Update() {
		
			if (_isActive) {

				// Grow the object
				float change = _speed * Time.deltaTime;
				this.transform.localScale = new Vector3(this.transform.localScale.x + change,
				                                        this.transform.localScale.y + change,
				                                        this.transform.localScale.z + change); 

			}
		
		}

		/// <summary>
		/// Play with the specified parameters.
		/// By default it will run the play method with no parameters
		/// </summary>
		/// <param name="parameters">Parameters.</param>

		public override void Play (string[] parameters)
		{
			if (parameters.Length > 0) {
			
				float.TryParse(parameters[0].ToString(), out _speed);
			
			}

			TryActivateAll ();

		}

		/// <summary>
		/// Activate this instance.
		/// </summary>

		public override void Activate ()
		{
			_isActive = true;

		}

		/// <summary>
		/// Deactivate this instance.
		/// </summary>

		public override void Deactivate() {
		
			_isActive = true;
			FinishPlaying ();

		}

	}
}