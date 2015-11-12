#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 29, 2015
#endregion 


#region using
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WhatPumpkin.ScriptingLanguage;

using WhatPumpkin.Localization;
using WhatPumpkin.Entities;
#endregion

namespace WhatPumpkin.Actions {

	public class StartBark : ActionType {

		
		#region argument receiver

		/// <summary>
		/// Gets a value indicating whether this instance has unlimited arguments.
		/// </summary>
		/// <value><c>true</c> if this instance has unlimited arguments; otherwise, <c>false</c>.</value>
		
		protected override bool HasUnlimitedArguments {get { return false; } } 

		/// <summary>
		/// Gets the minimum arguments.
		/// </summary>
		/// <value>The minimum arguments.</value>

		public override int MinArguments { get { return 2; } } // Set Default

		/// <summary>
		/// Gets the max arguments if the instance does not have unlimited arguments.
		/// </summary>
		/// <value>The max arguments.</value>
		
		protected override int MaxArguments { get { return 2; } } // Set Default

		
		Type [] _validTypes = new Type[] {typeof(ILocalizedText), typeof(IEntity)};
		
		
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
		
		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="WhatPumpkin.Actions.StartBark"/> class.
		/// </summary>

		public StartBark() {
			_name = "StartBark";
		}

		/// <summary>
		/// Releases unmanaged resources and performs other cleanup operations before the
		/// <see cref="WhatPumpkin.Actions.StartBark"/> is reclaimed by garbage collection.
		/// </summary>

		~StartBark() {
		
		}

		public override IEnumerator BeginAction(params object[] parameters) {

			// Make sure there is a parameter
			if (parameters.Length > 0) {

				// Get the variable name and the value
				string script = parameters[0].ToString();

				// Get the arguments 
				List<string> arguments = Scripting.GetArguments(script);

				// End action if there aren't enough arguments
				if(arguments.Count < MinArguments) {
					Debug.LogError("Not enough arguments to perform the StartBark Command. Start Bark requires at least two arguments 1. A Message Key 2. ");
					EndAction ();
					yield break;
				}

				// Get the barkTextKey and the Entity key that is required to send this message
				string barkTextKey = arguments[1]; string entityKey = arguments [0];


				// Perform the Bark
				try {

					// Send Message
					GameController.MessageManager.Bark(barkTextKey, GameObject.Find(entityKey).transform.position);
				}
				catch {
				
					Debug.LogError("Could not perform the Start Bark command '" + barkTextKey + "' on the entity '" + entityKey + "'");
				}

			}
			else {
				Debug.LogError("No parameters found for the Bark action.");
			}

			// End the action
			EndAction ();
			yield break;
		}


	}
}
