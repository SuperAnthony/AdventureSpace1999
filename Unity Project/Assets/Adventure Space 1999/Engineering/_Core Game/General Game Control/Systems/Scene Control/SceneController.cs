#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - October, 2014
#endregion 

#region using
using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

using WhatPumpkin.Sgrid;
#endregion

namespace WhatPumpkin { 

	/// <summary>
	/// Scene Controller for managing scenes. 
	/// </summary>

	public class SceneController : MonoBehaviour,  ISceneManager, ICutSceneManager {



		/// <summary>
		/// The singleton instance of the scene manager.
		/// </summary>

		static SceneController _instance;

		/// <summary>
		/// Gets the active scene manager.
		/// </summary>
		/// <value>The active scene manager.</value>

		
		static public SceneController Instance { get { return _instance; } }

	
		/// <summary>
		/// The current scene.
		/// </summary>

		SceneInfo _currentScene;

		/// <summary>
		/// The load screen scene name.
		/// </summary>

		[SerializeField] string _loadScreenScene;

		/// <summary>
		/// The name of the object the active character will spawn at 
		/// </summary>

		[SerializeField] string _activeCharSpawn = "";

		/// <summary>
		/// The collection of information about scenes.
		/// </summary>

		[SerializeField] List<SceneInfo> _sceneInfos = new List<SceneInfo> ();


		/// <summary>
		/// The current scene
		/// </summary>

		SceneInfo _sceneInfo; 

	
		/// <summary>
		/// The async opperation for loading.
		/// </summary>
		AsyncOperation async;

		/// <summary>
		/// Occurs when scene load starts.
		/// </summary>

		public event EventHandler SceneLoadStart;

		/// <summary>
		/// Occurs when the scene has completed loading
		/// </summary>

		public event EventHandler SceneLoadEnd;

		/// <summary>
		/// Occurs when scene data loaded.
		/// </summary>

		public event Action SceneDataLoaded;

		/// <summary>
		/// The previous scene.
		/// </summary>

		string _previousScene;

		/// <summary>
		/// The scene entities.
		/// </summary>

		Dictionary<string, Entity> _sceneEntities = new Dictionary<string, Entity>(); // TODO: This is somethign I want to remove

		/// <summary>
		/// The disabled entities list.
		/// </summary>

		List<string> _disabledEntitiesList = new List<string>();

		/// <summary>
		/// Gets the previous scene.
		/// </summary>
		/// <value>The previous scene.</value>
		public string PreviousScene { get { return _previousScene; } }

		/// <summary>
		/// Gets the current scene.
		/// </summary>
		/// <value>The current scene.</value>
		public string CurrentScene { get { return Application.loadedLevelName; } }

		/// <summary>
		/// Gets the scene infos.
		/// </summary>
		/// <value>The scene infos.</value>

		public List<SceneInfo> SceneInfos { get { return _sceneInfos; } }


		public void ReceiveSceneInfo(SceneInfo sceneInfo) {

			// TODO: Reset _sceneInfos when loading | TODO: Create a reset or exit game event for this

			// Then add
			_sceneInfos.Add (sceneInfo);

		}


	
		/// <summary>
		/// Begins to loads the scene.
		/// </summary>
		/// <param name="scene">Scene.</param>

		public void LoadScene(string scene, string spawnPointName = "", string preloaderScene = "") {

			// Clear the key list before loading a new scene. This is to take care of some unity issues
			Keyed.ClearLists ();
				 
			// First, store the name of the previous scene in case we want to return right back to it
			_previousScene = Application.loadedLevelName;


			// Set the new spawn point
			_activeCharSpawn = spawnPointName;
	

			//Application.LoadLevel (scene);
			if(SceneLoadStart!=null){SceneLoadStart.Invoke (this, null);}

			// Check to see if there is a preloader scene
			if(preloaderScene == "" || preloaderScene == null) {
				// Go to the scene loader and load the new scene
				_loadScreenScene = preloaderScene;
				// Without preloader
				StartCoroutine (LoadSceneWithoutLoadScreen (scene));
			}
			else {
				// With preloader
				StartCoroutine (LoadSceneWithLoadScreen (scene));
			}

			// Set the current scene. This is mainly to track object status
//			SetCurrentScene (scene);
		
		}


