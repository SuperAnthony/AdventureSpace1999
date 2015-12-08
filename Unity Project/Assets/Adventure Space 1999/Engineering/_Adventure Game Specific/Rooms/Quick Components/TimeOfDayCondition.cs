#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - June 5, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
using WhatPumpkin.ScriptingLanguage;
#endregion

// TODO: Remove this from this namespace asap
namespace WhatPumpkin.Sgrid.Environment {

	/// <summary>
	/// Time of day condition. Uses time of day to determine whether or not conditions are met before activating or deactivating an object on update
	/// </summary>

	[System.Serializable]

	public class TimeOfDayCondition : ConditionalGameObject   {

//		[SerializeField] string _timeOfDayRequirementKey = "";


		public TimeOfDayCondition() {
		

			//Debug.Log ("Time of Day Condition Constructed");
			
		
		}

		/// <summary>
		/// Check to see if the conditions are met for this object being active or not
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		/*
		protected override bool AreConditionsMet() {

			return Scripting.AreConditionsMet (_conditions) && (GameController.CurrentWorld == null || GameController.CurrentWorld.WorldTime.GetTimeOfDayKey () == _timeOfDayRequirementKey);

		}*/


	}
}