#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
#endregion 

#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;
using WhatPumpkin.ScriptingLanguage;
using WhatPumpkin.Actions.Sequences;
using WhatPumpkin.Sgrid.Environment;
using WhatPumpkin.Entities;

#if UNITY_EDITOR
using UnityEditor;
#endif


#endregion

namespace WhatPumpkin.Sgrid.Triggers {



	/// <summary>
	/// Hot spot event arguments.
	/// </summary>

	public class HotSpotEventArgs : EventArgs {

		#region fields

		/// <summary>
		/// The hot spot that is refered.
		/// </summary>

		HotSpot _hotSpot;

		/// <summary>
		/// The type of the _hot spot event.
		/// </summary>

		MouseEventType _mouseEventType; 

		/// <summary>
		/// The name of the hotspot
		/// </summary>

		string _name;

		bool _conditionsMet; 

		#endregion


		#region properties

		/// <summary>
		/// Gets the type of the hot spot event.
		/// </summary>
		/// <value>The type of the hot spot event.</value>

		public MouseEventType MouseEventType { get { return _mouseEventType; } }

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>

		public string Name { get { return _name; } }

		/// <summary>
		/// Gets the hot spot.
		/// </summary>
		/// <value>The hot spot.</value>

		public HotSpot HotSpot { get { return _hotSpot; } }

		/// <summary>
		/// Gets a value indicating whether this <see cref="WhatPumpkin.Sgrid.Triggers.HotSpotEventArgs"/> conditions met.
		/// </summary>
		/// <value><c>true</c> if conditions met; otherwise, <c>false</c>.</value>

		public bool ConditionsMet { get { return _conditionsMet; } }

		#endregion

		#region methods


		/// <summary>
		/// Initializes a new instance of the <see cref="WhatPumpkin.Sgrid.Triggers.HotSpotEventArgs"/> class.
		/// </summary>
		/// <param name="hsEventType">Hs event type.</param>
		/// <param name="name">Name.</param>
	
		public HotSpotEventArgs(MouseEventType hsEventType, HotSpot hotspot, string name, bool conditionsMet) {
		
			_mouseEventType = hsEventType;

			_hotSpot = hotspot;

			_name = name;

			_conditionsMet = conditionsMet;

		}

		#endregion

	}

	public class HotSpot : Entity, IHotSpot, ICombineable, IOpenVerbCoin {

		#region static constants and events

		// TODO: I'd like this to live in the hotspot controller after other more important refactoring is complete

		const int REQUIRED_SEQUENCES = 6;

		const string DEFAULT_ROLLOVER_MESSAGE = "";

		static public event EventHandler<HotSpotEventArgs> HotSpotEvent;

		#endregion

		#region static properties

		static public IHotSpotController Controller { get; set; }

		#endregion

		#region fields

		/// <summary>
		/// The conditions.
		/// </summary>

		[SerializeField] string _conditions;  

		/// <summary>
		/// The _sequences.
		/// </summary>

		[SerializeField] VerbActionSequence[] _sequences = new VerbActionSequence[REQUIRED_SEQUENCES]  ;

		/// <summary>
		/// The closeup cam that this hotspot is associated with "" means main cam.
		/// </summary>

		[SerializeField] string _closeupCam;

		/// <summary>
		/// The required items (by key) in the active player's inventory to perform this action sequence.
		/// </summary>
		
		[SerializeField] protected string _requiredItems;

		/// <summary>
		/// The cursor icon.
		/// </summary>

		[SerializeField] protected Cursor _cursor;

		/// <summary>
		/// The room associated with this hotspot.
		/// </summary>

		[SerializeField] protected Room _room;

		/// <summary>
		/// Is this a once click hotspot (meaning no verb coin gets opened when selected)
		/// </summary>

		[SerializeField] bool _isOneClick = false;

		#endregion


		#region properties

		/// <summary>
		/// Gets a value indicating whether this instance has required cam.
		/// </summary>
		/// <value><c>true</c> if this instance has required cam; otherwise, <c>false</c>.</value>

		// TODO: This logic looks completely backwards to me

		public bool HasRequiredCam { get { return _closeupCam == "" || _closeupCam == null; } }

		/// <summary>
		/// Gets a list of required items keys.
		/// </summary>
		/// <value>The required items.</value>
		
		public List<string> RequiredItems { get { return Scripting.GetArguments (_requiredItems); } }

		/// <summary>
		/// Gets the required cam name.
		/// </summary>
		/// <value>The required cam.</value>

		public string RequiredCamName { get { return _closeupCam; } }

		/// <summary>
		/// Gets the sequences.
		/// </summary>
		/// <value>The sequences.</value>

		public VerbActionSequence [] Sequences { get { return _sequences; } }

		/// <summary>
		/// Gets the verb sequences.
		/// </summary>
		/// <value>The verb sequences.</value>

		public VerbActionSequence [] VerbActionSequences { get { return Sequences; } }

		/// <summary>
		/// Gets the room.
		/// </summary>
		/// <value>The room.</value>

		public Room Room { 
		
			get { return _room; } 

			#if UNITY_EDITOR

			set { _room = value; }
		
			#endif
		} 

