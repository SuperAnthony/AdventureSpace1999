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

	public class DurationFX : Effect {

		/// <summary>
		/// The _duration.
		/// </summary>
		
		[SerializeField] float _duration = 5F;
		
		/// <summary>
		/// The time that's elapsed since this effect was active
		/// </summary>
		
		float _timeElapsed = 0F;


		[SerializeField] bool _deactivatePrimaryEffect = true;

		void Update() {
			
			// If this is active then increase the time elapsed
			
			if (_isActive) {
				
				_timeElapsed += Time.deltaTime;
				
				if(_timeElapsed >= _duration) {
					Deactivate();
				}
				
			}	
			
		}

		/// <summary>
		/// Sets the duration.
		/// </summary>
		/// <param name="duration">Duration.</param>

		public void SetDuration(float duration) {
		
			_duration = duration;

		}

		/// <summary>
		/// Activate this instance.
		/// </summary>

		public override void Activate() {
			_timeElapsed = 0F;
			_isActive = true;
		}

		/// <summary>
		/// Deactivate this instance.
		/// </summary>

		public override void Deactivate() {
		
			_isActive = false;
			_timeElapsed = 0F;


			if (_deactivatePrimaryEffect) {

				// Make sure to only deactivate the primary effect if it's inactive
								Effect primaryEffect = Effect.GetPrimaryEffect (this.gameObject);

								if (primaryEffect != null && !primaryEffect.IsActive) {
										primaryEffect.Deactivate ();

								}
						}
		
		}

	}
}
