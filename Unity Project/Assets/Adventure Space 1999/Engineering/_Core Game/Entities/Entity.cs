#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - November 5, 2014
#endregion

#region using
using UnityEngine;
using System;
using System.Collections.Generic;
#endregion

namespace WhatPumpkin.Sgrid
{

    /// <summary>
    /// Entity - Keyed scene objects
    /// </summary>


    [RequireComponent(typeof(EntityInfo))]

	public class Entity : MonoBehaviour, IEntity, IEnable, ISwitchable, ISceneObject<string> {


		#region events

		/// <summary>
		/// Occurs when an entity is renamed. This is primarily used for editor tools.
		/// </summary>

		static public event EventHandler<RenameKeyEventArgs> Renamed;


        // TODO: This is a disaster. Why didn't I just create one event with multiple args? Fix this!
		static public event EventHandler EntityDisabled;
		static public event EventHandler EntityEnabled; 
		public virtual event EventHandler Activated; 
		public virtual event EventHandler Destroyed;

		#endregion 


		#region properties

		/// <summary>
		/// Gets the entity info.
		/// </summary>
		/// <value>The entity info.</value>
		
		protected EntityInfo EntityInfo { get { 

				try {
					// Get the entity component attached
					EntityInfo e = this.GetComponent<EntityInfo>();
					
					// If on is not attached then attach one
					if(e == null){e = this.gameObject.AddComponent<EntityInfo>();}
					
					// Return the entity component
					return e; 
				}
				catch (Exception e){
					Debug.LogException(e);
					
				}

				return null;
			} 
		} 
		
		
		
		public int Id { get { return EntityInfo.Id; } }
		
		
		/// <summary>
		/// Gets the name based on the build setting's language.
		/// </summary>
		/// <value>The name.</value>
		
		public string Name {get { return EntityInfo.Name; } } 

		/// <summary>
		/// Gets the description based on the build setting's language.
		/// </summary>
		/// <value>The description.</value>
		
		public string Description {get { return EntityInfo.Description; } } 
		
		
		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>
		
		public string Key { 

			get { 
				if(EntityInfo != null) {
					return EntityInfo.Key; 
				}
				else {
					Debug.LogError ("Entity Info not found on object " + _key);
					return "";
				}
			} 
		
		}


		/// <summary>
		/// Gets a value indicating whether this <see cref="WhatPumpkin.Sgrid.Entity"/> disable on first start.
		/// </summary>
		/// <value><c>true</c> if disable on first start; otherwise, <c>false</c>.</value>

		public bool DisableOnStart { get { return EntityInfo.DisableOnStart; } }


		/// <summary>
		/// Gets a value indicating whether this instance is active.
		/// </summary>
		/// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>

		public virtual bool IsActive { get { 
				return this.gameObject.activeSelf;
			} }


		#endregion

		#region private properties

		// Set at runtime by loaded data

		/// <summary>
		/// Sets the _name.
		/// </summary>
		/// <value>The _name.</value>

		protected string _name {set { EntityInfo.Name = value; } } 

		/// <summary>
		/// Sets the _key. 
		/// This property is basically getting treated like a field in keyed objects, except that it's using the values from EntityInfo, 
		/// the goal of this is to separate entity info from other mono behaviours thereby decoupling my components
		/// </summary>
		/// <value>The _key.</value>

		protected string _key {

			get {
				return Key;
			}

			set { 
				EntityInfo.Key = value; 
			} 
		} 

		/// <summary>
		/// Sets the id.
		/// </summary>
		/// <value>The _id.</value>
		
		protected  int _id {set { EntityInfo.Id = value; } } 

		#endregion
		




		#region static methods

		/// <summary>
		/// Finds the object by key.
		/// </summary>
		/// <returns>The object by key.</returns>
		/// <param name="key">Key.</param>
		/// <param name="collection">Collection.</param>

		// TODO: Deprecate

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
		/// Find an entity of type T with the specified key. 
		/// Meant to be overrided by derived classes to return their own collection. 
		/// This also means that the user would have to use an instanciated version of the object.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>

