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

	public class Activate : ActionType {

		#region constants

		/// <summary>
		/// The name.
		/// </summary>
		
		public const string NAME = "Activate"; 

		#endregion

		#region argument receiver
		
		Type [] _validTypes = new Type[] {typeof(IActivatable)};
		
		
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
		/// Activate this instance.
		/// </summary>

		public Activate() {
			_name = "Activate";

		}

		public override IEnumerator BeginAction(params object[] parameters) {
			// Make sure there is a parameter
			if (parameters.Length > 0) {

				// TODO: Create a more general method that passes an action delegate to determine which method to invoke

				List<string> arguments = GetArguments(parameters[0].ToString());

				foreach(string argument in arguments) {

					IActivatable activatable = GameController.SceneManager.FindKeyedObject<IActivatable>(argument);

					if(activatable != null) {activatable.Activate();}
					else {
						Debug.LogError(argument.ToString() + " object not found in scene. Please check for correct spellings and that the correct game object is in the scene named properly.");
					}
				}

			}
			else {
				Debug.LogError("No target is defined for the Activate command.");
			}

			EndAction ();
			yield break;

		}

		#endregion

	}
}