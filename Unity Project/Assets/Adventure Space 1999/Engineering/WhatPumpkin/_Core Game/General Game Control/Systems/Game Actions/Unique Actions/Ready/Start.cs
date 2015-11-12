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

#endregion

namespace WhatPumpkin.Actions {
	
	/// <summary>
	/// Plays an IPerform keyed object or entity but DOES NOT wait for the performance to finish before ending the action
	/// This may work just the same as Activate
	/// </summary>
	
	public class Start : ActionType {

		
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

		public Start() { 
			_name = "Start";
		}


		public override IEnumerator BeginAction(params object[] parameters) {
			
			if (parameters.Length > 0) {

				// Check to see if there is an IPerformParams type attached
				// The object key, there should only be one parameter
				string [] arguments = parameters[0].ToString().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
				string key = arguments[0];
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
						performingObject.Play();
						
						
					}
					else 
					{
						// If it cannnot find the performing object then we finished playing 
						Debug.LogError("Was unable to retrieve the object '" + key + "' as an IPerformance object when performing the Start action");
						
					}
										
				}

			}
			
			// End the action
			EndAction ();
			yield break;
		}
		
		
		#endregion
		
	}
}
