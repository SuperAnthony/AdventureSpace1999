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
	/// Set the camera focal length
	/// </summary>
	
	public class SetFOV : ActionType {

		
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

		public SetFOV() { 
			_name = "SetFOV";
		}


		public override IEnumerator BeginAction(params object[] parameters) {
			
			if (parameters.Length > 0) {

				// Get the main camera
				Camera camera = GameController.CamManager.ActiveCamera;

				float fov = 0;

				if(float.TryParse(parameters[0].ToString(), out fov)) {
				
					camera.fieldOfView = fov;
				
				}


			}
			
			// End the action
			EndAction ();
			yield break;
		}
		
		
		#endregion
		
	}
}
