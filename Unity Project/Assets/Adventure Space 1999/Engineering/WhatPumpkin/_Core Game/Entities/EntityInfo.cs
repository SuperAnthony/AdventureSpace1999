#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - November 5, 2014
#endregion

#region using
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using WhatPumpkin.HiveSwap.GameControl;
using WhatPumpkin.Localization;
#endregion

namespace WhatPumpkin.Entities {

	/// <summary>
	/// EntityInfo - all entities require this component
	/// Do not add this component to an object, all objects that derive from type Enity will do so automatically
	/// Entity info allows 1. Separation of basic entity component data from derived components and 2. Ensures Default behaviours on an entity gets executed regardless of what derived classes do
	/// </summary>


	// TODO: I'm not sure that EntityInfo should implement IEntity

	public class EntityInfo : MonoBehaviour, IEntity {

		
		#region static events
		
		/// <summary>
		/// Occurs when awoken.
		/// </summary>

		static public event EventHandler Awoken;
		
		#endregion


		#region fields

		/// <summary>
		/// The key identifier.
		/// </summary>
		
		[SerializeField] protected string _key = "";

		/// <summary>
		/// The unique id of the entity.
		/// </summary>
		[SerializeField] protected int _id = 0; 

		/// <summary>
		/// The key name of the entity.
		/// </summary>

		[SerializeField] protected string _name = "";

		/// <summary>
		/// The description of the entity. Each element represents a langauge.
		/// </summary>

		[SerializeField] string [] _description;

		/// <summary>
		/// The _disable on awake.
		/// </summary>

		[SerializeField] bool _disableOnAwake = false;

		/// <summary>
		/// _disable on first start.
		/// </summary>

		[SerializeField] bool _disableOnStart = false; 

		/// <summary>
		/// Was the entity awake event raised?
		/// </summary>

		bool _wasEntityAwakeEventRaised = false;

		/// <summary>
		/// Occurs when object is destroyed. Needed to complete IEntity implementation though I'm not it should implement this interface at all
		/// </summary>

		public event EventHandler Destroyed;

		#endregion

		#region properties

		public int Id { get { return _id; } protected internal set { _id = value; } }
	

		/// <summary>
		/// Gets the name based on the build setting's language.
		/// </summary>
		/// <value>The name.</value>
		
		public string Name {get { return _name; } protected internal set { _name = value; }} 

		/// <summary>
		/// Gets the description based on the build setting's language.
		/// </summary>
		/// <value>The description.</value>

		public string Description {get { return "description"; } } 


		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>

		public string Key { get { return _key; } protected internal set { _key = value; } }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="WhatPumpkin.Entities.EntityInfo"/> disable on first start.
		/// </summary>
		/// <value><c>true</c> if disable on first start; otherwise, <c>false</c>.</value>

		public bool DisableOnStart { get { return _disableOnStart; } protected internal set { _disableOnStart = value; } }


		#endregion
	
		#region static methods

		/// <summary>
		/// Finds the object by key.
		/// </summary>
		/// <returns>The object by key.</returns>
		/// <param name="key">Key.</param>
		/// <param name="collection">Collection.</param>

		// TODO: Deprectae?

		static public T FindObjectByKey<T>(string key, IKeyed [] collection) {
		
			foreach (IKeyed element in collection) {

				if(element.Key == key) {

					return (T)element;
				}

			}

			return default(T);
		
		}

		#endregion

		#region methods

		/// <summary>
		/// Select this instance.
		/// </summary>

		public void Select() {
		
			GameController.Instance.SelectedEntity = this;

		}


		void Awake() {
			
			OnEntityAwake ();

			// Fill in blank data
			if(_key=="" || _key == null){_key=gameObject.name;}
			if(_name=="" || _name == null){_name=gameObject.name;}
			
		
			// TODO: Verify this works and that I can in fact comment this out of start
			Keyed.AddKey ((IKeyed)this.GetComponent<Entity>());

			if(_disableOnAwake){this.GetComponent<Entity>().Deactivate();}

		
		}

		void Start() {
		


			// TODO: Verify this works across the board
			// Add this entity to the keyed dictionary prior to the entity getting disabled
			//Keyed.AddKey ((IKeyed)this.GetComponent<Entity>());

			// Disable this object if it's meant to be disabled
			//if(DisableOnStart){this.gameObject.SetActive (false);}
			if(DisableOnStart){this.GetComponent<Entity>().Deactivate();}


		}

		internal void OnEntityAwake() {


			if(!_wasEntityAwakeEventRaised) {
				// Broadcast that this entity has awoken
				Entity _entity = this.GetComponent<Entity> ();


				if (_entity != null && Awoken != null) {
					Awoken.Invoke(_entity, null);
					_wasEntityAwakeEventRaised = true;
				}
			}
		

		}



		#endregion

#if UNITY_EDITOR

		[ExecuteInEditMode]
		/// <summary>
		/// Sets the key. Meant to be used by the editor in the inspector view and no place else.
		/// </summary>
		/// <param name="key">Key.</param>
		public void SetKey(string key) {_key= key;}

		[ExecuteInEditMode]

		/// <summary>
		/// Raises the destroy event.
		/// </summary>

		void OnDestroy() {
		
			// Let the keyed objects know that this has been destroyed
			Keyed.OnKeyedObjectDestroyed (this);
		

		}

#endif

	}

}