		/// <summary>
		/// Adds scene to currently loaded scene.
		/// </summary>
		/// <returns>The scene.</returns>
		/// <param name="scene">Scene.</param>

		IEnumerator AddScene(string scene) {
			
			

			// TODO:
			
			
			yield return true;
		}


		IEnumerator LoadSceneWithoutLoadScreen(string scene) {
			
			//Load our scene async
			async = Application.LoadLevelAsync (scene);
			async.allowSceneActivation = true; // False if I want it to hold
			yield return async;

			
			// Load Scene Data
			/*
			if (_loadXMLDataOnLoad) {

				LoadSceneData (Application.dataPath + _dataPath + scene + "/");
			}*/
			
	
			
			// Invoke Scene Load End
			if (SceneLoadEnd != null) {

				SceneLoadEnd.Invoke (this, new SceneLoadArgs (_activeCharSpawn));
			}
            
			// Store scene data
			StoreSceneData (scene);
			
			
			
			yield return true;
		}

		/// <summary>
		/// Loads the scene with load screen.
		/// </summary>
		/// <returns>The scene with load screen.</returns>
		/// <param name="scene">Scene.</param>
		
		IEnumerator LoadSceneWithLoadScreen(string scene) {
	
			// Load the Load Screen
			async =  Application.LoadLevelAsync(_loadScreenScene);
			async.allowSceneActivation = true;
			
			// Now that we've loaded the load screen
			// We can start loading our actual scene
			async = Application.LoadLevelAsync (scene);
			async.allowSceneActivation = true; // False if I want it to hold
			yield return async;
			
			
			// Load Scene Data
			/*
			if (_loadXMLDataOnLoad) {

				LoadSceneData (Application.dataPath + _dataPath + scene + "/");
			}*/
			
	
			
			// Invoke Scene Load End
			
			if (SceneLoadEnd != null) {
				SceneLoadEnd.Invoke (this, new SceneLoadArgs (_activeCharSpawn));
			}
			
		
			// Store scene data
			StoreSceneData (scene);



			yield return true;
		}

		/// <summary>
		/// Stores the scene data.
		/// </summary>


		void StoreSceneData(string scene) {
			// Check to see if a new scene info needs to be added or not
			
			// Try to set the scene info to one tha that already exists.
			// If it cannot be found then create and set a new one
			
			if (!TrySetSceneInfo (scene)) {
				_sceneInfo = new SceneInfo(scene);
				_sceneInfos.Add(_sceneInfo);
				
			}
			else {
				
				// Set to the correct scene info
				foreach(SceneInfo sceneInfo in _sceneInfos) {
					
					if(sceneInfo.Key == scene) {
						_sceneInfo = sceneInfo;
					}
					
				}
				
			
				
				foreach(Entity entity in GameObject.FindObjectsOfType<Entity>()) {
					
					foreach(SceneObject sceneObject in _sceneInfo.SceneObjects) {
						
						if(sceneObject.Key == entity.Key) {
							
							entity.ReceiveData((ISceneObject<string>)sceneObject);
							
						}
						
					}
					
				}
				
			}
		
		}

		
		bool TrySetSceneInfo(string scene) {
			
			foreach (SceneInfo sceneInfo in _sceneInfos) {
				
				if(sceneInfo.Key == scene) {

					_sceneInfo = sceneInfo;

					return true;

				}
				
			}

			return false;
			
		}


		/// <summary>
		/// Handles an entity being enabled/activated.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>

		void HandleEntityEnabled (object sender, EventArgs e)
		{
			if (_sceneInfo != null) {
				_sceneInfo.ReceiveSceneObjectChange ((ISceneObject<string>)sender);
			}

		}


		/// <summary>
		/// Handles an entity being disabled/deactivated.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>

		void HandleEntityDisabled (object sender, EventArgs e)
		{
			if (_sceneInfo != null) {
						_sceneInfo.ReceiveSceneObjectChange ((ISceneObject<string>)sender);
				}

		}

