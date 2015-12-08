#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - December 14, 2014
#endregion 

#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

#endregion

namespace WhatPumpkin.Actions {


	[System.Serializable]

	public class Action : IFrequency {

		#region static fields
		
		/// <summary>
		/// The _frequency options.
		/// </summary>
		
		static string []  _frequencyOptions = new string[] {"ALWAYS","ONCE","AFTERFIRST"};
		
		#endregion

		#region static properties
		
		/// <summary>
		/// Gets the frequency options.
		/// </summary>
		/// <value>The frequency options.</value>
		
		public static string []  FrequencyOptions { get { return _frequencyOptions; } }
		
		#endregion

		#region member fields

		/// <summary>
		/// The conditions required to be true in order to perform this action.
		/// </summary>
		
		[SerializeField] string _conditions;


		/// <summary>
		/// The type of the action.
		/// </summary>

		[SerializeField] string _actionType;

		/// <summary>
		/// The parameters separated by commas.
		/// </summary>

		[SerializeField] string _parameters;


		/// <summary>
		/// When is this Action Playerd
		/// </summary>
		[SerializeField] string _frequency; 

		#endregion

		#region member properties

		/// <summary>
		/// Gets the type of the action.
		/// </summary>
		/// <value>The type of the action.</value>

		public string ActionType { 
			get { 


				// Prevent null reference
				if(_actionType == null) {return "";}

				return _actionType; 
			} 
		}

		/// <summary>
		/// Gets the parameters.
		/// </summary>
		/// <value>The parameters.</value>

		public string Parameters { 
			get {

				// Prevent null reference
				if(_parameters == null) {
					return "";
				}

				return _parameters; 
			} 
		}

		/// <summary>
		/// Gets the conditions.
		/// </summary>
		/// <value>The conditions.</value>

		public string Conditions { get { 

				// Prevent null reference
				if(_conditions == null) {return "";}

				return _conditions;
			} 
		}

		/// <summary>
		/// Gets the frequency of when this action is played.
		/// </summary>
		/// <value>The frequency.</value>

		public string Frequency { get 
			{ 

				// Prevent null reference
				if(_frequency == null) {return "";}

				return _frequency;
			} 
		}


		#endregion


		#region methods

		public Action() {

#if UNITY_EDITOR
			//Debug.Log ("Action Created");
			// Register the renamed event
			Keyed.Renamed += HandleRenamed;
			WhatPumpkin.Sgrid.Entity.Renamed += HandleRenamed;
#endif

		}

	
		~Action() {
		
#if UNITY_EDITOR
			//Debug.Log ("Action Destroyed");
			// Unregister event;
			Keyed.Renamed -= HandleRenamed;
			WhatPumpkin.Sgrid.Entity.Renamed -= HandleRenamed;
#endif

		}

		#endregion



		void HandleRenamed (object sender, RenameKeyEventArgs e)
		{

			// TODO: Replace the old parameter keys with the new
			//Debug.Log ("Key Renamed - Old Key: " + e.OldKey + " New Key: " + e.NewKey); 

			// Change the parameters
			List<string> parameters = ScriptingLanguage.Scripting.GetArguments (_parameters);

			bool parameterChange = false;

			int i = 0;

			for (i = 0; i < parameters.Count; i++) {
			
				if(parameters[i] != null && parameters[i] != "" && parameters[i] == e.OldKey) {
				
					parameterChange = true;
					parameters[i] = e.NewKey;
				}
			
			}


			if (parameterChange) {
			
				_parameters = "";

				for(i = 0; i < parameters.Count; i++) {
				
					_parameters+=parameters[i];
				
					if(i != parameters.Count - 1) {
						_parameters+=",";
					}

				}
			}


		}


		/// <summary>
		/// Sets the properties.
		/// </summary>
		/// <param name="actionType">Action type.</param>
		/// <param name="parameters">Parameters.</param>
		/// <param name="conditions">Conditions.</param>
		/// <param name="frequency">Frequency.</param>

		public void SetProperties(string actionType, string parameters, string conditions, string frequency) {

			_actionType = actionType;
			_parameters = parameters;
			_conditions = conditions;
			_frequency = frequency;
			
		}


	}
}
