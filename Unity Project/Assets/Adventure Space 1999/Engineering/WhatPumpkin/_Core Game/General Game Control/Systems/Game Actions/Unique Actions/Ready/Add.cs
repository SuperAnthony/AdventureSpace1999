#region using
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WhatPumpkin.ScriptingLanguage;
#endregion

namespace WhatPumpkin.Actions {

	public class Add : ActionType {
		
		#region argument receiver
		
		Type [] _validTypes = new Type[] {typeof(IReceiver)};
		
		
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
				
				//Debug.Log("Returned Valid Arguments: " + _validArguments.Count);
				return _validArguments;
			}
		}

		/// <summary>
		/// Gets the minimum number of arguments. 1 argument by default.
		/// </summary>
		/// <value>The minimum number of arguments.</value>

		public override int MinArguments {
			get {
				return 2;
			}
		}

		/// <summary>
		/// Gets the max arguments if the instance does not have unlimited arguments.
		/// </summary>
		/// <value>The max arguments.</value>

		protected override int MaxArguments {
			get {
				return 2;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance has unlimited arguments.
		/// </summary>
		/// <value>true</value>
		/// <c>false</c>

		protected override bool HasUnlimitedArguments {
			get {
				return false;
			}
		}

		#endregion

		#region methods

		public Add() {
			_name = "Add";
		}

		public override IEnumerator BeginAction(params object[] parameters) {


			// Make sure there is a parameter
			if (parameters.Length > 0) {


				// Get the variable name and the value
				List<string> arguments =  Scripting.GetArguments(parameters[0].ToString());


				// Make sure we have enough arguments
				if(arguments.Count >= MinArguments) {

					// Argument 0 is the keyed object being searched for
					// Argument 1 is the value

					IReceiver receiver = (IReceiver)Keyed.KeyedDictionary[arguments[0]];

					if(receiver != null && arguments[1] != null) {
						receiver.Add(arguments[1]);
					}
				}
				else {
				
					Debug.LogError("Not enough parameters found for the ' " + _name + "' action. A minimum of " + MinArguments + " arguments required." );

				}

			}
		
			// End the action
			EndAction ();
			yield break;
		}

		#endregion


	}
}
