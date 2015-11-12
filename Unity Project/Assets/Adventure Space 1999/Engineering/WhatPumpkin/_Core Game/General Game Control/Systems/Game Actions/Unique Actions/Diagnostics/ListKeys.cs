#region Copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - July 10, 2015
#endregion

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

	public class ListKeys : ActionType {
	
		#region methods

		/// <summary>
		/// Activate this instance.
		/// </summary>
		
		public ListKeys() {
			_name = "ListKeys";
		}
		
		public override IEnumerator BeginAction(params object[] parameters) {


			// Display all the keys
			int i = 0;

			foreach (IKeyed iKeyed in Keyed.KeyList) {
				if(iKeyed.Key != "") {
					i++;
					Debug.Log ("Keyed: " + iKeyed.Key); 
				}

			}

			Debug.Log ("Unique Keys: " + i); 
			Debug.Log("Total Keys: " + Keyed.KeyList.Count);

			EndAction ();

			return null;
		}

		#endregion
	
	}
}
