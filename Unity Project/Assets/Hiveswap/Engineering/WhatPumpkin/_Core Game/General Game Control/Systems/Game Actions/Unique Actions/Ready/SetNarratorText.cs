using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WhatPumpkin.Localization;

namespace WhatPumpkin.Actions {

	public class SetNarratorText : ActionType {
	
		#region constants
		
		/// <summary>
		/// The name.
		/// </summary>
		
		public const string NAME = "SetNar"; 

		#endregion


		#region argument receiver

		Type [] _validTypes = new Type[] {typeof(ILocalizedText)};


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
		/// Constructor
		/// </summary>
		
		public SetNarratorText() {

			_name = NAME;


		}

		/// <summary>
		/// Releases unmanaged resources and performs other cleanup operations before the
		/// <see cref="WhatPumpkin.Actions.SetNarratorText"/> is reclaimed by garbage collection.
		/// </summary>

		~SetNarratorText() {
	
		}

		bool _finishedPlaying = false;
		
		public override IEnumerator BeginAction(params object[] parameters) {
		
			// Make sure there is a parameter
			if (parameters.Length > 0) {

				_finishedPlaying = false;
				GameController.MessageManager.StartNarratorMessage(parameters[0].ToString());
				GameController.MessageManager.NarrativeMessageStopped += HandleNarrativeMessageStopped;
					
			}
			else {
				//Debug.LogError("No target is defined for the talk command.");
			}

			while (!_finishedPlaying) {
			
				yield return false;
			
			}

			EndAction ();
			yield break;
		}

		void HandleNarrativeMessageStopped (object sender, EventArgs e)
		{
			_finishedPlaying = true;
		}
	
	}
}
