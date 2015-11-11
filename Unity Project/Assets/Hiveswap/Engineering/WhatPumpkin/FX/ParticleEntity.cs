#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - August 5th, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;

using WhatPumpkin.Entities;
#endregion

namespace WhatPumpkin.FX {

	/// <summary>
	/// Tools for activating, deactivating and controlling particle effects
	/// </summary>

	public class ParticleEntity : Entity, ISwitchable {

		#region fields

		/// <summary>
		/// The _is active.
		/// </summary>

		bool _isActive = false;


		#endregion

		#region properties

		/// <summary>
		/// Gets a value indicating whether this instance is active.
		/// </summary>
		/// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>

		public bool IsActive { get { return _isActive; } }

		#endregion

		#region methods

		/// <summary>
		/// Activate this instance.
		/// </summary>

		public override void Activate() {

//			Debug.Log ("Particle Activate");
		
			ParticleSystem particleSystem = this.GetComponent<ParticleSystem> ();

			if (particleSystem != null) {
			
				particleSystem.Play();

			}


			_isActive = true;


		}

		/// <summary>
		/// Deactivate this instance.
		/// </summary>

		public override void Deactivate() {

//			Debug.Log ("Particle Deactivate");
		
			_isActive = false;

			
			ParticleSystem particleSystem = this.GetComponent<ParticleSystem> ();
			
			if (particleSystem != null) {
				
				particleSystem.Stop();
				
			}


		}

		#endregion

	}
}
