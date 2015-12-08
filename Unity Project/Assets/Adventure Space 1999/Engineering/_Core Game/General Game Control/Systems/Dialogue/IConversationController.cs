#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - February 21, 2014
#endregion 

#region using
using WhatPumpkin.CameraManagement;
#endregion

namespace WhatPumpkin.Dialogue {

	public interface IConversationController {

		/// <summary>
		/// Starts the conversation.
		/// </summary>
		/// <param name="convo">Convo.</param>
		/// <param name="newCamNode">New cam node.</param>

		void StartConversation(string convo, CameraNode newCamNode);

	}
}