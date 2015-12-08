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

using WhatPumpkin.Sgrid;

#endregion

namespace WhatPumpkin.Actions {
	
	/// <summary>
	/// Plays a random Action Sequence
	/// </summary>
	
	public class Randomize : ActionType {

		
		#region argument receiver
		
		Type [] _validTypes = new Type[] {typeof(IPerform)};
		
		
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

		bool _finishedPlaying = false;
		
		#endregion

		#region methods

		public Randomize() { 
			_name = "Randomize";
		}


		string GetRandomlyGeneratedKey(string [] arguments) {
		
			int element = (int) UnityEngine.Random.Range (0, arguments.Length) % arguments.Length;

			return arguments [element];
		
		}


		
		void HandleFinishedPlaying(object sender, EventArgs e) {
			
			Debug.Log ("Finished Playing Randomize");
			
			// Get the performing object
			IPerform performingObject = (IPerform)sender;
			
			_finishedPlaying = true;

			// Unregister the event
			performingObject.FinishedPlaying -= HandleFinishedPlaying;
		}

		public override IEnumerator BeginAction(params object[] parameters) {
			
			if (parameters.Length > 0) {

				// Get Multiple arguments
				string [] arguments = parameters[0].ToString().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
				// Get a key
				string key = GetRandomlyGeneratedKey(arguments);


				IPerformParams<string> performParams = null;
				
				try {
					performParams = GameController.SceneManager.FindKeyedObject<IPerformParams<string>>(key);				
				}
				catch {

					// Could not find by keyed object so try to find by entity instead
					// Don't think this should happen
					
					Entity entity = GameController.SceneManager.FindEntity(key);
					if(entity != null) {performParams = entity.GetComponent(typeof(IPerformParams<string>)) as IPerformParams<string>;}
					
				}
				
				
				if(performParams != null) {

					// Get the parameters for this play object
					string [] perform_parameters = new string[arguments.Length - 1];
					for(int i = 1; i < arguments.Length; i++) {
						perform_parameters[i - 1] = arguments[i];
					}
					
					performParams.Play(perform_parameters);
					
					
				}
				else 
				{
					// Otherwise check for a normal perform object
					IPerform performingObject = null;
					
					try {
						performingObject = GameController.SceneManager.FindKeyedObject<IPerform>(key);				
					}
					catch {
						
						Debug.Log ("Could Not Find Performing Object");
						
						// Could not find by keyed object so try to find by entity instead
						// Don't think this should happen
						
						Entity entity = GameController.SceneManager.FindEntity(key);
						if(entity != null) {performingObject = entity.GetComponent(typeof(IPerform)) as IPerform;}
						
					}
					
					
					if(performingObject != null) {
						_finishedPlaying = false;
						performingObject.Play();
						performingObject.FinishedPlaying += HandleFinishedPlaying;
						_finishedPlaying = false;
						
					}
					else 
					{
						// If it cannnot find the performing object then we finished playing 
						Debug.LogError("Was unable to retrieve the object '" + key + "' as an IPerformance object when performing the Randomize action");
						_finishedPlaying = true;
					}
										
				}



			}

			// Don't end the action until we've finished playing
			while (!_finishedPlaying) {
				// Wait here until finished playing 
				yield return false;
				
			}

			Debug.Log ("End Action");
			
			// End the action
			EndAction ();
			yield break;
		}
		
		
		#endregion
		
	}
}
