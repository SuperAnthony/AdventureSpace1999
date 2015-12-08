#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - April 21, 2015
#endregion

#region using
using UnityEngine;
using System;
using System.Collections.Generic;
using WhatPumpkin.Sgrid;
using WhatPumpkin.CameraManagement;  
using WhatPumpkin.Actions.Sequences;
using WhatPumpkin.Sgrid.Environment;
#endregion

namespace WhatPumpkin.Sgrid.Markers { 
	
	/// <summary>
	/// Spawn Point. Where a character spawns on load - we can associate NPCs and Followers with this spawn point
	/// </summary>
	
	
	public class SpawnPoint : Marker, IKeyed, ISpawnPoint, IActivatable {
		
		#region static fields
		
		/// <summary>
		/// Occurs when an instance of spawn point activated.
		/// </summary>
		
		static public event EventHandler SpawnPointActivated;
		
		/// <summary>
		/// The _scene spawn points.
		/// </summary>
		
		static private SpawnPoint [] _sceneSpawnPoints;
		
		#endregion
		
		
		#region fields
		
		/// <summary>
		/// Spawn the active PC at this spawn point when activated
		/// </summary>
		
		[SerializeField] bool _spawnActivePC = true;

		/// <summary>
		/// The camera node that will be activated when this spawn point is initiated
		/// </summary>
		
		[SerializeField] CameraNode _cameraNode;
		
		/// <summary>
		/// The spawning characters.
		/// </summary>
		
		[SerializeField] List<Transform> _spawnables;
		
		/// <summary>
		/// The action sequence that gets played when this spawn point is activated.
		/// </summary>
		
		[SerializeField] ActionSequence  _actionSequence;
		
		/// <summary>
		/// The child spawn points.
		/// </summary>
		
		[SerializeField] SpawnPoint [] _childSpawnPoints;
		
		/// <summary>
		/// The room associated with this spawn point.
		/// </summary>
		
		[SerializeField] Room _room;
		
		#endregion
		
		#region properties
		
		/// <summary>
		/// Gets a value indicating whether the active pc gets spawned here
		/// </summary>
		/// <value><c>true</c> if spawn active P; otherwise, <c>false</c>.</value>
		
		public bool SpawnActivePC { get { return _spawnActivePC; } }
		
		/// <summary>
		/// Gets the camera node.
		/// </summary>
		/// <value>The camera node.</value>
		
		public CameraNode CameraNode { get { return _cameraNode; } }
		
		/// <summary>
		/// Gets the room associated with this spawn point.
		/// </summary>
		/// <value>The room (Oh Hai Denny).</value>
		
		public Room Room { get { return _room; } }

		/// <summary>
		/// Gets the action sequnce. This is the sequence that plays when the spawn point is activated.
		/// </summary>
		/// <value>The action sequnce.</value>

		public ActionSequence ActionSequnce { get { return _actionSequence; } }
		
		#endregion
		
		
		#region methods
		
		/// <summary>
		/// Finds the spawn point.
		/// </summary>
		/// <returns>The spawn point.</returns>
		/// <param name="key">Key.</param>
		
		static public SpawnPoint FindSpawnPoint(string key) {
			_sceneSpawnPoints = GameObject.FindObjectsOfType<SpawnPoint> ();
			SpawnPoint spawnPoint = Entity.FindObjectByKey<SpawnPoint> (key, _sceneSpawnPoints);
			
			return spawnPoint;
		}
		
		/// <summary>
		/// Activates the spawn point.
		/// </summary>
		/// <param name="spawnPoint">Spawn point.</param>
		
		static public void ActivateSpawnPoint(SpawnPoint spawnPoint) {
			
			spawnPoint.Activate ();
			
		}
		
		/// <summary>
		/// Updates the scene spawn points.
		/// </summary>
		
		static public void UpdateSceneSpawnPoints() {
			// Setting this to null makes me feel good
			_sceneSpawnPoints = null;
			// Find all spawn points in the current sceen
			_sceneSpawnPoints = GameObject.FindObjectsOfType<SpawnPoint> ();
			
		}

		/// <summary>
		/// Activate this instance.
		/// </summary>

		public override void Activate() {

            Spawn();


        }

		/// <summary>
		/// Handles the camera fade end.
		/// </summary>

		

		void Spawn(bool playActionSequence = true) {

			// Disable the active player
			if (GameController.PartyManager != null && GameController.PartyManager.ActivePC != null) {
						GameController.PartyManager.ActivePC.GetComponent<IAIPath> ().Disable ();
				}
			// Spawn all the spawnable characters at this spawnpoint
			
			// Spawn all the spawnables related to this spawn point 
			SpawnActors ();
			
			// Play action sequence
			if (playActionSequence && _actionSequence != null) {
				PlayActionSequence();
			}
			
			// Raise spawn point activated event
			if (SpawnPointActivated != null) {SpawnPointActivated.Invoke (this, null);}


			// Set to the correct camera node
			if (_cameraNode != null) {
				_cameraNode.Activate();
			}
			
			// Re set the move target
			try {

				TargetMover.Instance.SetTargetPosition (this.transform.position);
			}
			catch {
				Debug.LogWarning("Could not re set the target mover while spawning");
			}
			
			// Get the PC's AI Path and disable it
			//GameController.ActivePlayerCharacter.GetComponent<AIPath>().Disable();
			
			// Activate the associated room if ther is one
			if (_room != null) {_room.Activate();}

			if (GameController.PartyManager != null && GameController.PartyManager.ActivePC != null) {
						GameController.PartyManager.ActivePC.GetComponent<IAIPath> ().Enable ();
						
						if(_affectCharacterRotation) {
							GameController.PartyManager.ActivePC.transform.rotation = this.transform.rotation;
						}
				}
		}
        
		
		/// <summary>
		/// Spawns the actors.
		/// </summary>
		
		void SpawnActors() {
			
			foreach (Transform tf in _spawnables) {
				
				ISpawnable spawnable = tf.GetComponent(typeof(ISpawnable)) as ISpawnable;
				spawnable.Spawn(this);
			}
		}
		
		/// <summary>
		/// Plays the action sequence.
		/// </summary>
		
		void PlayActionSequence() {
			_actionSequence.Play ();
		}
		
		/// <summary>
		/// Activates the child spawn points.
		/// </summary>
		
		void ActivateChildSpawnPoints() {
			// Invoke child spawn points
			foreach (SpawnPoint spawnPoint in _childSpawnPoints) {
				
				// Activate the child spawn point but do not play it's action sequences
				// TODO: Be certain this is the effect you want
				if(spawnPoint != null){spawnPoint.Activate();}
				
			}
		}
		
		/// <summary>
		/// Raises the draw gizmos event.
		/// </summary>
		[ExecuteInEditMode]
		void OnDrawGizmos() {

			// TODO: Remove the hard coding

			Gizmos.DrawIcon(transform.position, "SpawnPointIcon.png", true);
		}
		
		
		#endregion



		/// <summary>
		/// Sets the properties. This will only happen in the editor.
		/// </summary>

		public void SetProperties(string key, bool doesSpawnPC, CameraNode cameraNode, Room room) {

			#if UNITY_EDITOR
			UnityEditor.EditorUtility.SetDirty(this);
			#endif
			//_key = key;
			OnRenameKey (key);
			_spawnActivePC = doesSpawnPC;
			_cameraNode = cameraNode;
			_room = room;
		}


		
		
	}
}
