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
using WhatPumpkin.Sgrid;

#endregion

namespace WhatPumpkin.Actions {

	public class SwitchHand : ActionType {

		#region constants

		/// <summary>
		/// The name.
		/// </summary>
		
		public const string NAME = "SwitchHand"; 

		#endregion

		#region argument receiver
		
		Type [] _validTypes = new Type[] {typeof(IKeyed)};
		
		
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

		public SwitchHand() {
			_name = NAME;

		}

		public override IEnumerator BeginAction(params object[] parameters) {
//			Debug.Log ("Begin Action");
			
			// Make sure there is a parameter
			if (parameters.Length > 0) {

				string key = parameters[0].ToString();


					
					Prop prop = GameController.SceneManager.FindKeyedObject<Prop>(key);
					if(prop == null) {
				
						// TODO: this should exist in the FindKeyedObject scene manager but I'm not quite sure why isn't or just plain not working

						// Look in each entity
						Entity entity = GameController.SceneManager.FindEntity(key);
						if(entity != null) {prop = entity.GetComponent<Prop>();}

					}



					if(prop != null) {prop.SwitchHand();}
					else {
					Debug.LogError(parameters[0].ToString() + " object not found in scene. Please check for correct spellings and that the correct game object is in the scene named properly.");
					}


			}
			else {
				Debug.LogError("No parameters found for the SwitchHand command.");
			}

			EndAction ();
			yield break;

		}

		#endregion

	}
}