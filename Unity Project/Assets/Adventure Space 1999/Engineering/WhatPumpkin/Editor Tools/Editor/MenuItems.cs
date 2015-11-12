#region using
using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using WhatPumpkin.CameraManagement;
using WhatPumpkin.Sgrid.Triggers;
using WhatPumpkin.CutScenes;
using WhatPumpkin;
using WhatPumpkin.Entities;
using WhatPumpkin.Actions.Sequences;
using WhatPumpkin.Sgrid.Markers;
using WhatPumpkin.Sgrid.Environment;

using WellFired;
using WhatPumpkin.Sound;

#endregion

public class MenuItems : MonoBehaviour {
	

	#region isolation mode

	/// <summary>
	/// Is  the editor in isolation mode.
	/// </summary>
	
	static bool _isIsolationMode = false;
	
	/// <summary>
	/// Is the editor in isolate canvas mode.
	/// </summary>
	
	static bool _isIsolateCanvasMode = false;
	
	/// <summary>
	/// The disabled canvases when in isolate canvas mode.
	/// </summary>
	
	static List<Canvas> _disabledCanvases = new List<Canvas>();

	
	[MenuItem("HiveSwap/View/Isolate Canvas &c") ]
	
	/// <summary>
	/// Trigger isolate canvas mode or end isolate canvas mode when the user selects this item.
	/// </summary>
	
	static  void IsolateCanvasMode() {
		
		if (_isIsolateCanvasMode == false) {
			StartIsolateCanvasMode();
		}
		else {
			EndIsolateCanvasMode();
		}
		
		
	}

	[MenuItem("HiveSwap/View/Isolation Mode &i") ]
	
	/// <summary>
	/// Trigger Isolation mode or end isolation mode when the user selects this item
	/// </summary>
	
	static public void IsolationMode() {
		
		
		if (_isIsolationMode) {
			EndIsolationMode ();
		}
		else {
			StartIsolationMode();
		}
		
		
	}

	/// <summary>
	/// Starts the isolate canvas mode.
	/// </summary>
	
	
	static void StartIsolateCanvasMode() {
		
		_disabledCanvases.Clear ();
		
		Canvas canvas = null;
		
		foreach (GameObject gObject in GameObject.FindObjectsOfType(typeof(GameObject))) {
			
			canvas = gObject.GetComponent<Canvas>(); 
			
			if(canvas!=null){
				canvas.enabled = false;
				_disabledCanvases.Add(canvas);
			}
		}
		
		
		canvas = Selection.activeGameObject.GetComponent<Canvas>(); 
		
		if (canvas != null) {
			canvas.enabled = true;
		}
		
		_isIsolateCanvasMode = true;
		
	}
	
	/// <summary>
	/// Ends the isolate canvas mode.
	/// </summary>
	
	static void EndIsolateCanvasMode() {
		
		foreach (Canvas canvas in _disabledCanvases) {
			
			
			//canvas = gObject.GetComponent<Canvas>(); 
			
			if(canvas!=null){canvas.enabled = true;}
			
			
		}
		
		_disabledCanvases.Clear ();
		_isIsolateCanvasMode = false;
		
	}
	
	
	/// <summary>
	/// Starts the isolation mode.
	/// </summary>
	
	static void StartIsolationMode() {
		
		
		foreach (GameObject gObject in GameObject.FindObjectsOfType(typeof(GameObject))) {
			
			if(gObject.GetComponent<Renderer>() != null) {
				gObject.GetComponent<Renderer>().enabled = false;
			}
		}
		
		if (Selection.activeGameObject.GetComponent<Renderer>() != null) {
			Selection.activeGameObject.GetComponent<Renderer>().enabled = true;
		}
		
		_isIsolationMode = true;
	}
	
	/// <summary>
	/// Ends the isolation mode.
	/// </summary>
	
	static void EndIsolationMode() {
		
		
		foreach (GameObject gObject in GameObject.FindObjectsOfType(typeof(GameObject))) {
			
			if(gObject.GetComponent<Renderer>() != null) {
				gObject.GetComponent<Renderer>().enabled = true;
			}
		}
		
		_isIsolationMode = false;
		
		
	}
	


