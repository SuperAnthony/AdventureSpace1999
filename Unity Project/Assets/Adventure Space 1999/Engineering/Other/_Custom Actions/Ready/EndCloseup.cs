// TODO: Date January 29, 2015

#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WhatPumpkin;
#endregion

namespace WhatPumpkin.Actions {

	/// <summary>
	/// Closeup action.
	/// </summary>

	public class EndCloseup : ActionType {
	
		#region methods

		/// <summary>
		/// Activate this instance.
		/// </summary>
		
		public EndCloseup() {
			_name = "EndCloseup";
		}
		
		public override IEnumerator BeginAction(params object[] parameters) {

			// End a closeup if we're in one
			if (GameController.CamManager != null) {
								GameController.CamManager.EndCloseup ();
						}

			EndAction ();

			return null;
		}

		#endregion
	
	}
}
