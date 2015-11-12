#region using
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin.CameraManagement {

	/// <summary>
	/// Triggers Camera Switches
	/// Cameras will look for a component that implements this
	/// For HiveSwap, this will likely only be for the player and CanTriggerSwitch will be true if that player is active
	/// </summary>

	public interface ITriggersCamSwitch {

		#region methods

		/// <summary>
		/// Gets a value indicating whether this instance can trigger switch.
		/// </summary>
		/// <value><c>true</c> if this instance can trigger switch; otherwise, <c>false</c>.</value>

		bool CanTriggerSwitch { get; }

		#endregion

	}
}