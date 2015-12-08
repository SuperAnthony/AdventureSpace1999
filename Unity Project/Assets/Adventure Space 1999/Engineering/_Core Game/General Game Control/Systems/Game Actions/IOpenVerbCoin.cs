#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - July 16, 2015
#endregion 

#region using
using WhatPumpkin.Actions.Sequences;
#endregion

namespace WhatPumpkin {

	/// <summary>
	/// For objects that open verb coins
	/// </summary>

	public interface IOpenVerbCoin  {

		/// <summary>
		/// Gets the verb action sequence.
		/// </summary>
		/// <value>The verb action sequence.</value>

		 VerbActionSequence [] VerbActionSequences { get; }

	}
}