		/// <summary>
		/// Determines whether this an entity of a given key is set to be disabled.
		/// </summary>
		/// <returns><c>true</c> if the specified entity is disabled; otherwise, <c>false</c>.</returns>
		/// <param name="key">Key.</param>

		bool IsEntityDisabled(string key) {
		
			foreach (string disabled_key in _disabledEntitiesList) {
			
				if(disabled_key == key) {
					return true;
				}

			}
		
			return false;
		
		}

        /// <summary>
        /// Adds the scene entity.
        /// </summary>
        /// <param name="entity">Entity.</param>

        // TODO: This gets added to a list of entities when an object is awoken, 
        // however, I don't think scene entities is being used any longer and I should then be able to remove this
        // Which would be really good because there is a big of sphagetti where I believe my entities is referencing a scene manager

        
		public void AddSceneEntity(Entity entity) {
			
			try {

				_sceneEntities.Add (entity.Key, entity);
			}
			catch {

                Debug.Log("Scene entity could not be added");

			}
		}

		/// <summary>
		/// Gets the cut scene.
		/// </summary>
		/// <returns>The cut scene.</returns>
		/// <param name="name">Name.</param>
	

		public ICutScene GetCutScene(string name) {

			
			foreach(ICutScene cutscene in GameData.SceneData.CutScenes) {
			

				if(cutscene.Key == name) { 

					return (ICutScene)cutscene;

				}

			}

			return null;

		}

        /*
		void OnGUI() {

			if (async != null) {
								GUILayout.Label ("Load progress: " + async.progress, GUILayout.Width (500F));
						}

		}*/

		/// <summary>
		/// Initializes the scene entities.
		/// </summary>

		void InitializeSceneEntities() {
		
			// TODO: Optimize 
			foreach (Entity entity in GameObject.FindObjectsOfType<Entity>()) {
			
				if(IsEntityDisabled(entity.Key)) {
					entity.gameObject.SetActive(false);
				
				}

			}

		
		}

		// Use this for initialization
		void Awake () {

			// Set this singleton instance
			_instance = this;


			// Keep track of disabled and enabled entities
			Entity.EntityDisabled += HandleEntityDisabled;
			Entity.EntityEnabled += HandleEntityEnabled;
            
		}

		void OnDestroy() {

            
		
		}

		void Start() {

			// Store data about this scene
			StoreSceneData (Application.loadedLevelName);
		}

		void HandleLoadSceneComplete (object sender, EventArgs e)
		{
			// This method sends data back to entities (as well as stores the data if necessary)
			StoreSceneData (Application.loadedLevelName);
		}

		/// <summary>
		/// Handles the user loading the scene.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>

		void HandleUserLoadsScene (object sender, EventArgs e)
		{
			_sceneInfos.Clear ();

		}


        /// <summary>
        /// Load scene data
        /// </summary>
        /// <param name="path"></param>

		void LoadSceneData(string path) {

            // Raise scene data loaded event
            if (SceneDataLoaded != null){SceneDataLoaded.Invoke();}
        }

	
        /// <summary>
        /// Used to locate a keyed object from anywhere
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
		
		public T FindKeyedObject<T>(string key) where T : class {
	
            // TODO: This method is a very important part of my architecture, it works but is a mess
            // There's a lot of cleaning up to do here, unfortunately important milestones prevent me 
            // from tinkering with this right now
            	
	
			// Search through scene entities
			Entity e = FindEntity(key);
			if (e != null) {
				T keyedObject = e.GetComponent (typeof(T)) as T;

				if (keyedObject != null) {
					return keyedObject;
				}

			}

		

			T keyed = (T)Keyed.GetKey(key);
			if (keyed != null) {return keyed;}
				

			// Get as a component
			
			try {
				T keyedObject = FindSceneObject(key).GetComponent(typeof(T)) as T;

				if(keyedObject != null) {
				
			//		Debug.Log ("Scene Object Found? " + keyedObject); 

					return keyedObject;
				}
				else {
				
			//		Debug.Log ("Scene Object Not Found");

				
				}
			}
			catch {
				


				
			}
	

			return null;
			
		
			
		}

	
		/// <summary>
		/// Finds an entity.
		/// </summary>
		/// <returns>The entity.</returns>
		/// <param name="entityKey">Entity key.</param>

