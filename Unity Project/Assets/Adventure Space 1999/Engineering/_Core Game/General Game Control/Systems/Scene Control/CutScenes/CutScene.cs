#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - January 20, 2015
#endregion 

#region using
using UnityEngine;
using System;
using System.Collections.Generic;
using WhatPumpkin.CameraManagement;
#endregion

namespace WhatPumpkin.CutScenes {

	/// <summary>
	/// Cutscene - contains cutscene data
	/// </summary>

	[System.Serializable]

	public class CutScene : Keyed, IKeyed, IPerform, IPlayable, ICutScene {

		#region static properties

		/// <summary>
		/// The FPS.
		/// </summary>

		public const int FPS = 30;


		public const int CAMERA_CUT_JUMP = 1006;

		/// <summary>
		/// The active cut scene.
		/// </summary>

		static public CutScene Active { get { return _active; } }

		/// <summary>
		/// Gets the currently playing cutscene.
		/// </summary>
		/// <value>The currently playing.</value>

		static public string CurrentlyPlaying { get
			{ 
				if(Active != null)
					return Active.Key; // Return the name of the cutscene that is currently playing
				else 
					return ""; // No cutscene is playing
			} 
		}

		/// <summary>
		/// Occurs when the currently playing cutscene ends.
		/// </summary>

		static public event Action EndCutscene;

		/// <summary>
		/// Occurs when a cutscene starts playing.
		/// </summary>

		static public event Action StartCutscene;



		#endregion


		#region static fields
		/// <summary>
		/// The active cut scene.
		/// </summary>
		
		static CutScene _active;

		#endregion

		#region fields

		/// <summary>
		/// TODO
		/// </summary>

		[SerializeField] int _frameCutAnticipation = 2;

		/// <summary>
		/// The number of frames to cut from the point at which a cut was designated by the developer.
		/// </summary>

		[SerializeField] int _numFramesToCut = 5;

		/// <summary>
		/// The _animation clip.
		/// </summary>

		[SerializeField] string _animationClip;

		/// <summary>
		/// The next cut scene that will get played immediately after this cut scene ends.
		/// </summary>

		[SerializeField] string _nextCutScene;

		/// <summary>
		/// The _actors.
		/// </summary>

		[SerializeField] List<Actor> _actors = new List<Actor>();

		/// <summary>
		/// The origin of the cutscene
		/// </summary>


		[SerializeField] Transform _origin;
	
		/// <summary>
		/// The frames in which a camera cuts occurs.
		/// </summary>

		[SerializeField] int [] _cameraCuts;

		/// <summary>
		/// Has this cutscene started playing.
		/// </summary>

		bool _hasStartedPlaying = false;



		#endregion


		#region properties

		/// <summary>
		/// Gets or sets the play order. The play action itself will use this to keep track of which object is getting played when. 
		/// This is being used as a way or IDing the object
		/// </summary>
		/// <value>The play order.</value>

		public int PlayOrder { get; set;}


		/// <summary>
		/// Gets the origin for the cutscene.
		/// </summary>
		/// <value>The origin.</value>

		public Transform Origin { get { return _origin; } }

		/// <summary>
		/// Gets a value indicating whether this instance has started playing.
		/// This is dependent on the IsPlaying property having been referenced
		/// </summary>
		/// <value><c>true</c> if this instance has started playing; otherwise, <c>false</c>.</value>

		public bool HasStartedPlaying { get { return _hasStartedPlaying;} }

		/// <summary>
		/// Gets a value indicating whether this instance has next cut scene.
		/// </summary>
		/// <value><c>true</c> if this instance has next cut scene; otherwise, <c>false</c>.</value>


		public bool HasNextCutScene { get { return _nextCutScene != null && _nextCutScene != ""; } }

		/// <summary>
		/// Gets the next cut scene.
		/// </summary>
		/// <value>The next cut scene.</value>

		public string NextCutScene { get { return _nextCutScene; } }

		/// <summary>
		/// Gets the CS animation.
		/// </summary>
		/// <value>The CS animation.</value>

		public string CSAnimation { get { return _animationClip; } }

		/// <summary>
		/// Gets the principal actor.
		/// </summary>
		/// <value>The principal actor.</value>

		internal Actor PrincipalActor { get { return _actors [0]; }}


