#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// December, 2014
#endregion 


#region using
using UnityEngine;
using System.Collections;
using WhatPumpkin.Actions.Sequences;
#endregion

namespace WhatPumpkin.Sgrid {

	[System.Serializable]

	public class VerbActionSequence : ActionSequence  {

		#region fields

		/// <summary>
		/// The verb associated with this action sequence that will be displayed by the assocaited verb coin.
		/// </summary>

		[SerializeField] string _verb;

		#endregion

		#region properties

		/// <summary>
		/// Gets the verb coin.
		/// </summary>
		/// <value>The verb coin.</value>
		
		public string VerbCoinText { 
			get {

				// Prevent null reference
				if(_verb == null) {
					return "";
				}

				return _verb; }
		}

		#endregion

		/// <summary>
		/// Sets the verb.
		/// </summary>
		/// <param name="verbText">Verb text.</param>

		internal void SetVerb(string verbText) {
			_verb = verbText;
		}

#if UNITY_EDITOR

		/// <summary>
		/// Sets the properties.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="conditions">Conditions.</param>
		/// <param name="verb">Verb.</param>

		public override void SetProperties(string key, string conditions, string verb) {
		
			_key = key;
			_conditions = conditions;
			_verb = verb;
		}

#endif
	}
}
