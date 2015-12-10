#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - June 22, 2015
#endregion 

#region using
using UnityEngine;
using WhatPumpkin.Actions.Sequences;
using WhatPumpkin.Sgrid;
using WhatPumpkin.CutScenes;
#endregion


namespace WhatPumpkin {

	/// <summary>
	/// Game data.
	/// </summary>

	public class GameData : MonoBehaviour {

		/// <summary>
		/// The persistent data.
		/// </summary>

		static GameData _persistentData;

		/// <summary>
		/// The scene data.
		/// </summary>

		static GameData _sceneData;

		/// <summary>
		/// Gets the persistent data.
		/// </summary>
		/// <value>The persistent data.</value>

		static public GameData PersistentData { get { return _persistentData; } }

		/// <summary>
		/// Gets the scene data.
		/// </summary>
		/// <value>The scene data.</value>

		static public GameData SceneData { get { return _sceneData; } }

		/// <summary>
		/// Is this persisent game data or will it get destroyed when we move on to a new unity scene?
		/// </summary>

		[SerializeField] bool _isPersisentData = false;

		#region data collection - read note

		// *** Remember, whenever adding a new data type add it to the key list in the HandleSceneLoad end handler *** //

		/// <summary>
		/// The action sequences.
		/// </summary>

		[SerializeField] ActionSequence [] _actionSequences;

		/// <summary>
		/// The _verb action sequences.
		/// </summary>
		
		[SerializeField] VerbActionSequence [] _verbActionSequences; 

		/// <summary>
		/// The combine action sequences.
		/// </summary>

		[SerializeField] CombineActionSequence [] _combineActionSequences;

		/// <summary>
		/// The cut scenes.
		/// </summary>
		
		[SerializeField] CutScene [] _cutScenes;

		/// <summary>
		/// A collection of materials.
		/// </summary>

		[SerializeField] Material [] _materials;

		// Why are these lists and not Arrays?
        

		#endregion

		/// <summary>
		/// Gets a value indicating whether this instance is persistent data.
		/// </summary>
		/// <value><c>true</c> if this instance is persistent data; otherwise, <c>false</c>.</value>

		public bool IsPersistentData { get { return _isPersisentData; } }


		public CutScene [] CutScenes {
		
			get {return _cutScenes;}

#if UNITY_EDITOR

			set {_cutScenes = value;}

#endif
		
		}

		/// <summary>
		/// Gets the action sequences.
		/// </summary>
		/// <value>The action sequences.</value>

		public ActionSequence [] ActionSequences { 

			get { return _actionSequences; }

#if UNITY_EDITOR
			set { _actionSequences = value;}
#endif
		}

		/// <summary>
		/// Gets the verb action sequences.
		/// </summary>
		/// <value>The verb action sequences.</value>

		public VerbActionSequence [] VerbActionSequences { 
			get { return _verbActionSequences; } 

#if UNITY_EDITOR
			set { _verbActionSequences = value;}
#endif
		}

		/// <summary>
		/// Gets the combine action sequences.
		/// </summary>
		/// <value>The combine action sequences.</value>
		
		public CombineActionSequence [] CombineActionSequences { 
		
			get { return _combineActionSequences; } 
		
        }

		/// <summary>
		/// Gets the materials.
		/// </summary>
		/// <value>The materials.</value>

		public Material [] Materials { get { return _materials; } }

		/// <summary>
		/// Gets the collection of a given key.
		/// </summary>
		/// <returns>The collection.</returns>
		/// <param name="key">Key.</param>

		public IKeyed [] GetCollection(string key) {
				
			foreach (DataCollection collection in this.GetComponents<DataCollection>()) {
			
				if(collection.Key == key) {
				
					return collection.Collection; 

				}
			
			}

			// No collection found

			return null;

		}

		/// <summary>
		/// Occurs on awake
		/// </summary>

	    void Awake() {

			// Set up persistent and scene data as necessary

			if (_isPersisentData) {
				// Peristent Data
				_persistentData = this;
				// Keep track of whenever a scene loads
				GameController.SceneManager.SceneLoadEnd += HandleSceneLoadEnd;
			}
			else {

				// Scene Data
				_sceneData = this;
			}

		}

	    void HandleSceneLoadEnd (object sender, System.EventArgs e)
	    {
            //			Debug.Log ("Persistent Data Handle's scene load end");

            // Handles this for persistent data
            // Unfortunately I'll have to remember to do this for every data type in this collection... it sucks... thanks unity
            // TODO: I am changing this significantly so that I will no longer need "Game Data"
            Keyed.AddKeys(_actionSequences);
			Keyed.AddKeys(_verbActionSequences);
			Keyed.AddKeys(_combineActionSequences);
            Keyed.AddKeys(_cutScenes);

	    }

		void OnDestroy() {
		
			if (_persistentData) {
				GameController.SceneManager.SceneLoadEnd -= HandleSceneLoadEnd;
			}

		}

#if UNITY_EDITOR

		void SetDirty() {
			
			UnityEditor.EditorUtility.SetDirty (this);
			
		}

		/*
		public void AddNewToSequence<T>(T [] sequence) where T : new () {
		
			T [] temp = sequence;

			sequence = DataUtilities.AddArrayElement<T> (temp, new T ());
		
		}*/

		// TODO: Generify all this
	
/*
		public void AddActionSequence() {
		
			_actionSequences = DataUtilities.AddArrayElement<ActionSequence> (_actionSequences, new ActionSequence ());
		
		}

		public void RemoveActionSequene(ActionSequence actionSequence) {
		
		
			_actionSequences = DataUtilities.RemoveArrayElement<ActionSequence> (_actionSequences, actionSequence);

		}


		public void AddCombineActionSequence() {
			
			_combineActionSequences = DataUtilities.AddArrayElement<CombineActionSequence> (_combineActionSequences, new CombineActionSequence ());
			
		}
		
		public void RemoveCombineActionSequene(ActionSequence actionSequence) {
			

			CombineActionSequence combineSequence = (CombineActionSequence)actionSequence;

			if(combineSequence != null) {
				_combineActionSequences = DataUtilities.RemoveArrayElement<CombineActionSequence> (_combineActionSequences, combineSequence);
			}
		}




		#region quick scripts just used for transfering data over

	
		//public VerbActionSequence VerbActionSequencwe

		public void ReceiveCombineActionSequences(CombineActionSequence [] _sequences) {
		
			_combineActionSequences = _sequences;
		
		}

		public void ReceiveVerbActionSequences(VerbActionSequence [] _sequences) {
			
			_verbActionSequences = _sequences;
			
		}

		public void ReceiveActionSequences(ActionSequence [] _sequences) {
			
			_actionSequences = _sequences;
			
		}*/

	

#endif


	}
}
