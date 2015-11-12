#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 12, 2015
#endregion 


#region using
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using WhatPumpkin.Entities;
using WhatPumpkin.FX;

#endregion

namespace WhatPumpkin.Actions {
	
	/// <summary>
	/// Plays an IPerform keyed object or entity but DOES NOT wait for the performance to finish before ending the action
	/// This may work just the same as Activate
	/// </summary>
	
	public class PlayFX : ActionType {

		
		#region argument receiver
		
		Type [] _validTypes = new Type[] {typeof(IPerformParams<string>)};
		
		
		protected override Type[] ValidTypes {
			get {
				return _validTypes;
			}
		}


		List<IKeyed> _validArguments = new List<IKeyed> ();
		
		
		/// <summary>
		/// Gets the valid arguments.
		/// </summary>
		/// <value>The valid arguments.</value>
		
		protected override List<IKeyed> ValidArguments {
			get {
				return _validArguments;
			}
		}
		
		#endregion

		#region methods

		public PlayFX() { 
			_name = "PlayFX";
		}

		bool _finishedPlaying = false;

		void HandleFinishedPlaying(object sender, EventArgs e) {
			
			//	Debug.Log ("finisehd playing - unregistered event");
			
			// Get the performing object
			Effect effect = (Effect)sender;
			
			_finishedPlaying = true;
			
			//EndAction ();
			
			// Unregister the event
			effect.FinishedPlaying -= HandleFinishedPlaying;
		}


		public override IEnumerator BeginAction(params object[] parameters) {
			
			if (parameters.Length > 0) {

				// Check to see if there is an IPerformParams type attached
				// The object key, there should only be one parameter
				string [] arguments = parameters[0].ToString().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
				string key = arguments[0];
				Effect effect = null;
				
				try {
					effect = GameController.SceneManager.FindKeyedObject<Effect>(key);				
				}
				catch {

					// Could not find by keyed object so try to find by entity instead
					// Don't think this should happen
					
					Entity entity = GameController.SceneManager.FindEntity(key);
					if(entity != null) {effect = entity.GetComponent(typeof(Effect)) as Effect;}
					
				}
				
				
				if(effect != null) {

					Effect FX = effect;

					// Get the primary FX from the effect
					foreach(Effect fx in effect.GetComponents<Effect>()) {
					
						if(fx.IsPrimaryEffect) {
						
							FX = fx;
						
						}
					
					}


					// Get the parameters for this play object
					string [] perform_parameters = new string[arguments.Length - 1];
					for(int i = 1; i < arguments.Length; i++) {
						perform_parameters[i - 1] = arguments[i];
					}


					_finishedPlaying = false;
					FX.FinishedPlaying += HandleFinishedPlaying;
					FX.Play(perform_parameters);
					
					
				}


			}


			while (!_finishedPlaying) {
				// Wait here until finished playing 
				yield return false;
				
			}

			_finishedPlaying = false;
			// End the action
			EndAction ();
			yield break;
		}
		
		
		#endregion
		
	}
}
