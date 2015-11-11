#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - November 19, 2014
#endregion 

#region using
using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using WhatPumpkin.CameraManagement;
using WhatPumpkin.Sgrid.Characters;
#endregion

namespace WhatPumpkin.Dialogue {

	/// <summary>
	/// A Conversation.
	/// </summary>

	[System.Serializable]

	public class Conversation : Keyed, ITalk, IKeyed, IConversation, IPlayable {

		#region fields

		static IConversationController _conversationController;

		/// <summary>
		/// The active conversation.
		/// </summary>

		static Conversation _activeConversation;

		/// <summary>
		/// The _key.
		/// </summary>

		//[SerializeField] string _key;


		/// <summary>
		/// The conversation that will get played.
		/// </summary>

		[SerializeField] string _conversation;

		/// <summary>
		/// The key name of the camera node that the camera controller will reference.
		/// </summary>

		[SerializeField] string _cameraControllerKey;

		/// <summary>
		/// The active camera node of the conversation. 
		/// This is the camera node that the main camera will be positioned at
		/// </summary>

		[SerializeField] CameraNode _cameraController;

		/// <summary>
		/// The _camera nodes.
		/// </summary>

		[SerializeField] Transform [] _cameraAngles; 


		/// <summary>
		/// Will the camera lerp to it's position?
		/// </summary>

		//[SerializeField] bool _lerpCamToPosition = false;


		/// <summary>
		/// The npc animation that will play by default. | TODO: This may not be necessary at all and will be done in the sequences
		/// </summary>

		[SerializeField] string _defaultNPCAnimation; 

		/// <summary>
		/// The player character.
		/// </summary>

		[SerializeField] PlayerCharacter _playerCharacter;

		/// <summary>
		/// The _default PC animation.
		/// </summary>

		[SerializeField] string _defaultPCAnimation;

		/// <summary>
		/// The force player angle.
		/// </summary>

		[SerializeField] Transform _forcePlayerAngle;



		#endregion

		#region properties 

		static public Conversation ActiveConversation { get { return _activeConversation; } }


		static public IConversationController ConversationController { 
			get {

				// If the conversation controller is null locate the conversation controller
				if(_conversationController == null) {
				
						_conversationController =  (IConversationController)ConversationControl.Instance;

					if(_conversationController == null) {
						// If it's still null, then we can't locate one - attach one to this game object
				
						// Create an empty game object and attach a conversation controller to it
						GameObject gObject = new GameObject();

						// Add the conversation controller 
						_conversationController  = (IConversationController)gObject.AddComponent<ConversationControl>();
					
					}
				}

				// Now we should certainly have a conversation controller of some kind, return it
				return _conversationController;


			}

			set {
				_conversationController = value;
			}
		}

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>

		public override string Key { get { return _key; } }

		/// <summary>
		/// Gets the camera controller.
		/// </summary>
		/// <value>The camera controller.</value>

		public CameraNode CameraController { get { return _cameraController; } }

		#endregion

		#region methods

		public void Play() {

			Talk ();
		}

		/// <summary>
		/// Plays the default animations.
		/// </summary>

		void PlayDefaultAnimations() {
			// Change animation
			/*
			Animator animator = this.GetComponent<Animator> ();
			if(animator != null) {
				Debug.Log("Attempt to play: " + _defaultNPCAnimation);
				animator.Play (_defaultNPCAnimation);
			}
			
			animator = null;	

			animator = _playerCharacter.AnimatedCharacter.GetComponent<Animator> ();
			if(animator != null) {
				animator.Play (_defaultPCAnimation);
			}*/
		}


		/// <summary>
		/// Switches the camera node.
		/// </summary>
		/// <returns><c>true</c>, if camera node was switched, <c>false</c> otherwise.</returns>
		/// <param name="index">Index.</param>

		public bool SwitchCameraNode(int index) {

			if (index < _cameraAngles.Length) {
				CameraNode.SetCamNodeProperties(_cameraController.transform, _cameraAngles[index]);
				// Camera was switched
				return true;
			}

			// The correct node was not found
			Debug.LogWarning ("Camera node not found.");
			return false;

		}

		/// <summary>
		/// Switches the camera node.
		/// </summary>
		/// <param name="name">Name.</param>

		public bool SwitchCameraNode(string name) {

			// Search for camera node of the same name and switch to it
			foreach (Transform cN in _cameraAngles) {

				if(cN.name == name) {
					// Switch camera
					CameraNode.SetCamNodeProperties(_cameraController.transform, cN.transform);
					// Camera was switched
					return true;
				}
			}

			// The correct node was not found so nothing was switched, return false
			Debug.LogWarning ("Camera node not found.");
			return false;
		}




		/// <summary>
		/// Talk to this instance. // TODO: Rename?
		/// </summary>

		public void Talk() {

			if (_cameraController == null) {
			

				_cameraController = CameraNode.GetFromScene(_cameraControllerKey);

				// If no camera was found then use the active one
				if(_cameraController == null) {
				
					_cameraController = GameController.CamManager.ActiveCameraNode;

				}


			}





			// Make sure there is a camera controller
			// Then tell the game controller that we're going to start a conversation
			if(_cameraController != null) {
				ConversationController.StartConversation (_conversation, _cameraController);
			}
			else {
				// There was no camera controller found and a conversation was not initiated
				Debug.LogError("Camera Controller was not found. Conversation was not started. Did you make sure to " +
				               "add a camera node to the conversation in the inspector?");
			}

			/*
			if (_forcePlayerAngle != null) {
				Debug.Log ("Player Angle Forced");
				// TODO: this isn't working
				_playerCharacter.transform.rotation = _forcePlayerAngle.transform.rotation;
			}*/
			
			// Set the active conversation
			_activeConversation = this;
			
			PlayDefaultAnimations ();
		}

		#endregion
	}
}