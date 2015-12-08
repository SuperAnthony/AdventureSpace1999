#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 12, 2015
#endregion 


namespace WhatPumpkin.Actions {

	/// <summary>
	/// Utility functions for handling common behaviors with actions and action sequences.
	/// </summary>

	public class ActionUtils {


		#region frequency utilities

		/// <summary>
		/// The once frequency name.
		/// </summary>

		const string OnceFrequency = "ONCE";

		/// <summary>
		/// The always frequency.
		/// </summary>

		const string AlwaysFrequency = "ALWAYS";

		/// <summary>
		/// The after first frequency.
		/// </summary>

		const string AfterFirstFrequency = "AFTERFIRST";

		/// <summary>
		/// Checks to see if an IFrequency type meets frequency conditions.
		/// </summary>
		/// <returns><c>true</c>, if frequency conditions was meetsed, <c>false</c> otherwise.</returns>
		/// <param name="frequentObject">Frequent object.</param>
		
		static public bool MeetsFrequencyConditions(IFrequency frequentObject, int occurances) {

			string frequency = "";

			// Check for null references before getting the correct frequency 
			if(frequentObject.Frequency != null) {frequency = frequentObject.Frequency.ToUpper();}

			// Return true if this action should occur everytime - most common
			if (frequency == "" || frequency == AlwaysFrequency) {
				return true;
			}
			
			// Return true if this action should only play once and the action sequence has not occured
			else if (frequency == OnceFrequency && occurances == 1)
			{
				return true;
			}
			
			// Return true if frequency is after first and this sequence has occured once
			else if (frequency == AfterFirstFrequency && occurances > 1) {
				return true;
			}

			return false;
		
		}

		#endregion

		/// <summary>
		/// There will not be any instances of the ActionUtils.
		/// </summary>

		private ActionUtils(){
		}

	}
}