	#endregion

	#region create

	/// <summary>
	/// Creates the object and attaches the propert component.
	/// </summary>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	
	private static T CreateObject<T>(string name, string parentName = "no parent", bool tagParentAsCollection = true) where T : Component {
		
		// Create a HiveSwap Scene If Required
		// AddRequiredObjectsToScene ();
		
		// Create a game object
		GameObject gObject = new GameObject ();
		
		// Name the object
		gObject.name = name;
		
		// Attach the components
		gObject.AddComponent<T> ();
		
		
		// Set Parent
		SetParent (gObject, parentName, tagParentAsCollection);
		
		// Select the object
		UnityEditor.Selection.activeGameObject = gObject;
		
		
		
		// Retrun the component
		return gObject.GetComponent<T> ();
		
	}
	
	
	[MenuItem("HiveSwap/Create/Create Hot Spot %h")]
	
	
	/// <summary>
	/// Creates the hot spot.
	/// </summary>
	
	public static void CreateHotSpot() {
		
		// Create the hot spot
		HotSpot hotspot = CreateObject<HotSpot> ("HotSpot");
		// Setup Hotspot
		SetupActionSequenceTrigger (hotspot.gameObject);
		
	}
	
	
	[MenuItem("HiveSwap/Create/Create Trigger %t")]
	
	/// <summary>
	/// Creates the trigger.
	/// </summary>
	
	private static void CreateTrigger() {
		
		// Create trigger
		TriggerActionSequence trigger = CreateObject<TriggerActionSequence> ("Actions Trigger");
		// Setup Trigger
		SetupActionSequenceTrigger (trigger.gameObject);
	}
	
	[MenuItem("HiveSwap/Create/Create Move To Target Node %m")]
	
	/// <summary>
	/// Creates the move to target and add it to our scene collection.
	/// </summary>
	
	private static void CreateMoveToTarget() {
		
		CreateObject<Target> ("Move Target", "Entities", true);
		
	}
	
	[MenuItem("HiveSwap/Create/Create Spawn Point %w")]
	
	public static void CreateSpawn()  {
		SpawnPoint spawn = CreateObject<SpawnPoint> ("SPWN_1");
		spawn.transform.parent = GameObject.FindObjectOfType<ReferencedSceneObjectCollection>().transform;
	}
	
	
	
	
	/// <summary>
	/// Creates the camera trigger.
	/// </summary>
	
	[MenuItem("HiveSwap/Create/Camera Trigger &a")]
	private static void CreateCameraTrigger() {
		
		CameraTrigger camTrigger = CreateObject<CameraTrigger> ("Camera Trigger");	
		
		// TODO: Let the camera trigger handle this logic
		
		camTrigger.GetComponent<Collider>().isTrigger = true; 
		camTrigger.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
		
	}


    [MenuItem("HiveSwap/Create/Positional Sound &g")]
    private static void CreatePositionalSound()
    {
        CreateObject<SFXGO>("Game Audio");
    }

    #endregion

    #region attach


    [MenuItem("HiveSwap/Attach Component/Action sequence Trigger")]
	
	/// <summary>
	/// Attachs the trigger action sequence component.
	/// </summary>
	
	private static void AttachTriggerActionSequenceComponent() {AttachComponent<TriggerActionSequence> ();}
	
	[MenuItem("HiveSwap/Attach Component/Cut Scene Actor")]
	
	/// <summary>
	/// Attachs the actor component.
	/// </summary>
	
	private static void AttachActorComponent() {AttachComponent<Actor> ();}
	
	
	[MenuItem("HiveSwap/Attach Component/Look At")]
	
	/// <summary>
	/// Attachs the actor component.
	/// </summary>
	
	private static void AttachLookAtComponent() {AttachComponent<LookAt> ();}

