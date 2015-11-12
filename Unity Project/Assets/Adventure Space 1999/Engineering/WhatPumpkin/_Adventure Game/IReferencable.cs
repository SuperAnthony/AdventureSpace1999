using System;
using UnityEngine;
using System.Collections;

namespace WhatPumpkin {

	/// <summary>
	/// For objects which are referencable. Primarily, this is so that when a name is changed, the referencing object can update the name properly
	/// </summary>

	public interface IReferencable  {

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>

		string Key { get; }


		/// <summary>
		/// Occurs when the key name is changed.
		/// </summary>

		event EventHandler KeyChanged;

	}

	public class KeyChangedArgs : System.EventArgs {

		/// <summary>
		/// The old key.
		/// </summary>

		public string oldKey;

		/// <summary>
		/// The new key.
		/// </summary>

		public string newKey;

	}

}
