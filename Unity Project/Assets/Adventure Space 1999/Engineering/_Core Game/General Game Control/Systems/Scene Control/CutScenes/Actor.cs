#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - January, 20 2015
#endregion 

#region using
using UnityEngine;
#endregion

namespace WhatPumpkin.CutScenes {

	#region required components
	[RequireComponent(typeof(Animator))]
	#endregion

	public class Actor : MonoBehaviour, IActor {

        #region constants

        /// <summary>
        /// The root bone name our searches are relative to
        /// </summary>

        const string ROOT_BONE = "HIPS";

        /// <summary>
        /// This is used to follow the convention of the artists
        /// </summary>

        const string CHARACTER_GROUP_NAME = "Character_GRP";

		/// <summary>
		/// The name of the camera group
		/// </summary>

		const string CAMERA_GROUP_NAME = "Camera_GRP";

		/// <summary>
		/// The name of the node that will represent the camera transform information.
		/// </summary>

		const string CAMERA_NODE_NAME = "camera_location";

        /// <summary>
        /// The name of the target that will be referenced when using LookAt.cs
        /// </summary>
        const string CAMERA_TARGET_NAME = "camera_target";

		#endregion

		#region fields 

		/// <summary>
		/// The in game animation prior to the cut scene
		/// </summary>

		string _inGameAnimaton;

	
		/// <summary>
		/// The mesh.
		/// </summary>

		Transform _mesh;

		/// <summary>
		/// The _root position.
		/// </summary>
		
		Vector3 _rootPosition;

		/// <summary>
		/// The _start position.
		/// </summary>

		Vector3 _startPosition;

		/// <summary>
		/// The current animation clip.
		/// </summary>

		string _currentAnimationClip;
        

		#endregion


		#region properties

        /// <summary>
        /// Get the camera game object transform
        /// </summary>

		public Transform CameraGO { get
			{
				// Get the camera group
				Transform cameraGroup = GetCameraGroup();

				// Check for null reference
				if(cameraGroup != null) {
				
					// Search through each transform in the camera group and return the node related to the camera
					foreach (Transform element in cameraGroup) {
					
						if(element.name == CAMERA_NODE_NAME) {						
							return element;
						}
					}
				}
			

				// If nothing is found then return null
				return null;
			} 
		}



        public Transform CameraTarget { get
            {
                Transform cameraGroup = GetCameraGroup();
                if (cameraGroup != null){
                    foreach (Transform element in cameraGroup){
                        if (element.name == CAMERA_TARGET_NAME){
                            return element;
                        }
                    }
                }
            return null;
            }

        }

		#endregion

		#region methods

		internal void JumpToFrame(int frame) {
			
            // TODO: In progress

			Animator animator = this.GetComponent<Animator> ();

			Debug.Log ("Jumping to Frame: " + frame);
			Debug.Log ("Frame Time: " + GetTimeByFrame(frame).ToString());


			if (animator != null) {
				animator.Play(_currentAnimationClip,0,GetTimeByFrame(frame));
			}

		}
		
		float GetTimeByFrame(int frame) {

			int length = GetCurrentAnimationFrameLength ();

			// Prevent a divide by zero exception
			if (length != 0) {
			
				return (float)frame / (float)length;

			}

			// return 0;
			return 0;
		}

		int GetCurrentAnimationFrameLength() {
		
			Animator animator = this.GetComponent<Animator> ();



			if (animator != null) {
				return (int)(animator.GetCurrentAnimatorStateInfo(0).length * CutScene.FPS);
				
			}
			else {
				Debug.Log ("Animator not found, returning 0");
			}

			return 0;

		}

		internal void UpdatePosition() {
		
			
			// Update the position
			if(GetRoot() != null) {
				_rootPosition = GetRoot ().position;
			
			}

			//if (_parent && _resetParentRotOnCS) {_parent.transform.rotation = new Quaternion (0F, 0F, 0F, 0F);}
		}

		/// <summary>
		/// Gets the actor's root.
		/// </summary>
		/// <returns>The root.</returns>

		public Transform GetRoot() {

			Transform characterGroup = null;

			foreach (Transform t in this.transform) {
			
				if(t.name == CHARACTER_GROUP_NAME) {
				
					characterGroup = t;
					break;
				}
			}

			Transform characterMesh = null;
		
			if(characterGroup != null) {

				foreach (Transform t in characterGroup.transform) {
				
					if(t.name == "HIPS") {
					
						characterMesh = t;
						break;
					
					}
				
				}
			}

			return characterMesh;
		}

		/// <summary>
		/// Gets the camera group.
		/// </summary>
		/// <returns>The camera group.</returns>

		Transform GetCameraGroup() {
		
			Transform cameraGroup = null;
			
			foreach (Transform t in this.transform) {
				
				if(t.name == CAMERA_GROUP_NAME) {
					
					cameraGroup = t;
					break;
				}
			}

			return cameraGroup;
		
		}


		/// <summary>
		/// Play the specified _animationClip.
		/// </summary>
		/// <param name="_animationClip">_animation clip.</param>

		public void Play(string _animationClip, Transform origin) {
	

			// Get the animator to play it
			Animator animator = this.GetComponent<Animator> (); 

			// Check to see if there is an animator, otherwise log an error
			if (animator != null) {


				_startPosition = new Vector3 (this.transform.position.x,
				                              this.transform.position.y,
				                              this.transform.position.z);
				                          

	
				// Set the actors origin point (this is meant to adust it's origin relative to the correct environment)		
				if(origin != null){SetActorsOrigin(origin);}


				// Set the current animation clip
				_currentAnimationClip  = _animationClip;

				// Play the animator
				try {

					animator.Play(_animationClip);
				}
				catch {
				
					Debug.LogError("Could not play the animation on Actor: " + this.name + " Clip: " + _animationClip);

				}

			
			}
			else {
				Debug.LogError("Could not play animation: An animator component is not attached to the " + this.name + "cutscene actor. All cut scene actors require an animator componet attached.");
			}


		}

		/// <summary>
		/// Sets the actors origin.
		/// </summary>
		/// <param name="origin">Origin.</param>

		void SetActorsOrigin(Transform origin) {

			this.transform.position = new Vector3(origin.position.x,
			                                      origin.position.y,
			                                      origin.position.z);
			
			
			this.transform.rotation = new Quaternion(origin.rotation.x, 
			                                         origin.rotation.y,
			                                         origin.rotation.z,
			                                         origin.rotation.w);
		}
        
		/// <summary>
		/// Stop this instance.
		/// </summary>

		public void Stop() {
		

			// Get the animator to play it
			Animator animator = this.GetComponent<Animator> (); 
			
			// Check to see if there is an animator, otherwise log an error
			if (animator != null) {
				
				
				// Return to the old position
				this.transform.position = new Vector3(_rootPosition.x, 
				                                      _rootPosition.y,
				                                      _rootPosition.z);

			
				this.transform.position = _startPosition;


			}
			else {
				Debug.LogError("Could not play animation: An animator component is not attached to the " + this.name + "cutscene actor. All cut scene actors require an animator componet attached.");
			}


		
		}

		void OnDestory() {
		
			Debug.Log ("Actor Destroyed");
		
		}

		#endregion


	}
}