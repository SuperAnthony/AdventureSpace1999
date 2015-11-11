#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - February 21, 2015
#endregion 

#region using
using UnityEngine;
using System;
using System.Collections;
using WhatPumpkin.Dialogue;
using WhatPumpkin.CameraManagement;
using PixelCrushers.DialogueSystem;
#endregion

namespace WhatPumpkin.Dialogue {

	public class ConversationControl : MonoBehaviour, IConversationController {

		#region static members

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The singleton instance of the controller.</value>

		static public ConversationControl Instance { get; private set; }

		/// <summary>
		/// Occurs when a conversation starts.
		/// </summary>

		static public event EventHandler ConversationStart;

		#endregion

		#region properties

		/// <summary>
		/// Gets the conversation camera.
		/// </summary>
		/// <value>The conversation camera.</value>

		public CameraNode ConversationCamera { get; private set;}

		/// <summary>
		/// The available conversations in the scene.
		/// </summary>

		[SerializeField] Conversation [] _conversations;

		#endregion

		#region methods


		public void Start() {
		
			Instance = this;
		
		}

		public void Talk(string convoKey) {
		
			Conversation conversation = Entities.Entity.FindObjectByKey<Conversation> (convoKey, _conversations);

			// Start the conversation
			if (conversation != null) {conversation.Talk ();}
		
		}

		/// <summary>
		/// Starts the conversation.
		/// </summary>
		/// <param name="convo">Convo.</param>
		/// <param name="newCamNode">New cam node.</param>
		
		public void StartConversation(string convo, CameraNode newCamNode) {
			// Start the conversation
			DialogueManager.StartConversation (convo);
			

			// Set the conversation camera
			ConversationCamera = newCamNode; 

			// Broadcast that this conversation has started
			BroadcastConversationStart ();

		}

		/// <summary>
		/// Broadcasts the conversation start.
		/// </summary>

		void BroadcastConversationStart() {

			Debug.Log ("Broadcast Start");
			if (ConversationStart != null) {
								ConversationStart.Invoke (this, null);
						}
		}

		#endregion
	}
}
