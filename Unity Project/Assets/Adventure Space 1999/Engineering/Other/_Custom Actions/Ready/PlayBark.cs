#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 29, 2015
#endregion 

#region using
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using WhatPumpkin.ScriptingLanguage;

using WhatPumpkin.Sgrid;
using WhatPumpkin.Localization;
#endregion

namespace WhatPumpkin.Actions {

	public class PlayBark : ActionType {

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

		public PlayBark() {
			_name = "PlayBark";
		}

		/// <summary>
		/// Did the bark end
		/// </summary>

		bool didBarkEnd = false;

		/// <summary>
		/// Begin the action.
		/// </summary>
		/// <param name="">.</param>
		/// <returns>The action.</returns>
		/// <param name="parameters">Parameters.</param>

		public override IEnumerator BeginAction(params object[] parameters) {

			// Make sure there is a parameter
			if (parameters.Length > 0) {

				// Get the arguments
				List<string> arguments = Scripting.GetArguments(parameters[0].ToString());


				// End action if there aren't enough arguments
				if(arguments.Count < MinArguments) {
					Debug.LogError("Not enough arguments to perform the StartBark Command. Start Bark requires at least two arguments 1. A Message Key 2. ");
					EndAction ();
					yield break;
				}



				// Bark
				if(arguments[0] != null && arguments[1] != null) {

					// Get the arguments
					string gameObjectKey = arguments[0];
					string barkTextKey = arguments[1];

		
					// Perform a bark and get the bark instance

					//Debug.Log ("Bark Character Mesh: " + GameController.SceneManager.FindEntity(gameObjectKey));
					// This is suboptimal
					WhatPumpkin.Screens.Bark bark = GameController.MessageManager.Bark(barkTextKey, GameObject.Find(gameObjectKey).transform.position);
				
					if(bark != null) {
						// Subscribe the bark end event
						bark.BarkEnd += HandleBarkEnd;
					}
					else {
						// Something went wrong, end
						didBarkEnd = true;
					}

					// Temp
					didBarkEnd = true;
				
				}
				else {
					didBarkEnd = true;
				}

			}
			else {
				Debug.LogError("No parameters found for the Bark action.");
			}

			// Wait until the bark ends
			while (didBarkEnd == false) {
				yield return null;
			}

			// The bark has ended, end the action
			EndAction ();
			yield break;
		}

		void HandleBarkEnd(object sender, EventArgs e) {
		
			// Get the bark
			WhatPumpkin.Screens.Bark bark = (WhatPumpkin.Screens.Bark)sender;

			if (bark != null) {
				// unsubscribe 
				bark.BarkEnd -= HandleBarkEnd;
			}

			// The bark ended
			didBarkEnd = true;
		}


	}
}
