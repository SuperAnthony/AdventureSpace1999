#region using
using System.Collections;
#endregion

namespace WhatPumpkin.Dialogue {

	public interface IConversation : IKeyed {

		/// <summary>
		/// Play this instance.
		/// </summary>

		void Play();

	}
}
