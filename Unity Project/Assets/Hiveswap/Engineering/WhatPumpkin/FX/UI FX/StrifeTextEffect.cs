#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - August 21, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
using WhatPumpkin.Entities;
using WhatPumpkin.Localization;
#endregion

namespace WhatPumpkin.FX {

	[RequireComponent(typeof(Entity))]
	[RequireComponent(typeof(LocalizeUIText))]
	[RequireComponent(typeof(DurationFX))]

	public class StrifeTextEffect : TextEffect {

		/// <summary>
		/// The localize user interface text component.
		/// </summary>
		
		LocalizeUIText _localizeUIText;

		/// <summary>
		/// The _duration FX.
		/// </summary>

		DurationFX _durationFX;

	
		/// <summary>
		/// Start this instance.
		/// </summary>

		void Awake() {

			// Get a localized text component
			_localizeUIText = MonoBehaviorUtils.GetForcedComponent<LocalizeUIText> (this.gameObject);
			_durationFX = MonoBehaviorUtils.GetForcedComponent<DurationFX> (this.gameObject);

		}

		/*
		void Update() {
		
			// If this is active then increase the time elapsed

			if (_isActive) {
			
				_timeElapsed += Time.deltaTime;
	
				if(_timeElapsed >= _duration) {
					Deactivate();
				}

			}

		
		}*/

		/// <summary>
		/// Play the specified parameters.
		/// </summary>
		/// <param name="parameters">Param 1 sets the strife text. Param 2 sets the duration</param>

		public override void Play(string [] parameters) {
		


			// Set the localzied text
			if (parameters.Length > 0 && parameters [0] != null) {

				try{
					_localizeUIText.SetMessageKey(parameters[0]);
				}
				catch {

					// Get a localized text component
					_localizeUIText = MonoBehaviorUtils.GetForcedComponent<LocalizeUIText> (this.gameObject);
					_localizeUIText.SetMessageKey(parameters[0]);
				
				}
			}

			// Set the duration
			if (parameters.Length > 1 && parameters [1] != null) {
				float duration = 0F;
				if(float.TryParse(parameters[1], out duration)) {
					_durationFX.SetDuration(duration);
				}
			}

			// Try to activate all
			TryActivateAll ();
		}



		public override void Activate() {

			_durationFX.Activate ();
			Show ();


		}

		/// <summary>
		/// Deactivate this instance.
		/// </summary>

		public override void Deactivate() {



			Hide ();

			// Let the game know that this has finished playing
			FinishPlaying ();
				
		}

		/// <summary>
		/// Show this instance.
		/// </summary>

		void Show() {
		
			this.GetComponent<UnityEngine.UI.Text> ().enabled = true;

		}
		/// <summary>
		/// Hide this instance.
		/// </summary>

		void Hide() {
		
			this.GetComponent<UnityEngine.UI.Text> ().enabled = false;

		}


	}
}