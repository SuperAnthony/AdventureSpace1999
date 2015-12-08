#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - November 13, 2014
#endregion 

#region using
using System;
using UnityEngine;
using WhatPumpkin.Screens; // TODO: Spaghetti - I should not need this
#endregion

namespace WhatPumpkin.CameraManagement {

	/// <summary>
	/// Camera Node switch arguments.
	/// </summary>
	
	public class CamSwitchArgs : EventArgs {
	
		CameraNode _previousActiveCamera;

		CameraNode _newActiveCamera;
		
		public CameraNode NewActiveCamera { get { return _newActiveCamera; } }

		public CameraNode PrevActiveCamera { get { return _previousActiveCamera; } }

		public CamSwitchArgs (CameraNode newActiveCamera, CameraNode previousActiveCamera) {
			
			_newActiveCamera = newActiveCamera;
			_previousActiveCamera = previousActiveCamera;
		}
		
	}


	/// <summary>
	/// Camera Controller. Mainly used for switching between camera nodes.
	/// </summary>

	public class CameraController : MonoBehaviour {

		#region events

		public event EventHandler<CamSwitchArgs> SwitchCameraNode;

		#endregion

		#region fields
	
		/// <summary>
		/// The Active Camera. Use the ActiveCamera property.
		/// </summary>
		Camera _activeCamera; 

		[SerializeField] Camera _mainCamera;

		/// <summary>
		/// The _active camera node.
		/// </summary>
		[SerializeField] CameraNode _activeCameraNode; // What node is the camera set to

		/// <summary>
		/// Camera to return to.
		/// </summary>

		CameraNode _returnCamera;

		#endregion

		#region static properties

		/// <summary>
		/// Gets or sets the active camera manager.
		/// </summary>
		/// <value>The active camera manager.</value>

		public static CameraController Instance { get; private set;} 


		#endregion

		#region properties

		/// <summary>
		/// Gets the active camera.
		/// </summary>
		/// <value>The active camera.</value>

		public Camera ActiveCamera {
						get { 
			
							// If an active camera exists then return it
							if(_activeCamera != null) {
								return _activeCamera;
							} // Otherwise, set the active camera to the main camera and return it
							else {
								_activeCamera = GetMainCamera();
								return _activeCamera;
							}

						}
				}

		// Instance Properties

		public CameraNode ActiveCameraNode { 

			// TODO: Setter should be internal

			set { 

				CameraNode oldCameraNode = _activeCameraNode;
				// Set the active camera node
				_activeCameraNode = value; 
				// Set the active camera to the camera node's propeties and transforms
				SetCameraPropetiesToActiveNode(ActiveCamera);
				
				// Raise the camera switch event
				if(SwitchCameraNode != null) {
				
					CamSwitchArgs camSwitchArg = new CamSwitchArgs(value, oldCameraNode); 

					SwitchCameraNode(this.gameObject, camSwitchArg);

				}
			} 
			get {
				return _activeCameraNode;
			}
		} 



		// Static Methods

		/// <summary>
		/// Creates the camera manager. This is done so that a level mapper does not to create an instance of it in a scene.
		/// Should be called by the game manager
		/// </summary>
	
		public static void CreateCameraManager(bool overrideCurrent = false) {



			// Does a camera manager already exist? or are we overriding the current one anyway (no reccomended)
			if (Instance == null || overrideCurrent == true) {
				// If conditions are true then create camera manager
				// First create an empty game object
				GameObject camManage = new GameObject();
				// Attach the camera manager component to it
				camManage.AddComponent<CameraController>();
				// This should be done at the start of the new instance once the component is attached
				// but just in case we'll set the active manager
				Instance = camManage.GetComponent<CameraController>();
			}

		}

		#endregion

		#region methods

		// Use this for initialization
		public void Start () {
            
			// Set the active camera manager to this on start
			if(Instance == null){Instance = this;}

			// Check to see if the active camera is null, if so then set it
			if(_activeCamera == null){_activeCamera = GetMainCamera();}
		
			// If a particular camera node is designated as the active camera node then set the active camera to it
			TransformCamToActiveNode (ActiveCamera);


		}


		/// <summary>
		/// Sets the camera propeties to active node.
		/// </summary>
		/// <param name="cam">Cam.</param>
		/// <param name="setTransform">If set to <c>true</c> set transform.</param>

