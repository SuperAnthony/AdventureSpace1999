// Anthony Paul Albion, Copyright (c) 2015

using UnityEngine;
using WhatPumpkin.Actions.Sequences;

namespace WhatPumpkin.Sgrid {

	/// <summary>
	/// Verb action sequence script.
	/// </summary>

	public class VerbActionSequenceScript : ActionSequenceScript, IVerbCoin {

		#region fields

		/// <summary>
		/// The verb associated with this action sequence
		/// </summary>
		 
		[SerializeField] string _verb;

		#endregion

		#region properties

		public VerbActionSequence VerbActionSequence {
		
			get {

				// Generate the vas from script
				VerbActionSequence verbActionSequence = GenerateActionSequence<VerbActionSequence>();
				// Set the verb
				verbActionSequence.SetVerb(_verb);
				// Retrun the vas
				return verbActionSequence;

			}
		
		}


		#endregion

	}
}