		/// <summary>
		/// Gets the actors.
		/// </summary>
		/// <value>The actors.</value>
		/// 
		public List<IActor> Actors {
            get {

                List<IActor> iActors = new List<IActor>();

                foreach(Actor actor in _actors)
                {
                    iActors.Add((IActor)actor);
                }

                return iActors; }
        }


		public CutScene() {
		

		}

		~CutScene() {
		}

		/// <summary>
		/// Performs a camera cut from the current frame
		/// </summary>

		void PerformCameraCut() {
		
			Animator principalActor = _actors[0].GetComponent<Animator>();
			int currentFrame = GetCurrentFrame (principalActor);

			foreach (Actor actor in _actors) {
				actor.JumpToFrame(currentFrame + _numFramesToCut);			
			}

		}

		/// <summary>
		/// Performs a camera cut from a designated frame
		/// </summary>
		/// <param name="frame">From Frame Designated.</param>

		void PerformCameraCut(int from_frame) {
			
			_actors[0].GetComponent<Animator>();

			foreach (Actor actor in _actors) {
				actor.JumpToFrame(from_frame + _numFramesToCut);			
			}
			
		}
	

		/// <summary>
		/// Determines whether a camera cut occurs at the specified frame
		/// </summary>
		/// <returns><c>true</c> if camera cut return true; otherwise, <c>false</c>.</returns>
		/// <param name="frame">Frame.</param>

		bool IsCameraCut(int current_frame) {

			if (_cameraCuts != null) {
			
				foreach(int cameracut_frame in _cameraCuts) {
				
					if(current_frame >= (cameracut_frame - _frameCutAnticipation) &&
					   current_frame < (cameracut_frame + (_numFramesToCut))) {
						return true;
					}

				}
			}

			return false;
		}

		static int GetCurrentFrame(Animator actor){

			return (int)Mathf.Floor ((actor.GetCurrentAnimatorStateInfo (0).normalizedTime * GetAnimationLength(actor)));

		}

		static int GetAnimationLength(Animator actor) {

			return Mathf.RoundToInt (actor.GetCurrentAnimatorStateInfo (0).length * FPS);

		}


		internal void Update() {



			Animator principalActor = _actors[0].GetComponent<Animator>();

			if(principalActor.GetCurrentAnimatorStateInfo(0).IsName(_animationClip)) {

				// Check for camera cut
				if(IsCameraCut(GetCurrentFrame(principalActor))) {PerformCameraCut();}

				_hasStartedPlaying = true;



			}
		

			if (HasStartedPlaying && !principalActor.GetCurrentAnimatorStateInfo (0).IsName (_animationClip)) {
			
				Stop ();

			}


		}

		public CameraNode CameraNode { 
			get {

				// TODO: Remove hardcoding


				// Find the object called Camera_GRP
				Transform  Camera_GRP = null;
				foreach(Transform t in _actors[0].transform) {

					if(t.name == "Camera_GRP") {
					
						Camera_GRP = t;
						break;
					}

				}

				// Find the object called camera_direction in camera group
				Transform camera_location = null;
				foreach(Transform t in Camera_GRP) {
					if(t.name == "camera_location") {
						camera_location = t;
					}
				}

				if(camera_location != null) {

					// Check to see if the camera direction has a camera node attached, if not then attach one
					if(camera_location.GetComponent<CameraNode>() == null) {
					
						// Component not found attach one and send a warning
						camera_location.gameObject.AddComponent<CameraNode>();
						Debug.LogWarning("A camera node was not attached to the camera associated with " + _actors[0].name + " so one was attached automatically.");

					}

				}
				else {
					// No camera direction object was found, return null
					return null;

				}

				return camera_location.GetComponent<CameraNode>();
			}
		}


		public override string Key { get { return _key; } }

		#endregion


		#region methods


		/// <summary>
		/// Initializes a new instance of the <see cref="WhatPumpkin.CutScenes.CutScene"/> class.
		/// </summary>
		/// <param name="actors">Actors.</param>
		/// <param name="origin">Origin.</param>
		/// <param name="clipName">Clip name.</param>
		/// <param name="nextClip">Next clip.</param>

		public CutScene(List<Actor> actors, Transform origin, string clipName, string nextClip ) {
		
			_actors = actors;
			_origin = origin;
			_animationClip = clipName;	
			_nextCutScene = nextClip;
			
		}

		/// <summary>
		/// Raises the CS end event.
		/// </summary>

