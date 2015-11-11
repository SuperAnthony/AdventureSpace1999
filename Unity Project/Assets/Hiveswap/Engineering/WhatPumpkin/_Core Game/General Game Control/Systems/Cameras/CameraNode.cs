#region Copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - November 13, 2014
#endregion

#region using
using UnityEngine;
using System.Collections;
using WhatPumpkin.Entities;

using WhatPumpkin.Sgrid.Environment;
#endregion

namespace WhatPumpkin.CameraManagement {

	/// <summary>
	/// Camera node - designates transform information about a camera. The camera manager can move the main camera to this node
	/// This may also include information about animation sequences in time as well
	/// In general - Camera nodes are used to designate a place for where a camera should be.
	/// </summary>

	//[RequireComponent(typeof(Camera))]

	public class CameraNode : Entity, IActivatable {

		#region fields

		/// <summary>
		/// The active player tracker node.
		/// </summary>

		[SerializeField] GameObject _activePlayerTracker;

		/// <summary>
		/// The local X offset.
		/// </summary>

		[SerializeField] float _localXOffset = 0F;

		/// <summary>
		/// The _local offset.
		/// </summary>

		[SerializeField] Vector3 _localOffset = new Vector3 (0F, 0F, 0F);

		/// <summary>
		/// The is this a closeup cam.
		/// </summary>

		[SerializeField] bool _isCloseupCam;

		/// <summary>
		/// Activate this room
		/// </summary>

		[SerializeField] Room _room;

		#endregion

		#region properties

		/// <summary>
		/// Gets a value indicating whether this instance is closeup cam.
		/// </summary>
		/// <value><c>true</c> if this instance is closeup cam; otherwise, <c>false</c>.</value>

		public bool IsCloseupCam { get { return _isCloseupCam; } }

		/// <summary>
		/// Gets the active player tracker.
		/// This node tracks the player's position and is needed for rails
		/// </summary>
		/// <value>The active player tracker.</value>
		public GameObject ActivePlayerTracker { get { return _activePlayerTracker; } }

		/// <summary>
		/// Gets the local X offset.
		/// </summary>
		/// <value>The local X offset.</value>

		public float LocalXOffset { get { return _localXOffset; } }


		/// <summary>
		/// Gets the local offset.
		/// </summary>
		/// <value>The local offset.</value>

		public Vector3 LocalOffset { get { return _localOffset; } }

		#endregion

		#region methods

		public override void Activate() {

//			Debug.Log ("Activate");

			if (_isCloseupCam) {
			
				// If this is a closeup cam then handle it as such
				GameController.CamManager.StartCloseup(this.Key);
			}
			else {

				// If this is not a closeup cam then just switch
				if(GameController.CamManager.ActiveCameraNode != this) {
					GameController.CamManager.ActiveCameraNode = this;
				}
			}

			//Debug.Log ("Attempt To Set Parent");

			// Child the main camera to this node
			//GameController.CamManager.GetMainCamera ().transform.SetParent (this.transform);

			// Activate a room if necessary
			if (_room != null && !_room.IsActive) {
				//Debug.Log ("Room Activation");
				_room.Activate();
			
			}
		
		}


		static public CameraNode GetFromScene(string key) {
		
			return Entities.Entity.FindObjectByKey<CameraNode> (key, GameObject.FindObjectsOfType<CameraNode> ());
		
		}

		// Use this for initialization
		void Start () {

		
		}

		void Awake() {

			// Check to see if the object has a camera attached and then destroy the camera
			// TODO: It's possible I will not want to destroy the camera
			
			Camera cam = this.GetComponent<Camera> ();
			if (cam != null) {
				cam.enabled = false;
			}

		}


		/// <summary>
		/// Sets the camera node properties.
		/// </summary>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>

		static public void SetCamNodeProperties(Transform from, Transform to) {
		
			from.transform.position = to.transform.position;
			from.transform.rotation = to.transform.rotation;

		}

		/// <summary>
		/// Sets the active player tracker position.
		/// </summary>
		/// <param name="pos">Position.</param>

		public void SetActivePlayerTrackerPosition(Vector3 pos) {
		
			if(_activePlayerTracker != null) {
				_activePlayerTracker.transform.position = new Vector3(pos.x, pos.y, pos.z);
			}
		}


		// Update is called once per frame
		void Update () {

		}

		[ExecuteInEditMode]

		void OnDrawGizmos() {

			Gizmos.color = Color.cyan;
		
		}

		#endregion



		[ExecuteInEditMode]

		public void SetSgridProperties(string key, bool isCloseupCam) {

			#if UNITY_EDITOR
			UnityEditor.EditorUtility.SetDirty (this);
			#endif
			
			// Rename this Camera Node
			OnRenameKey (key);

			// Is this a closeup cam?
			_isCloseupCam = isCloseupCam;
			
		}

		[ExecuteInEditMode]

		public void SetSgridProperties(string key, bool isCloseupCam, float fov, float nearClipPlane, float farClipPlane) {
		
			SetSgridProperties (key, isCloseupCam);

			Camera unityCam = this.GetComponent<Camera> ();

			if (unityCam != null) {
			
				unityCam.fieldOfView = fov;
				unityCam.nearClipPlane = nearClipPlane;
				unityCam.farClipPlane = farClipPlane;
			
			}



		}


	}

}