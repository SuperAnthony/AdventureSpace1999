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
using WhatPumpkin.Actions.Sequences;
#endregion

namespace WhatPumpkin.Actions {
	
	/// <summary>
	/// Stops an IPerform keyed object or entity
	/// </summary>
	
	public class Stop : ActionType {

		
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

		public Stop() {
			_name = "Stop";


		}



		public override IEnumerator BeginAction(params object[] parameters) {


			// Make sure there is a parameter
			if (parameters.Length > 0) {


				// The object key, there should only be one parameter
				string key = parameters[0].ToString();

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


					performingObject.Stop();

				}
				else 
				{
					// If it cannnot find the performing object then we finished playing 
					Debug.LogError("Was unable to retrieve the object '" + key + "' as an IPerformance object when performing the Stop action");

				}

			}


			EndAction ();
			yield break;
		}
		
		
		#endregion
		
	}
}