		static void OnCSEnd() {

			// Return state
			GameController.GameStateMachine.ReturnToPreviousState (null);


			if (EndCutscene != null) {

		
				EndCutscene.Invoke();

				// Unsubscribe all End CS event
				EndCutscene = null;

			}

		
		}

		/// <summary>
		/// Broadcasts the CS start.
		/// </summary>

		static void RaiseStartCSEvent() {
			
			if (StartCutscene != null) {
				
				StartCutscene.Invoke();
				
			}
			
		}




		#region IPerform implemenation

		/// <summary>
		/// Occurs when finished playing.
		/// </summary>

		public event EventHandler FinishedPlaying;

		/// <summary>
		/// Play this instance.
		/// </summary>
		
		public void Play() {

			GameController.ApplicationState.OnUpdate += Update;

			// Set this to the active cutscene
			_active = this;

			// Play the animation for each actor in the cut scene
			foreach(Actor actor in _actors) {
				if(actor != null) {
					actor.Play(_animationClip, _origin);
				}
			}

			_hasStartedPlaying = false;

			// Broadcast the start of this cutscene
			RaiseStartCSEvent ();
            
		}

		
		/// <summary>
		/// Stop the cutscene. 
		/// </summary>

		public void Stop() {

			GameController.ApplicationState.OnUpdate -= Update;

			// There are no active cutscenes
			_active = null;

			// Stop all actors
			foreach(Actor actor in _actors) {actor.Stop();}

			// TODO: Event manager with a cutscene stop to handle
			if (HasNextCutScene) {

				GameController.CutsceneManager.StartCutscene (_nextCutScene, true);

			} 
			else {

				OnCSEnd (); // This may be removed eventually
				OnFinishedPlaying();
			}
		}

		/// <summary>
		/// Raises the finished playing event.
		/// </summary>

		void OnFinishedPlaying() {
		
			if (FinishedPlaying != null) {
				FinishedPlaying.Invoke(this,null);
			}

		}


		public void Pause() {
			
		}


		public void Resume() {
			// TODO:
		}

		#endregion

		/// <summary>
		/// Determines whether this instance has an actor of the specified gameObjectName.
		/// </summary>
		/// <returns><c>true</c> if this instance has actor the specified gameObjectName; otherwise, <c>false</c>.</returns>
		/// <param name="gameObjectName">Game object name.</param>

		public bool HasActor(string gameObjectName) {
			foreach (Actor actor in _actors) {
                if (actor == null)
                {
                    return false;
                }
				else if(actor.gameObject.name == gameObjectName) {
					return true;
				}
			}
			return false;
		}

        public GameObject GetActor(string gameObjectName)
        {
            foreach (Actor actor in _actors)
            {
                if (actor.gameObject.name == gameObjectName)
                {
                    return actor.gameObject;
                }
            }
            return null;
        }

        public bool HasEnvironment(string gameObjectName)
        {
            if (_origin == null)
            {
                return false;
            }
            else if (_origin.gameObject.name == gameObjectName)
            {
                return true;
            }
            return false;
        }

        public GameObject GetEnvironment(string gameObjectName)
        {
            if (_origin.gameObject.name == gameObjectName)
            {
                return _origin.gameObject;
            }
            return null;
        }
	
		/// <summary>
		/// Test this instance.
		/// </summary>

		public void Test() {
		
			Play ();

			// Get a camera manager
			CameraController camManager = GameObject.FindObjectOfType<CameraController> ();
		
			// Create a camera node
			GameObject camNodeGO = new GameObject ("CameraNode");

			camNodeGO.AddComponent<CameraNode> ();

			// Set the active camera node
			if (camManager != null) {
								//camManager.ActiveCameraNode = camNode;
			}

		}

		
		/// <summary>
		/// Sets the CS properties.
		/// </summary>
		/// <param name="actors">Actors.</param>
		/// <param name="origin">Origin.</param>
		/// <param name="clipName">Clip name.</param>
		/// <param name="nextClip">Next clip.</param>

		public void SetCSProperties(List<Actor> actors, Transform origin, string clipName, string nextClip ) {
		
			// TODO: Remove this method

			_actors = actors;
			_origin = origin;
			_animationClip = clipName;
            if (nextClip != "")
            {
                _nextCutScene = nextClip;
            }

		}

		#endregion

	}
}
