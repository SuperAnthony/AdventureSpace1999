#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 29, 2015
#endregion 

#region using
//using WhatPumpkin.Entities;
#endregion


namespace WhatPumpkin {
	/// <summary>
	/// Gives items to receivers.
	/// </summary>

	public interface IGiver  {

		/// <summary>
		/// Take an item from this object and send it to the receiver
		/// </summary>
		/// <param name="sendItemKey">Key of item being sent.</param>
		/// <param name="receiverKey">Key of receiver object.</param>

		void Give( string sendItemKey, string receiverKey);


	}
}
