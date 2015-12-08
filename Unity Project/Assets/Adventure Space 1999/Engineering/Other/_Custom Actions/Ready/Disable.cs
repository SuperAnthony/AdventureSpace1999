#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - December 19, 2014
#endregion

#region using
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#endregion

namespace WhatPumpkin.Actions {

	/// <summary>
	/// Disables a specified object when used in an action sequence.
	/// </summary>

	public class Disable : ActionType {

		
		#region constants
		
		
		/// <summary>
		/// The name.
		/// </summary>
		
		public const string NAME = "Disable"; 
		
		#endregion
		
		#region argument receiver
		
		Type [] _validTypes = new Type[] {typeof(IEnable)};
		
		
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


		public Disable() {
			_name = NAME;
		}

		public override IEnumerator BeginAction(params object[] parameters) {

			// Make sure there is a parameter
			if (parameters.Length > 0) {

				List<string> arguments = ScriptingLanguage.Scripting.GetArguments(parameters[0].ToString());

				foreach(string argument in arguments) {

					IEnable item = GameController.SceneManager.FindKeyedObject<IEnable>(argument);
					
					if(item != null) {
						item.Disable();
					}
					else {
						Debug.LogError(argument.ToString() + " object not found in scene. Please check for correct spellings and that the correct game object is in the scene named properly.");
					}
				}

			}

			
			// End the action
			EndAction ();
			yield break;
		}
	}
}