		public Entity FindEntity(string entityKey)  {

			foreach(Entity e in GameObject.FindObjectsOfType<Entity>()) {
				
				if(e.Key == entityKey) {
					
					return e;
					
				}
				
			}


			return null;
		}


		/// <summary>
		/// Finds the scene object. Deprecating - do not use this
		/// </summary>
		/// <returns>The scene object.</returns>
		/// <param name="objectName">Object name.</param>


		public GameObject FindSceneObject(string entityKey) {
		
			// TODO: Remove this in favor of find entity

			// Get the entity by key

			Entity entity;
			_sceneEntities.TryGetValue (entityKey, out entity);

			if(entity != null && entity.gameObject != null){

				return entity.gameObject;
			}
		
			// Could not locate the object, log warning and return GameObject.Find as a worst case scenario
			Debug.LogWarning ("Could not locate '" + entityKey + "' in the scene manager referenced object, searching all objects" +
								"with GameObject.Find");


			// Otherwise, find entity by game object name but throw a warning
			return GameObject.Find (entityKey);

		}

		/// <summary>
		/// Finds the scene object.
		/// </summary>
		/// <returns>The scene object.</returns>
		/// <param name="entityKey">Entity key.</param>
		/// <typeparam name="TComponent">The 1st type parameter.</typeparam>

		public TComponent FindSceneObject<TComponent>(string entityKey) where TComponent : Component {
			GameObject gObject = FindSceneObject(entityKey);	
		
			if (gObject != null) {
				// Return the component of the type
				return gObject.GetComponent<TComponent>();
			}

			Debug.Log ("Scene Object not located");



			// Return null if nothing was found
			return null;
		}

		/// <summary>
		/// Starts the cutscene.
		/// </summary>
		/// <param name="cutsceneKey">Cutscene key.</param>
		/// <param name="continuesFromPrevious">If set to <c>true</c> continues from previous.</param>

		public void StartCutscene(string cutsceneKey, bool continuesFromPrevious = false) {
			

			// Find cutscene and play it
			ICutScene cutscene = SceneController.Instance.GetCutScene (cutsceneKey);
			
			if(cutscene!=null) {
				// Play Cut Scene
				cutscene.Play();
			}
			// If we're continuing from a previous cutscene then we don't want to change states
			if(!continuesFromPrevious) {
				GameController.GameStateMachine.ChangeGameState (GameController.GameStateMachine.OnCutScene, GameController.CamManager.ActiveCameraNode);
			}
			
			// Set the new camera position

            // TODO: Having the status of cutscenes live in static members of the CutScene Data type 
            // Was a quick solution to solve a problem, Ideally this should live in the controller for consistency
            // Not a good time to refactor this yet, will do so when opportunity arises

			if (GameController.CamManager != null && WhatPumpkin.CutScenes.CutScene.Active != null && WhatPumpkin.CutScenes.CutScene.Active.CameraNode != null) {
						GameController.CamManager.ActiveCameraNode = WhatPumpkin.CutScenes.CutScene.Active.CameraNode;
			}
			
			
			if (cutscene == null) {
				GameController.GameStateMachine.ReturnToPreviousState(null);
			}
			
		}

	}


	/// <summary>
	/// Scene load arguments.
	/// </summary>

	public class SceneLoadArgs : EventArgs {

		/// <summary>
		/// Gets or sets the spawn point.
		/// </summary>
		/// <value>The spawn point.</value>

		public string SpawnPoint { get; internal set; }
	
		/// <summary>
		/// Initializes a new instance of the <see cref="WhatPumpkin.HiveSwap.SceneManager.SceneLoadEvent"/> class.
		/// </summary>
		/// <param name="spawnpoint">Spawnpoint.</param>

		public SceneLoadArgs(string spawnpoint) {

			SpawnPoint = spawnpoint;
		
		}

	}

}