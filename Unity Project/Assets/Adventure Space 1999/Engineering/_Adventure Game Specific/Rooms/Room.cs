#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - March 16, 2015
#endregion 

#region using
using UnityEngine;
using System;
using WhatPumpkin.Sgrid;
#endregion

namespace WhatPumpkin.Sgrid.Environment {

	public class RoomEventArgs : EventArgs {
	
		/// <summary>
		/// The _thumbnail key.
		/// </summary>

		string _thumbnailKey;

		/// <summary>
		/// Gets the thumbnail key.
		/// </summary>
		/// <value>The thumbnail key.</value>

		public string ThumbnailKey { get { return _thumbnailKey; } }

		/// <summary>
		/// Initializes a new instance of the <see cref="WhatPumpkin.Sgrid.Environment.RoomEventArgs"/> class.
		/// </summary>

		public RoomEventArgs(string thumbnailKey) {

			_thumbnailKey = thumbnailKey;

		}
	
	}

	/// <summary>
	/// Room. Rooms will contiain information about their lights and environments, they will get turned on and off when players spawn.
	/// Spawn points will contain information about rooms that get turned on and off. 
	/// </summary>
	
	public class Room : Entity, ISwitchable {

		const string FADE_TYPE = "ROOM_FADE";

		#region static fields

		/// <summary>
		/// The active room.
		/// </summary>

		static Room _activeRoom;

		/// <summary>
		/// The rooms in the scene.
		/// </summary>

		static Room [] _rooms;


		static public event EventHandler<RoomEventArgs> RoomActivated;


		#endregion

		#region fields

		/// <summary>
		/// The room thumbnail highlighted.
		/// </summary>

		[SerializeField] string _thumbnailHighlightedKey;

		/// <summary>
		/// The thumbnail key when unhighlighted.
		/// </summary>

		[SerializeField] string _thumbnailUnhighlightedKey;

		#endregion

		#region field lighting and fog
	


		/// <summary>
		/// The fog settings.
		/// </summary>

		[SerializeField] Fog _fogSettings;

		#endregion


		#region field environments

		[SerializeField] TimeOfDayCondition [] _conditionalObjects;
	
		#endregion

		#region properties

		/// <summary>
		/// Gets the active room.
		/// </summary>
		/// <value>The active room.</value>

		static public Room ActiveRoom { get { return _activeRoom; } }

		/// <summary>
		/// Gets a value indicating whether this instance is active.
		/// </summary>
		/// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>

		public override bool IsActive { get { return _activeRoom == this; } }

		/// <summary>
		/// Gets the fog settings for this room.
		/// </summary>
		/// <value>The fog settings.</value>

		public Fog FogSettings { get { return _fogSettings; } }

		/// <summary>
		/// Gets the thumbnail image key name.
		/// </summary>
		/// <value>The thumbnail image.</value>

		public string ThumbnailHighlightedKey { get { return _thumbnailHighlightedKey; } }

		/// <summary>
		/// Gets the thumbnail key unhighlighted.
		/// </summary>
		/// <value>The thumbnail key unhighlighted.</value>

		public string ThumbnailUnhighlightedKey { get { return _thumbnailUnhighlightedKey; } }


		#endregion

		#region room activation methods

		protected override void Start() {
		
			foreach (TimeOfDayCondition conditional in _conditionalObjects) {
			
				conditional.Init();

			}
		
		}

		/// <summary>
		/// Inits the rooms.
		/// </summary>

		static public void InitRooms() {
		
			CollectRooms ();

		}

		/// <summary>
		/// Handles the scene start.
		/// </summary>
	
		static void HandleSceneStart() {
		
			CollectRooms ();

		}

		/// <summary>
		/// Add all rooms in the scene to the rooms collection
		/// </summary>


		static void CollectRooms() {
			_rooms = GameObject.FindObjectsOfType<Room> ();

		}

		/// <summary>
		/// Deactives all rooms in the scene.
		/// </summary>

		static void DeactiveRooms() {
			foreach (Room room in _rooms) {
				room.Disable();
			}
		}


		/// <summary>
		/// Activate this instance.
		/// </summary>

		public override void Activate() {

	//		Debug.Log ("Activate Room: " + this.Key);

			// Disable the active room
			//if (_activeRoom != null) {_activeRoom.Deactivate ();}
			// Enable this object
			Enable ();
			// Set the active room to this
			_activeRoom = this;
			// Apply the fog settings of this room

			_fogSettings.ApplyFog ();

			// Attempt to activate conditional object - most of these will be lighting conditions
			UpdateConditionalObjects ();

			foreach (Room room in GameObject.FindObjectsOfType<Room>()) {
				if(room != this) {
					room.Deactivate();
				}
			
			}
         

			// Raised the room activated event

			if (RoomActivated != null) {
			
				RoomActivated.Invoke(this, new RoomEventArgs("blank"));
			
			}
		}

		/// <summary>
		/// Updates the conditional objects.
		/// </summary>

		public void UpdateConditionalObjects() {
			foreach (ConditionalGameObject gO in _conditionalObjects) {			
				gO.Update();			
			}

		}

		/// <summary>
		/// Deactivate this instance.
		/// </summary>

		public override void Deactivate() {

//			Debug.Log ("Deactivating Room: " + this.Key);

			Disable ();
			if (this.IsActive) {
								_activeRoom = null;
						}
			
		}

		#endregion

		#region methods

		void Awake() {

			// TODO: This may be temp
			Keyed.AddKey ((IKeyed)this);
		
		}

		#endregion


		[ExecuteInEditMode]

		public void SetProperties(string key) {

#if UNITY_EDITOR
			UnityEditor.EditorUtility.SetDirty (this);
#endif

			OnRenameKey (key);
		
		}

		[ExecuteInEditMode]

		public void SetFogSettings(bool enabled, Color color, float density, float startDistance, float endDistance, FogMode fogMode) {
		
#if UNITY_EDITOR
			UnityEditor.EditorUtility.SetDirty (this);
#endif

			_fogSettings.SetProperties (enabled, color, density, startDistance, endDistance, fogMode);
		
		}

		
	}
}
