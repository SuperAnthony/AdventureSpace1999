// December 19, 2014

using UnityEngine;
using System.Collections;

namespace WhatPumpkin.Actions.Sequences {

	[System.Serializable]

	/// <summary>
	/// Use action sequence.
	/// </summary>

	public class UseActionSequence : ActionSequence  {

		/// <summary>
		/// The item.
		/// </summary>

		[SerializeField] string _item = "";

		/// <summary>
		/// Gets the item.
		/// </summary>
		/// <value>The item.</value>

		public string Item { get { return _item; } }
	}
}
