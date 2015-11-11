#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - February 13, 2015
#endregion 

#region using
using System;
using UnityEngine;
#endregion

namespace WhatPumpkin {	

	/// <summary>
	/// Hot spot event type.
	/// </summary>
	
	public enum HotSpotEventType {MOUSE_DOWN, MOUSE_OUT, MOUSE_OVER};
	
	/// <summary>
	/// Hot spot event arguments.
	/// </summary>

	// TODO: Utilize this after other major refactoring

	public class HotSpotEventArgs : EventArgs {

		/// <summary>
		/// The type of hot spot event.
		/// </summary>

		HotSpotEventType _hotSpotEventType;

		/// <summary>
		/// The message, if there is one.
		/// </summary>

		string _message;

		public HotSpotEventType HotSpotEventType { get { return _hotSpotEventType; } }



		public string Message { get { return _message; } }

		
		public HotSpotEventArgs(HotSpotEventType eventType, string message = "") {

			_hotSpotEventType = eventType;
			_message = message;
			
		}
		
	}

	public interface IHotSpotController  {

		#region properties

		/// <summary>
		/// Gets the required number sequences.
		/// </summary>
		/// <value>The required number of sequences.</value>
		
		int RequiredSequences { get; } 
		
		/// <summary>
		/// Gets the default rollover message.
		/// </summary>
		/// <value>The default rollover message.</value>
		
		string DefaultRolloverMessage { get ; }

		#endregion

		#region methods

		/// <summary>
		/// Gets a value indicating whether this <see cref="WhatPumpkin.Entities.Triggers.IHotSpotController"/> are hot spots active.
		/// </summary>
		/// <value><c>true</c> if are hot spots active; otherwise, <c>false</c>.</value>

		bool AreHotSpotsActive { get; }

		/// <summary>
		/// Are the hot spot conditions met.
		/// </summary>
		/// <returns><c>true</c>, if hot spot conditions were met, <c>false</c> otherwise.</returns>
		/// <param name="hotspot">Hotspot.</param>

		bool AreHotSpotConditionsMet(IHotSpot hotspot);

		/// <summary>
		/// Disables the room hot spots.
		/// </summary>

		void DisableRoomHotSpots();

		/// <summary>
		/// Enales the room hot spots.
		/// </summary>

		void EnaleRoomHotSpots();

		
		/// <summary>
		/// Gets a value indicating whether or not the user is hovering over a hotspot
		/// </summary>

		bool IsHoveringOverHotSpot { get; }

		/// <summary>
		/// Gets the hotspot the the user is hovering over
		/// </summary>
		/// <value>The hovering hotspot.</value>

		IHotSpot HoveringHotspot { get; }

		#endregion

	}
}
