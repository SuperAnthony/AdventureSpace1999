#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - June 2, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin {

	/// <summary>
	/// Interface for implementing cursor controls
	/// </summary>

	public interface ICursorControl {

		/// <summary>
		/// Sets the cursor using the _cursors collection in the game controller.
		/// </summary>
		/// <param name="index">Index.</param>
		
		void SetCursor (int index);
		
		/// <summary>
		/// Sets the cursor.
		/// </summary>
		/// <param name="cursorIcon">Cursor icon.</param>
		
		void SetCursor(WhatPumpkin.Cursor cursorIcon);

		/// <summary>
		/// Sets the cursor.
		/// </summary>
		/// <param name="key">Key.</param>

		void SetCursor(string key);

		/// <summary>
		/// Uses the default cursor.
		/// </summary>

		void UseDefaultCursor ();

		/// <summary>
		/// Handle rollovers
		/// </summary>
		/// <param name="cursorRolloverOverride">Cursor rollover override.</param>

		void HandleRollover(Cursor cursorRolloverOverride);

		/// <summary>
		/// Handles mouse rolling out of an interactive object
		/// </summary>

		void HandleRollout();

		/// <summary>
		/// Changes the default cursor.
		/// </summary>
		/// <param name="defaultCursor">Default cursor.</param>
		/// <param name="defaultRolloverCursor">Default rollover cursor.</param>

		void ChangeDefaultCursor (int defaultCursor, int defaultRolloverCursor);

	}
}