		/// <summary>
		/// Gets the conditions of the hotspot.
		/// </summary>
		/// <value>The conditions.</value>

		public string Conditions { get { return _conditions; } }


		public event ItemKeyEvent SendItem;

		/// <summary>
		/// Is this hotspot live?
		/// </summary>

		public bool IsLive { get { return HotSpotConditionsMet (); } }

		#endregion

		/// <summary>
		/// The _event system.
		/// </summary>

		static EventSystem _eventSystem;


		#region implement IEnable

		/// <summary>
		/// Enable this instance.
		/// </summary>
		/// 
		public void Enable() {
		
			this.gameObject.SetActive (true);

		}

		/// <summary>
		/// Disable this instance.
		/// </summary>

		public void Disable() {
			this.gameObject.SetActive (false);
		}


		#endregion

		#region monobehaviours


		void Awake() {
		


		}
		// Use this for initialization
		protected override void Start() {
		
			base.Start ();

			if (_eventSystem == null) {
				_eventSystem = GameObject.FindObjectOfType<EventSystem>();
			}

			GameController.CombineItemController.AddToCombineList (this);

		}

		/// <summary>
		/// Raises the mouse enter event.
		/// </summary>
		
		public void MouseEnter() {


			if(HotSpotConditionsMet()) {

				//Debug.Log ("Conditions Met");
			
				// Handlecursor rollover
				GameController.CursorControl.HandleRollover(_cursor);

				// Show the hint message
				//GameController.MessageManager.ShowHotSpotRollover (this.EntityInfo.Name);

				if(HotSpotEvent != null) {
					HotSpotEvent(this, new HotSpotEventArgs(MouseEventType.MOUSE_ENTER, this, this.Name, true));
				}
			}
			else {

				//Debug.Log("Conditions Not Met");

				if(HotSpotEvent != null) {
					HotSpotEvent(this, new HotSpotEventArgs(MouseEventType.MOUSE_ENTER, this, this.Name, false));
				}

				// If Conditions are not met then use the default cursor
				if (GameController.CursorControl != null) {
					GameController.CursorControl.UseDefaultCursor ();
				}

			}
		}
		
		/// <summary>
		/// Raises the mouse exit event.
		/// </summary>
		
		public void MouseExit() {

			HotSpotEvent(this, new HotSpotEventArgs(MouseEventType.MOUSE_EXIT, this, this.Name, HotSpotConditionsMet()));

			// On mouse out return to the default cursor
			if (GameController.CursorControl != null) {
				GameController.CursorControl.UseDefaultCursor ();
			}
			
		}

		public void MouseDown() {
		
			HotSpotEvent(this, new HotSpotEventArgs(MouseEventType.MOUSE_DOWN, this, this.Name, HotSpotConditionsMet()));
		
		}


		/// <summary>
		/// Occurs on mouse up
		/// </summary>
		
		public void MouseUp() {


			HotSpotEvent(this, new HotSpotEventArgs(MouseEventType.MOUSE_UP, this, this.Name, HotSpotConditionsMet()));


			// Display verb coin if conditions are met
			if( HotSpotConditionsMet() && !GameController.CombineItemController.IsUseMode ) { // Verb coin panel conditions
					
					// Select this HotSpot
					Select();
					// Open the verb coin panel with this hot spot's sequences
					if(_sequences != null && _sequences.Length > 0) {
						
						if(_isOneClick == true) {
							_sequences[0].Play();
						}
						else {

							// TODO: Move this to hotspot controller
							GameController.ActionControl.OpenVerbCoinPanel(_sequences, this.transform);
						}
						
					}
				}


			// Send item for use mode
			if (SendItem != null && !_eventSystem.IsPointerOverGameObject ()) {
				SendItem (Key); 
			}

		}

		/// <summary>
		/// Raises the destroy event.
		/// </summary>

		void OnDestroy() {

			if (GameController.CombineItemController != null) {GameController.CombineItemController.RemoveFromCombineList (this);}

			SendItem = null;
		
		}

		#endregion

