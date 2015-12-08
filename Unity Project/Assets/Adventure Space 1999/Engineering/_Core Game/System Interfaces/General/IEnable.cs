#region using
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin {

	/// <summary>
	/// I enable interface.
	/// </summary>

	public interface IEnable  {

		/// <summary>
		/// Enable this instance.
		/// </summary>

		void Enable();

		/// <summary>
		/// Disable this instance.
		/// </summary>

		void Disable();

	}
}