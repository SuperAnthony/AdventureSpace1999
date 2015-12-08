#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - February 24, 2015
#endregion 

#region using
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#endregion


namespace WhatPumpkin.Actions {

	public class Deactivate : ActionType {

		#region constants
		
		/// <summary>
		/// The name.
		/// </summary>
		
		public const string NAME = "Activate"; 
		
		#endregion
		
		#region argument receiver
		
		Type [] _validTypes = new Type[] {typeof(ISwitchable)};
		
		
		protected override Type[] ValidTypes {
			get {
				return _validTypes;
			}
		}
		
		List<IKeyed> _validArguments = new List<IKeyed> ();
		
		
		protected override List<IKeyed> ValidArguments {
			get {
				return _validArguments;
			}
		}
		
		
		#endregion

		#region methods

		/// <summary>
		/// Deactivate this instance.
		/// </summary>

		public Deactivate() {
			_name = "Deactivate";
		}

		public override IEnumerator BeginAction(params object[] parameters) {

			// Make sure there is a parameter
			if (parameters.Length > 0) {
				
				ISwitchable switchable = GameController.SceneManager.FindKeyedObject<ISwitchable>(parameters[0].ToString());
				
				if(switchable != null) {switchable.Deactivate();}
				else {
					Debug.LogError(parameters[0].ToString() + " Object not found in scene. Please check for correct spellings and that the correct game object is in the scene named properly.");
				}
				
			}
			else {
				Debug.LogError("Deactivate action no performed");
			}

			EndAction ();
			yield break;

		}

		#endregion

	}
}