		public virtual T Find<T>(string key) where T : Entity {
		
			return (T)Find (key);
		}

		/// <summary>
		/// Find the specified key.
		/// </summary>
		/// <param name="key">Key.</param>

		static public Entity Find(string key) {
		
            // TODO: Optimize: I would prefer to store this in a dictionary
            // However, Unity seemed to have a lot of trouble with dictionaries
            // Whenever changing scenes
            // Perhaps I just need clear the dictionary each scene change, I could create my own hash,
            // that said, there are usually not that many entities, therefore this search is not 
            // too strenuous

				foreach(Entity entity in GameObject.FindObjectsOfType<Entity>()) {
				
					if(entity.Key == key) {
					
						return entity;
					}
                    
			}

            return null;

        }

		/// <summary>
		/// Select this instance.
		/// </summary>

		public void Select() {
		
			// TODO: This may still be necessary for combined items. I would like to be able to get rid of this.
			GameController.Instance.SelectedEntity = this;

		}

	

		void Awake() {

//			Debug.Log ("Entity Awake: " + this.Key);

			// When a new entity is created in the middle of the scene the event doesn't always get raised
			// This will make certain that it does
			// The OnEntityAwake method also makes certain that it doesn't get invoked twice
			EntityInfo.OnEntityAwake ();
		}

		/// <summary>
		/// Start this instance.
		/// </summary>
		
		protected virtual void Start() {
			Keyed.OnKeyedObjectCreated (this);
		}

		void OnDestroy() {
		
	

			// Remove this object from any of the keyed lists that may exist
			if (Destroyed != null) { 
				Destroyed(this, null);
			}
		
		}

		/// <summary>
		/// Disable this instance.
		/// </summary>

		public virtual void Disable() {

			// Disable the object
			this.gameObject.SetActive (false);

			// Then raise the disable event
			if (EntityDisabled != null) {
								EntityDisabled (this, null);
						}

		
		
		}

		/// <summary>
		/// Enable this instance.
		/// </summary>

		public virtual void Enable() {

			// Raise the entity enabled event
			if (EntityEnabled != null) {
				EntityEnabled(this,null);
			}

			this.gameObject.SetActive (true);
		}


		/// <summary>
		/// Activate this instance.
		/// </summary>

		public virtual void Activate() {
		
			Enable ();

			if (Activated != null) {
				Activated(this, null);
			}


		
		}

		/// <summary>
		/// Deactivate this instance.
		/// </summary>

		public virtual void Deactivate() {
			Disable ();
		}


		/// <summary>
		/// Sets the position.
		/// </summary>
		/// <param name="position">Position.</param>

		protected  void SetPosition(Vector3 position) {
		
			this.transform.position = new Vector3 (position.x,
			                                       position.y,
			                                       position.z);

		}

		/// <summary>
		/// Sets the rotation.
		/// </summary>
		/// <param name="rotation">Rotation.</param>

		protected void SetRotation(Quaternion rotation) {
		
			this.transform.rotation = new Quaternion (rotation.x,
			                                         rotation.y,
			                                         rotation.z,
			                                         rotation.w);
		
		}



		/// <summary>
		/// Receives scene object data. Primarily used for loading scenes
		/// </summary>
		/// <param name="item">Item.</param>

		public  void ReceiveData(ISceneObject<string> item) {

			if (item.IsActive) {
				this.gameObject.SetActive(true);
			
			}
			else if (!item.IsActive) {
				this.gameObject.SetActive(false);
				
			}
            
		}


		/// <summary>
		/// For the edtior - Raises the rename key event.
		/// </summary>
		/// <param name="key">New Key.</param>

		protected void OnRenameKey(string newKey) {
			// Make sure the keys are actually renamed before raising the event
			string oldKey = _key;

			if (oldKey != newKey) {
				_key = newKey;

				// Don't raise the event if the old key was blank or null
				if(oldKey != null && oldKey != "") {
					Renamed(this, new RenameKeyEventArgs(oldKey, newKey));
				}
			}
		}



		#endregion

	}

}