		/// <summary>
		/// Receives the data.
		/// </summary>
		/// <returns><c>true</c>, if data was received, <c>false</c> otherwise.</returns>
		/// <param name="hotspotDataCollection">Hotspot data collection.</param>
/*
		public bool ReceiveData(HotSpotData [] hotspotDataCollection, VerbActionSequence [] VAS) {
		
#if UNITY_EDITOR
			EditorUtility.SetDirty (this);
			EditorUtility.SetDirty (this.gameObject);
			EditorUtility.SetDirty (this.GetComponent<EntityInfo>());
#endif

			// Debug.Log ("Receive Data Collection: " + hotspotDataCollection.Length);

			// Get the correct key name
			string key = this.Key;
			// If we don't find any key name, one wasn't set up, try to use the name in the game object instead but send a warning
			if(this.Key == ""){
				key = gameObject.name;
				Debug.Log("Hot spot did not have a key name, using the game object name '" + this.gameObject.name + "' instead. " +
				          "Please set up key names for hotspots to prevent unforseen conflicts.");
			}

			//Debug.Log ("Key: " + key);
			HotSpotData hotSpotData = Entity.FindObjectByKey<HotSpotData> (key, hotspotDataCollection);
			//Debug.Log ("hotSpotData: " + hotSpotData.Key);

			if (hotSpotData != null) {
			
				return ReceiveData(hotSpotData, VAS);
			
			}

			return false;
		}

		/// <summary>
		/// Receives the data.
		/// </summary>
		/// <returns><c>true</c>, if data was received, <c>false</c> otherwise.</returns>
		/// <param name="hotSpotData">Hot spot data.</param>

		bool ReceiveData(HotSpotData hotSpotData, VerbActionSequence [] VAS) {
		
			//Debug.Log ("Receiving Data 2");
			//EditorUtility.SetDirty (this);
			//EditorUtility.SetDirty (this.gameObject);


			// Allocate space for _sequences if there isn't any
			if(_sequences.Length == 0){_sequences = new VerbActionSequence[REQUIRED_SEQUENCES];}

		
			// Send Sequence data
			for (int i = 0; i < hotSpotData.Sequences.Count; i++) {
				_sequences[i] = Entity.FindObjectByKey<VerbActionSequence>(hotSpotData.Sequences[i], VAS); 

			}
		
			// Send the remaining information
			_name = hotSpotData.Name;
			_closeupCam = hotSpotData.CloseupCam;
		
			return true;
		}*/

	
		/// <summary>
		/// Are the hot spot conditions met.
		/// </summary>
		/// <returns><c>true</c>, if hot spot conditions were met, <c>false</c> otherwise.</returns>

		bool HotSpotConditionsMet() {


			// Make sure the verb coin is not open
			if (_eventSystem != null) {
								if (_eventSystem.IsPointerOverGameObject ())
										return false;
						}
			else {

			}


				return 
					
				// TODO: It seems like I'm checking certain conditions twice
				!GameController.ActionControl.IsSequencePlaying &&
				// Scripting conditions
				Scripting.AreConditionsMet (_conditions) &&

				// Game Conditions
				(Controller == null || (Controller.AreHotSpotsActive && Controller.AreHotSpotConditionsMet(this)) ); 
		}

		/// <summary>
		/// Validates the sequences. Makes certain that all sequences are valid.
		/// If there is a null entry then a new sequence will be created.
		/// </summary>

		public void ValidateSequences() {

			// Make sure the sequences have the required length
			if (_sequences.Length < REQUIRED_SEQUENCES) {
				_sequences = DataUtilities.ResizeArray<VerbActionSequence>(_sequences, REQUIRED_SEQUENCES);
			}

			for(int i = 0; i < REQUIRED_SEQUENCES; i++) {
			
				if(_sequences[i] == null) {
					_sequences[i] = new VerbActionSequence();
				}

			}
		}

		// Editor Methods
		[ExecuteInEditMode]
		void OnDrawGizmos() {
		
			Gizmos.matrix = this.transform.localToWorldMatrix;
			Gizmos.color = new Color(1, .5F, .5F, .8F);
			Gizmos.DrawWireCube (Vector3.zero, Vector3.one);

			Gizmos.matrix = this.transform.localToWorldMatrix;
			Gizmos.color = new Color(1, .4F, .4F, .6F);
			Gizmos.DrawCube (Vector3.zero, Vector3.one);

		}

		/// <summary>
		/// Sets the key. Meant to be used by the editor in the inspector view and no place else.
		/// </summary>
		/// <param name="key">Key.</param>
		public void SetKey(string key) {
			// Raise the rename key event if necessary and rename the key
			OnRenameKey (key);
		}

		/// <summary>
		/// Sets the properties. This is meant to only be used in the Editor Mode.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="name">Name.</param>

		public void SetProperties(string key, string name, string conditions = "") {

#if UNITY_EDITOR
			EditorUtility.SetDirty (this);
#endif


			/*
			EditorUtility.SetDirty (this);
			EditorUtility.SetDirty (this.gameObject);
			EditorUtility.SetDirty (this.GetComponent<EntityInfo>());
			*/
			// Rename The Key
			SetKey(key);
			_name = name;
			_conditions = conditions;
			// Set this value as "dirty" to allow undo 
			//EditorUtility.SetDirty (this);

		
		}

		/// <summary>
		/// Autofill this instance using the key provided for reference.
		/// </summary>

		public void Autofill() {

			_name = Key.Replace("HS_", "HST_"); // TODO: This does not appear to be working?

			// Try to find the room based on the name
			Room r = Autofills.FindGOComponentByPrefix<Room>(Key, "HS_", "RM_",'_',3);

			if (r != null) {_room = r;}

			// Set the second action sequence to automatically have a look action
			if(_sequences.Length > 0) {
				_sequences[0].SetProperties(Key.Replace ("HS_","VAS_"),"","VERB_LOOK");

				string lookKey = Key.Replace("HS_", "NAR_");
				lookKey = Autofills.InsertStringAfterKeyPrefix(lookKey, "LOOK_", '_', 3);

				_sequences[0].AddAction("SetNar", lookKey, "", "");
			}
		
		}

	
	}



}
