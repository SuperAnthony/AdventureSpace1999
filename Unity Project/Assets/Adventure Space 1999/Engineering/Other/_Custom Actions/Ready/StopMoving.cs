#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - August 18, 2015
#endregion 


#region using
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using WhatPumpkin.Sgrid.Characters;
#endregion

namespace WhatPumpkin.Actions {
	
	/// <summary>
	/// Stops an IPerform keyed object or entity
	/// </summary>
	
	public class StopMoving : ActionType {

		
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

		/// <summary>
		/// Initializes a new instance of the <see cref="WhatPumpkin.Actions.StopMoving"/> class.
		/// StopMoving stops the active pc, it is one action with no paramaters "StopPC"
		/// </summary>

		public StopMoving() {
			_name = "StopPC";
		}



		public override IEnumerator BeginAction(params object[] parameters) {


			// Make sure there is a parameter
			if (parameters.Length > 0) {


				// The object key, there should only be one parameter
//				string key = parameters[0].ToString();
				GameController.PartyManager.ActivePC.StopMoving();

			}


			EndAction ();
			yield break;
		}
		
		
		#endregion
		
	}
}