		void SetCameraPropetiesToActiveNode(Camera cam, bool setTransform = true)
		{

			if (cam != null && _activeCameraNode != null && _activeCameraNode.GetComponent<Camera>() != null) {
				// Set camera properties
				cam.fieldOfView = _activeCameraNode.GetComponent<Camera>().fieldOfView;
				cam.backgroundColor = _activeCameraNode.GetComponent<Camera>().backgroundColor;

				// Set the transform
				if(setTransform) {TransformCamToActiveNode(cam);}
			}
		}

		/// <summary>
		/// Transforms the cam to the active node's transform info.
		/// </summary>

		void TransformCamToActiveNode(Camera cam) {

			if (cam != null && _activeCameraNode != null) {
								cam.transform.position = _activeCameraNode.transform.position;
								cam.transform.rotation = _activeCameraNode.transform.rotation;


				Camera activeCamComponent = _activeCameraNode.GetComponent<Camera>();


				if(activeCamComponent != null) {
					cam.fieldOfView = activeCamComponent.fieldOfView;
					cam.backgroundColor = activeCamComponent.backgroundColor;
				}
			} 
			else {
				Debug.LogWarning("Could not locate an active camera or active camera node." +
					"Make sure you have a MainCamera and that your active camera node is set up in the camera manager");
			}

		}


		/// <summary>
		/// Gets the main camera in the scene.
		/// </summary>
		/// <returns>The main camera.</returns>

		public Camera GetMainCamera() {


			if (_mainCamera != null) {
				return _mainCamera;
			}

			Camera cam;
			try {
				// Look for an object that is tagged as the Main Camera and try to get it's camera component
				cam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
			}
			catch {
				return null;			
			}

			// Make certain the object exists
			if (cam != null) {
				return cam;
			}

			Debug.LogError ("Did not find an object tagged as Main Camera. Returning first object Unity finds with a Camera script attached... not good");

			return GameObject.FindObjectOfType<Camera>();
		}

		/// <summary>
		/// Updates the camera position.
		/// </summary>

		void UpdateCameraPosition() {

			//Debug.Log ("Active Camera Node: " + _activeCameraNode.transform.position);

			if(_activeCameraNode != null) {

				GetMainCamera().transform.position = new Vector3(_activeCameraNode.transform.position.x,
				                                                 _activeCameraNode.transform.position.y,
				                                                 _activeCameraNode.transform.position.z);


				GetMainCamera().transform.position = _activeCameraNode.transform.position;

				GetMainCamera().transform.rotation = new Quaternion (_activeCameraNode.transform.rotation.x,
				                                                     _activeCameraNode.transform.rotation.y,
				                                                     _activeCameraNode.transform.rotation.z,
				                                                     _activeCameraNode.transform.rotation.w);

				GetMainCamera().transform.rotation = _activeCameraNode.transform.rotation;

			}

		
		}
        


		void UpdatePlayerTracker() {

			_activeCameraNode.SetActivePlayerTrackerPosition (GameController.PartyManager.ActivePC.transform.position);
		}

		// Update is called once per frame
		void Update () {

			// Constantly update the position of the camera.
			UpdateCameraPosition ();
            
		}


		/// <summary>
		/// Ends the current closeup.
		/// </summary>

		public void EndCloseup() {
		
			//GameController.GameStateMachine.EndCloseup ();
		}


		/// <summary>
		/// Starts a closeup.
		/// </summary>
		/// <param name="cameraName">Camera name.</param>
		
		public void StartCloseup(string cameraName) {
			
			
			// Change the game state
			// TODO: This should all be handled by the camera controller (this)
			if(GameController.GameStateMachine != null) {
				GameController.GameStateMachine.ChangeGameState (GameController.GameStateMachine.OnCloseup, ActiveCameraNode);
			}

			CameraNode newCamNode = null;
			if(GameController.SceneManager != null) {
				newCamNode = GameController.SceneManager.FindEntity (cameraName).GetComponent<CameraNode> ();
			}
			
			
			if (newCamNode != null) {
				ActiveCameraNode = newCamNode;
			}

			// Disable the room hotspots
			try {
				// TODO: Clean this up a bit
				this.GetComponent<IHotSpotController>().DisableRoomHotSpots (); // TODO: Let the event manager know when a closeup has started then perform this
			}
			catch {
			
				Debug.LogError("Failed to disable room hotspots, this is most likely because the hot spot controller is not attached to the game controller");
			
			}

			// Show the closeup screen
			//GameController.ScreenManager.OpenScreen ("Closeup Screen");


			
			
		}
		#endregion
	}

}
