#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 18, 2015
#endregion 

#region using
using System;
#endregion

namespace WhatPumpkin {

	public enum MessageType {DEFAULT, BARK, NARRATOR, ROLLOVER, MULTI_PROMPT}

	/// <summary>
	/// Interface for message managers. 
	/// </summary>

	public interface IMessageManager<TKey> : INarratorMeessageManager<TKey>, IBarkMessageManager<TKey>, IHotsSpotMessageManager<TKey> {

		/// <summary>
		/// Occurs when narrative message stopped.
		/// </summary>

		event EventHandler NarrativeMessageStopped;

		/// <summary>
		/// Gets the message.
		/// </summary>
		/// <returns>The message.</returns>
		/// <param name="key">Key.</param>


		string GetMessage(TKey key);

		/// <summary>
		/// Opens the multi option message.
		/// </summary>
		/// <param name="messageKey">Message key.</param>
		/// <param name="optionKeys">Option keys.</param>

		// TODO: It's possible to cut out the middle man

		void OpenMultiOptionMessage(TKey messageKey, TKey [] optionKeys);

	}

	/// <summary>
	/// Message event arguments.
	/// </summary>

	public class MessageEventArgs : EventArgs {
	
		MessageType _messageType;
		string _message;
		string [] _options;

		public MessageType MessageType { get { return _messageType; } }
		public string Message { get { return _message; } }
		public string [] Options { get { return _options; } }

		public MessageEventArgs(string message, string [] options = null, MessageType messageType = MessageType.DEFAULT) {
		
			_message = message;
			_options = options;
			_messageType = messageType;

		}

	}



}
