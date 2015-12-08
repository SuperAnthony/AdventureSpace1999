#region Copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - April 16, 2015
#endregion

using System;

namespace WhatPumpkin.Sgrid.Characters {

	/// <summary>
	/// IParty member.
	/// </summary>

	public interface IPartyMember  {

		#region events 

		/// <summary>
		/// Occurs when joined party.
		/// </summary>
		
		event Action JoinedParty; 

		/// <summary>
		/// Occurs when made unavailable from party.
		/// </summary>

		event Action MadeUnavailableFromParty;

		/// <summary>
		/// Occurs when made available to party.
		/// </summary>
		
		event Action MadeAvailableToParty;

		#endregion

		#region methods

		/// <summary>
		/// Joins the party.
		/// </summary>

		void JoinParty();

		/// <summary>
		/// Makes this character available to the party.
		/// </summary>

		void MakeAvailable();

		/// <summary>
		/// Make ths character unavailable.
		/// </summary>

		void MakeUnavailable();

		
		#endregion

		#region properties

		/// <summary>
		/// Gets a value indicating whether this instance is an available party member.
		/// </summary>
		/// <value><c>true</c> if this instance is available party member; otherwise, <c>false</c>.</value>

		bool IsAvailablePartyMember { get ;  }


		#endregion

	}
}
