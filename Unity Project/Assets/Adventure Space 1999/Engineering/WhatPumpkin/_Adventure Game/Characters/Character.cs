#region Copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - November 6, 2014
#endregion

#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WhatPumpkin.Sgrid.Triggers;

using WhatPumpkin.Entities;
using WhatPumpkin.States;
#endregion

namespace WhatPumpkin.Sgrid.Characters {

	#region summary
	/// <summary>
	/// Character - Base class for characters in the game
	/// </summary>
	#endregion
	
	public abstract class Character : Entity, ISpawnable, ITriggerActions, IHaveStates {

		#region fields

		// Animation States
		protected const string ANIM_STATE_SPEED = "speed";
		
		protected string ANIM_STATE_WALK = "walk";

		/// <summary>
		/// The animated character.
		/// </summary>
		
		[SerializeField] protected Animator _animatedCharacter; // The character mesh. TODO: This needs to be instantiated at runtime instead of just referenced

		/// <summary>
		/// The walk speed of the character.
		/// </summary>

		[SerializeField] protected float _speed = 3F;

		/// <summary>
		/// Triggers all actions on entry?
		/// </summary>


		[SerializeField] protected bool _triggerAllActionsOnEntry = false;

		/// <summary>
		/// Triggers Camera Sequences on entry
		/// </summary>

		[SerializeField] protected bool _triggerCameraSwitchesOnEntry = false;

		/// <summary>
		/// The accessories associated with this character. Accessories can be used to reference props, hotspots, and other game object assocaited with this character.
		/// </summary>

		[SerializeField] protected List<ConditionalGameObject> _accessories = new List<ConditionalGameObject> ();

		#endregion

		#region properties


		/// <summary>
		/// Gets a value indicating whether this <see cref="WhatPumpkin.Entities.Character"/> trigger all actions on entry.
		/// </summary>
		/// <value><c>true</c> if trigger all actions on entry; otherwise, <c>false</c>.</value>

		public bool TriggerAllActionsOnEntry { get { return _triggerAllActionsOnEntry; } }

		/// <summary>
		/// Gets a value indicating whether this <see cref="WhatPumpkin.Entities.Character"/> trigger camera switches on entry.
		/// </summary>
		/// <value><c>true</c> if trigger camera switches on entry; otherwise, <c>false</c>.</value>

		public bool TriggerCameraSwitchesOnEntry { get { return _triggerCameraSwitchesOnEntry; } }

		/// <summary>
		/// Gets the animated character.
		/// </summary>
		/// <value>The animated character.</value>
		
		public Animator AnimatedCharacter { get { return _animatedCharacter; } }

		/// <summary>
		/// Gets the current speed.
		/// </summary>
		/// <value>The current speed.</value>

		abstract public float CurrentSpeed {get; }

		#endregion

		#region methods

		/// <summary>
		/// Start this instance.
		/// </summary>

		protected override void Start() {
		
			base.Start ();
		}

		/// <summary>
		/// Spawn the specified spawnPoint.
		/// </summary>
		/// <param name="spawnPoint">Spawn point.</param>
		/// <param name="rotate">If set to <c>true</c> rotate.</param>
		
		public void Spawn(ISpawnPoint spawnPoint, bool rotate = false) {
			
			// Set the position of the object to the spawn point

			SetPosition (spawnPoint.transform.position);
			
			// Set rotation
			if(rotate) {
				SetRotation(spawnPoint.transform.rotation);
			}
		}

		#region state methods

		protected virtual void Update() {
		
			// Check to see if there is an animatedCharacter attached
			// If so send speed information to the animation
			if (_animatedCharacter != null) {
				_animatedCharacter.SetFloat(ANIM_STATE_SPEED, CurrentSpeed);
			}

		}

		/// <summary>
		/// Gets one of the state type components attached to this object.
		/// </summary>
		/// <returns>The state type.</returns>
		/// <param name="type">Type.</param>

		StateType GetStateType(string type) {

			foreach (StateType stateGroup in this.GetComponents<StateType>()) {
			
				if(type == stateGroup.Type) {
					return stateGroup;
				}
			
			}

			Debug.Log ("Could not find the type of state '" + type + "' on the character '" + Key + "'"); 

			return null;
		}

		public void ChangeStateTo(string stateType, string toState) {
		
			StateType stateGroup = GetStateType (stateType);

			if (stateGroup != null) {
			
			
				stateGroup.ChangeState(toState);

			
			}
			else {
				Debug.Log ("Could not locate the type of state '" + stateType + "' on the character '" + this.Key + "'");
			}



		
		}

		/// <summary>
		/// Gets the active state of a speified state type
		/// </summary>
		/// <returns>The active state.</returns>
		/// <param name="type">Type.</param>

		public State GetActiveState(string type) {
		
			return GetStateType(type).Active;
		
		}

		/// <summary>
		/// Sets the pathfinding target.
		/// </summary>
		/// <param name="target">Target.</param>
		
		public void SetTarget(Transform target) {
		
			AIPath aiPath = this.GetComponent<AIPath> (); 
			
			if (aiPath) {
				aiPath.target = target;
			}
			
		}

		/// <summary>
		/// Stops moving.
		/// </summary>
		
		public virtual void StopMoving() {
		
			// TODO:


		}


		#endregion


		#endregion


		#if UNITY_EDITOR

		// Methods here will be primarily used for sgrid editing if not exclusively

	
		/// <summary>
		/// Sets the properties. This is used exclusively for the sgrid editor.
		/// </summary>

		public void SetProperties(string key, string nameKey) {
		
			_key = key;
			_name = nameKey;

		}

		#endif

	}
}