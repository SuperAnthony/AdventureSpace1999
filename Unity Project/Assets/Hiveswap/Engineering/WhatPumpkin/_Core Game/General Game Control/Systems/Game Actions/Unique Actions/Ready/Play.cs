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
using WhatPumpkin.Actions.Sequences;
#endregion

namespace WhatPumpkin.Actions {
	
	/// <summary>
	/// Plays an IPerform keyed object or entity and waits for the performance to finish before ending the action
	/// </summary>
	
	public class Play: ActionType {
		
		
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
		
		#endregion
		
		#region methods
		
		List<bool> _isSequenceComplete = new List<bool>();
		
		public Play() {
			_name = "Play";
		}
		
		
		
		void HandleFinishedPlaying(object sender, EventArgs e) {
			
			// Get the performing object
			IPerform performingObject = (IPerform)sender;
			
			_isSequenceComplete[performingObject.PlayOrder] = true;
			
			// Unregister the event
			performingObject.FinishedPlaying -= HandleFinishedPlaying;
		}
		
		public override IEnumerator BeginAction(params object[] parameters) {
			
			int order = 0;
			bool performingObjectFound = false;
			
			
			// Make sure there is a parameter
			if (parameters.Length > 0) {
				
				// The object key, there should only be one parameter
				string key = parameters[0].ToString();
				
				IPerform performingObject = null;
				
				try {
					//Debug.Log ("Trying To Play: " + parameters [0].ToString ());
					performingObject = GameController.SceneManager.FindKeyedObject<IPerform>(key);
					
				}
				catch {
					
					//Debug.Log ("Could Not Find Performing Object");
					
					// Could not find by keyed object so try to find by entity instead
					// Don't think this should happen
					
					Entity entity = GameController.SceneManager.FindEntity(key);
					if(entity != null) {performingObject = entity.GetComponent(typeof(IPerform)) as IPerform;}
					
				}
				
				
				if(performingObject != null) {
					
					// If a performing object is found then set it's play order 
					
					// Set the order of the Play object
					performingObjectFound = true;
					order = _isSequenceComplete.Count;
					performingObject.PlayOrder = order;
					_isSequenceComplete.Add(false);

					
					performingObject.FinishedPlaying += HandleFinishedPlaying;
					performingObject.Play();
					
				}
				else 
				{
					// If it cannnot find the performing object then we finished playing 
					Debug.LogError("Was unable to retrieve the object '" + key + "' as an IPerformance object when performing the Play action");
				}
				
			}
			
			// Don't end the action until we've finished playing
			while (performingObjectFound && !_isSequenceComplete[order]) {
				// Wait here until finished playing 
				yield return false;
				
			}

			//Debug.Log ("Played Play Order ID: " + order.ToString());
			
			// End the action
			EndAction ();
			yield break;
		}
		
		
		#endregion
		
	}
}