	/// <summary>
	/// Attachs the specified component.
	/// </summary>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	
	private static void AttachComponent<T>() where T : Component {
		
		// Get the selected object
		GameObject selectedObject = Selection.activeGameObject;
		
		
		// Attach the use component to the selected object if there is a selected object
		// and the object  does not already have the component attached
		// Is an object selected
		if(selectedObject != null) {
			
			// Check to see if a use component is already attached
			T component = selectedObject.GetComponent<T>();
			
			//T t = selectedObject.GetComponent(typeof(T));
			
			if(component == null)
			{
				// Attach the component
				selectedObject.AddComponent<T> ();
			}
			else
			{
				// Let the developer know a component is already attached
				Debug.LogWarning("Selected component is already attached to this object.");
			}
		}
		else {
			Debug.LogWarning("No object is selected to attach this component.");
		}
		
	}

	[MenuItem("HiveSwap/Make Entity &e")]
	static public void MakeEntity() {
		
		Selection.activeGameObject.AddComponent<Entity> ();
		
	}
	
	
	
	
	[MenuItem("HiveSwap/Attach Component/Room &r") ]
	
	
	static void AttachRoomComponent() {
		
		AttachComponent<Room> ();
		
	}

	#endregion

	#region cameras



	[MenuItem("HiveSwap/Create/Camera/Camera Node")]
	private static void CreateCameraNode() {
		CameraNode gO;
		gO = CreateObject<CameraNode> ("Camera Node");	
		// Attach a camera to the camera node for previewing in the editor
		gO.gameObject.AddComponent<Camera> ();
	}

	[MenuItem("HiveSwap/Create/Camera/Simple Camera Rail")]
	public static void CreateSimpleCameraRail() {
		
		
		// Create a new game object
		GameObject gO = new GameObject ();
		
		// Call it camera rail
		gO.name = "Simple Camera Rail";
		
		// Attach the Camera node component
		gO.AddComponent<CameraNode> ();
		
		// Attach a component that follows the player
		gO.AddComponent<FollowPlayer> ();
		
	}

	/// <summary>
	/// Creates a camera rail.
	/// </summary>

	[MenuItem("HiveSwap/Create/Camera/Camera Rail")]
	public static void CreateCameraRail() {
		
		
		
		// Create a game object to attach the camera rail component to
		GameObject go = new GameObject ("Camera Rail");
		
		go.AddComponent<CameraPath> ();
		go.AddComponent<CameraPathAnimator> ();
		go.AddComponent<CameraPlayerTracker>();
		

	}


	#endregion



	[MenuItem("HiveSwap/Create/Cut Scene Tester &t") ]

	public static CutSceneTester CreateCutSceneTester() {
		CutSceneTester gO;
		gO = CreateObject<CutSceneTester>("CutSceneTester");

		return gO;
	}

	/*

	[MenuItem("HiveSwap/Create HS Scene &s")]

	static void CreateHSScene() {
	
		GameObject instance = (GameObject)Instantiate(Resources.Load("Prefabs/HS Scene Test", typeof(GameObject)));

		instance = (GameObject)Instantiate(Resources.Load("Prefabs/AStar Stuff", typeof(GameObject)));
	
	}*/

	/// <summary>
	/// Setups the action sequence trigger.
	/// </summary>
	/// <param name="trigger">Trigger.</param>

	private static void SetupActionSequenceTrigger(GameObject trigger) {
	
		// Attach a bounding box
		// TODO: Triggers should attach bounding boxes automatically
		BoxCollider boxCollider = trigger.gameObject.AddComponent<BoxCollider> ();
		// Set the boxCollider to a trigger
		boxCollider.isTrigger = true;
	}

	



	static void SetParent(GameObject gObject, string parentName, bool tagAsCollection = true) {
		// Create a parent if necessary and set a child object
		if (parentName != "no parent") {
			
			// Check to see if the parent exists
			GameObject parent = GameObject.Find(parentName);
			
			// If the parent doesn't exist then create it
			if(parent == null) {
				parent = new GameObject(parentName);
			}
			
			// Set the game objects parent
			gObject.transform.parent = parent.transform;
			
			if(tagAsCollection) {
				parent.tag = "Collection";
			}
			
		} // End Set Parent name
	
	}


	
}
