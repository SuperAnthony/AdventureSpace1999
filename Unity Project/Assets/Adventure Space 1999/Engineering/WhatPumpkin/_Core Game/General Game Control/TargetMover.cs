using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using WhatPumpkin.Actions.UI;
using WhatPumpkin.Sgrid.Markers;

// TODOs
// The instance of the target mover should probably live in the input component
// Do a little bit of restructuring
// Remove the target stuff from the event manager
// Consider changing this name to Ground

namespace WhatPumpkin {
	

	public class TargetMover : MonoBehaviour {



		Transform _target;
		AIPath[] ais2;
		Camera cam;
		EventSystem _eventSystem;

		#region WhatPumpkin

		// For constraints

		[SerializeField] Transform _forceAxisTransform = null;

		[SerializeField] bool _forcedXAxis = false;

		[SerializeField] bool _forcedYAxis = false;

		[SerializeField] bool _forcedZAxis = false;


		/// <summary>
		/// Is the mouse button down
		/// </summary>

		bool _isMouseDown = false;



		static public TargetMover Instance { get; private set; } 

		/// <summary>
		/// The game object name of the target that gets moved around
		/// </summary>

		const string TARGET_NAME = "Target";

		/// <summary>
		/// The cursor that the icon changes to when rolled over
		/// </summary>


		const string CURSOR_CHANGE = "CURSOR_WALK";

		public Transform Target {
		
			get {
				if(_target != null) {
					return _target;
				}

				_target = GameObject.Find(TARGET_NAME).transform;;

				return _target;
			}
		
		}


		/// <summary>
		/// Raises the mouse enter event.
		/// </summary>

		public void OnMouseEnter() {

			// Show roll over cursor icon when the mouse enters
			//GameController.CursorControl.SetCursor (CURSOR_CHANGE);

		}

		/// <summary>
		/// Raises the mouse exit event.
		/// </summary>

		public void OnMouseExit() {
		
			// Set the cursor back to default on exit
			//GameController.CursorControl.UseDefaultCursor ();

		}

		public void Awake() {
			Instance = this;
		}

		public void Start () {

		
			_eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

			//Cache the Main Camera
			cam = Camera.main;
			ais2 = FindObjectsOfType(typeof(AIPath)) as AIPath[];
		}


		public void SetTargetPosition(Vector3 position) {

			Target.transform.position = position;

		}

		void AdjustTarget() {

			// Don't adjust the target if an action sequence is playing
			if (GameController.ActionControl.IsSequencePlaying) {
				return;			
			}

			
			// Retreive from the input manager
			//_target = GameObject.Find(TARGET_NAME).transform; // TODO: Place target in the input manager
			_target = GameController.InputManager.Target.transform;
			
			//			Debug.Log (_target.name);
			
			if (_eventSystem.IsPointerOverGameObject())
				return;
			
			
			// Make sure the verb coin is not open
			if(VerbCoinPanel.IsOpen == false 
			   && GameController.GameStateMachine.CanExplore 
			   // && !GameController.ActionControl.IsSequencePlaying // TODO: Player can't move while an action sequence is playing | Bring this back
			   ) { // TODO: More eloquent solution to this conditional
				
				
				// Get a target if there isn't any
				if(_target == null){
					_target = GameObject.Find("Target").transform; 
				}
				
				//Physics.Raycast(cam.ScreenPointToRay (Input.mousePosition), out WheelHit, Mathf.Infinity,
				
				//Fire a ray through the scene at the mouse position and place the target where it hits
				RaycastHit hit;
				if (Physics.Raycast	(cam.ScreenPointToRay (Input.mousePosition), out hit, Mathf.Infinity) && hit.point != Target.position) {

					_target.position = hit.point;
				
					
					// Apply forced contraint 
					if(_forceAxisTransform) {
						ApplyForcedAxis();
					}

					if (ais2 != null) {
						for (int i=0;i<ais2.Length;i++) {
							if (ais2[i] != null) ais2[i].SearchPath ();
						}
					}
				}


				// Let the input manager know that the target was moved
				GameController.InputManager.ReceiveTargetEvent(this.gameObject);
			}
			else {
				//		Debug.Log ("Can't explore");
				//		Debug.Log (VerbCoinPanel.IsOpen);
			}
		
		}

		/// <summary>
		/// Applies the forced axis.
		/// </summary>

		void ApplyForcedAxis() {
		
			// If there is no target then break
			if(_target == null) {return;}

			// Store the transform information of the target
			float x = _target.transform.position.x;
			float y = _target.transform.position.y;
			float z = _target.transform.position.z;

			// Get the forced axis position
			if (_forcedXAxis) {x = _forceAxisTransform.position.x;}
			if (_forcedYAxis) {y = _forceAxisTransform.position.y;}
			if (_forcedZAxis) {z = _forceAxisTransform.position.z;}

			// Apply
			_target.position = new Vector3 (x, y, z);

		}


		void OnMouseDown () {

			// This does not execute if we are hovering over a hotspot that is live
			if (GameController.HotSpotController.IsHoveringOverHotSpot 
			    /*&& !GameController.HotSpotController.HoveringHotspot.IsLive*/) {
				return;			
			}

			_isMouseDown = true;
			AdjustTarget ();

		}

		void OnMouseUp () {
			_isMouseDown = false;
		}

		void Update() {
		
			if (_isMouseDown) {
				AdjustTarget();			
			}

		}

		#endregion

		// Editor

		// Editor Methods
		[ExecuteInEditMode]
		void OnDrawGizmos() {
			
			Gizmos.matrix = this.transform.localToWorldMatrix;
			Gizmos.color = new Color(1F, 1F, 0F, .8F);
			Gizmos.DrawWireCube (Vector3.zero, Vector3.one);
			
			Gizmos.matrix = this.transform.localToWorldMatrix;
			Gizmos.color = new Color(.8F, .8F, 0F, .6F);
			Gizmos.DrawCube (Vector3.zero, Vector3.one);
			
		}
	}


}
