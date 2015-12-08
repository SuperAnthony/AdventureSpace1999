#region copyright (c) 2015 What Pumpkin Studio
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
#endregion 

#region using
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WhatPumpkin.ScriptingLanguage;
#endregion

namespace WhatPumpkin.Actions {

	public class AutoSave : ActionType {

		#region argument receiver
		
		Type [] _validTypes = new Type[] {typeof(IGameVariable)};
		

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

		/// <summary>
		/// The name.
		/// </summary>
		
		public const string NAME = "AutoSave"; 


		#region methods

		public AutoSave() {
			_name = NAME;
		}

		public override IEnumerator BeginAction(params object[] parameters) {

            // TODO:
			//GameController.SaveLoadManager.Save (SaveLoad.AUTO_SAVE_SLOT);

			// End the action
			EndAction ();
			yield break;
		}

		#endregion


	}
}
