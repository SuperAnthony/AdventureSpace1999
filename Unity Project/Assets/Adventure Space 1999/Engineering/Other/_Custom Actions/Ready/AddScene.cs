#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - September 24, 2015
#endregion 

#region using
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace WhatPumpkin.Actions {

	public class AddScene : ActionType {

		#region constants

		/// <summary>
		/// The name.
		/// </summary>
		
		public const string NAME = "AddScene"; 

		#endregion

		#region argument receiver
		
		Type [] _validTypes = new Type[] {typeof(string)};
		
		
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

		public AddScene() {
//			Debug.Log ("Add Scene Created");
			_name = NAME;

		}

		public override IEnumerator BeginAction(params object[] parameters) {
//			Debug.Log ("Begin Action");
			
			// Make sure there is a parameter
			if (parameters.Length > 0) {

				AsyncOperation async = Application.LoadLevelAdditiveAsync(parameters[0].ToString());;

				yield return async;

				//while(!async.isDone);

			}
			else {
				Debug.LogError("No scene defined for the Add Scene action.");
			}

			EndAction ();
			yield break;

		}

		#endregion

	}
}