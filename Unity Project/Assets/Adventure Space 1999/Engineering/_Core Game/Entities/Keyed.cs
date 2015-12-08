#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 10, 2015
#endregion 

#region using
using System;
using UnityEngine;
using System.Collections.Generic;
#endregion

namespace WhatPumpkin {

    public class KeyEvents : EventArgs
    {
        public enum KeyEventType { Created, Destoryed};

        // Fields
        KeyEventType _eventType;
        IKeyed _keyedObject;


        // Properties
        public IKeyed KeyedObject { get { return _keyedObject; } }
        public string Key { get { return _keyedObject.Key; } }
        public KeyEventType EventType { get { return _eventType; } }


        public KeyEvents(IKeyed item, KeyEventType eventType)
        {
            _keyedObject = item;
            _eventType = eventType;
        }
    }

	/// <summary>
	/// Rename key event arguments.
	/// </summary>

	public class RenameKeyEventArgs : EventArgs {
	
		/// <summary>
		/// The old key.
		/// </summary>

		string _oldKey;

		/// <summary>
		/// The new key.
		/// </summary>

		string _newKey;


		/// <summary>
		/// Gets the old key.
		/// </summary>
		/// <value>The old key.</value>

		public string OldKey { get { return _oldKey; } }

		/// <summary>
		/// Gets the new key.
		/// </summary>
		/// <value>The new key.</value>

		public string NewKey { get { return _newKey; } }


		public RenameKeyEventArgs(string oldKey, string newKey) {
			_oldKey = oldKey;
			_newKey = newKey;
		
		}
	
	}

	/// <summary>
	/// Keyed. For objects that I want to automatically alert it's creation. 
	/// They will broadcast it's creation and a game or scene manager can add the keyed object to a dictionary.
	/// Unlike the Entity type, a keyed object does not inheret from a mono behavior. They also do not require a name. 
	/// </summary>

	[System.Serializable]

	public abstract class Keyed : IKeyed {

		/// <summary>
		/// Occurs when an entity is renamed.
		/// </summary>

		static public event EventHandler<RenameKeyEventArgs> Renamed;
        static public event EventHandler<KeyEvents> KeyEvent;
        /*
        static public event EventHandler KeyedObjectCreated;
		static public event EventHandler KeyedObjectDestroyed;
        */
        
        // Unfortunate artifact I'd like to remove
        public event EventHandler Destroyed;

        /// <summary>
        /// The key list.
        /// </summary>

        static List<IKeyed> _keyList = new List<IKeyed> ();
		static public List<IKeyed> KeyList { get { return _keyList; } }




		[SerializeField] protected string _key;

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>

		public abstract string Key { get;}


		static public void ClearLists() {
		
			_keyList.Clear ();
		
		}

		/// <summary>
		/// Keies the exists.
		/// </summary>
		/// <returns><c>true</c>, if exists was keyed, <c>false</c> otherwise.</returns>
		/// <param name="key">Key.</param>
		
		static public bool KeyExists(string key) {

            // TODO: Similar to issue with entities, Unity scenes seemed to mess up my dictionaries
            // When I get a chance, optimize with dictionary or hash

			// Search Key List
			foreach (IKeyed keyed in _keyList) {
				
				if(keyed.Key == key) {
					return true;
				}
				
			}
			
			return false;
			
		}

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <returns>The key.</returns>
		/// <param name="key">Key.</param>


		static public IKeyed GetKey(string key) {
	
			// Search Key List
			foreach (IKeyed keyed in _keyList) {
				
				if(keyed.Key == key) {
					return keyed;
				}
				
			}
			return null;

		}

		/// <summary>
		/// Finds the in collection.
		/// </summary>
		/// <returns>The in collection.</returns>
		/// <param name="key">Key.</param>
		/// <param name="collection">Collection.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>

		static public T FindInCollection<T>(string key, IKeyed [] collection) {
			
			foreach (IKeyed element in collection) {
				
				if(element.Key == key) {
					
					return (T)element;
				}
				
			}
			
			return default(T);
			
		}

		public Keyed() {

            // Add key to list or dictionary
			AddKey (this);


			// Raised the keyed object created event
			OnKeyedObjectCreated (this);

		}

		static public void AddKey(IKeyed item) {

			if (item == null) {
				Debug.LogWarning("Attempting to add key but item is null");
								return;}

			_keyList.Add (item);
          
		
		}

		/// <summary>
		/// Adds a collection of keys to the list
		/// </summary>
		/// <param name="items">Items.</param>

		static public void AddKeys(IKeyed [] items) {
				
			foreach (IKeyed item in items) {
						
				AddKey(item);

			}
		
		}

		static public List<T> GetObjectsOfType<T>() {
		
			List<T> list = new List<T>();

			foreach (IKeyed item in _keyList) {

				T obj;

				try {
					obj = (T)item;
					list.Add(obj);
				}
				catch (InvalidCastException e){

                    throw (e);
				}


			}

			return list;
		}

		/// <summary>
		/// Releases unmanaged resources and performs other cleanup operations before the <see cref="WhatPumpkin.Keyed"/> is
		/// reclaimed by garbage collection.
		/// </summary>

		~Keyed() {
			if (Destroyed != null) {		
				Destroyed(this, null);
			}

			OnKeyedObjectDestroyed (this); 


		}

		/// <summary>
		/// Raises the keyed object created event.
		/// </summary>

	
		static public void OnKeyedObjectCreated(IKeyed keyedObject) {

			ArgumentReceiver.TryInit ();

            if(KeyEvent != null ) { KeyEvent(keyedObject, new KeyEvents(keyedObject, KeyEvents.KeyEventType.Created)); }
            
			keyedObject.Destroyed += HandleDestroyed;
		}

		/// <summary>
		/// Raises the keyed object destroyed event.
		/// </summary>

		static public void OnKeyedObjectDestroyed(IKeyed keyedObject) {

            if (KeyEvent != null) { KeyEvent(keyedObject, new KeyEvents(keyedObject, KeyEvents.KeyEventType.Destoryed)); }


        }
	

		/// <summary>
		/// Renames the key. 
		/// This is different from merely setting the key in that when this is invoked it will trigger an event letting other objects know that this entity has been renamed.
		/// </summary>
		/// <param name="key">Key.</param>

		public void RenameKey(string key) {

			string oldKey = _key;
			string newKey = key;

			// Change the key
			_key = key;

			// Raise the key rename event
			if (Renamed != null) {Renamed(this, new RenameKeyEventArgs(oldKey, newKey));}
	
		}


        /// <summary>
        /// Handles destroyed keyed objects.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>

        static void HandleDestroyed(object sender, EventArgs e)
        {


            IKeyed iKeyed;





            try
            {
                iKeyed = (IKeyed)sender;
            }
            catch (InvalidCastException invalidcastexception)
            {

                Debug.Log("Could not cast the object " + sender + " to a keyed object.");
                throw (invalidcastexception);

            }

            if (iKeyed != null)
            {

                if (_keyList.Contains(iKeyed))
                {
                    _keyList.Remove(iKeyed);



                }
            }
            
        }

	}
}