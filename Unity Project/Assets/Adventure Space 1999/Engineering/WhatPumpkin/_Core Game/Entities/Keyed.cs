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

		/// <summary>
		/// Occurs when a keyed object is created.
		/// </summary>


		static public event EventHandler KeyedObjectCreated;

		/// <summary>
		/// Occurs when a keyed object is destroyed.
		/// </summary>

		static public event EventHandler KeyedObjectDestroyed;

		/// <summary>
		/// The keyed objects list.
		/// </summary>

		static List<IKeyed> _blankKeyList = new List<IKeyed>();

		/// <summary>
		/// The key list.
		/// </summary>

		static List<IKeyed> _keyList = new List<IKeyed> ();

		/// <summary>
		/// The keyed objects dictionary.
		/// </summary>

		static Dictionary<string, IKeyed> _keyedDictionary = new Dictionary<string, IKeyed>(); 


		static public List<IKeyed> KeyList { get { return _keyList; } }

		public event EventHandler Destroyed;

		/// <summary>
		/// Gets all keyed objects in a list
		/// </summary>
		/// <value>The keyed objects.</value>

		static public Dictionary<string, IKeyed> KeyedDictionary { get 
			{ 
				UpdateBlankKeysList();
				return _keyedDictionary; 
			} 
		}

		[SerializeField] protected string _key;

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>

		public abstract string Key { get;}


		static public void ClearLists() {
		
			_blankKeyList.Clear ();
			_keyList.Clear ();
			_keyedDictionary.Clear ();
		
		}

		/// <summary>
		/// Keies the exists.
		/// </summary>
		/// <returns><c>true</c>, if exists was keyed, <c>false</c> otherwise.</returns>
		/// <param name="key">Key.</param>
		
		static public bool KeyExists(string key) {

			// Search Key List
			foreach (IKeyed keyed in _keyList) {
				
				if(keyed.Key == key) {
					return true;
				}
				
			}
			
			/*
			try {
				IKeyed iKeyed = _keyedDictionary [key];
				if(iKeyed != null) {
					
					return true;
					
				}
			}
			catch {
				Debug.LogWarning ("Item not found in dictionary");
			}

			// TODO: I should be able to reverse the order of the other two

			// Search Key List
			foreach (IKeyed keyed in _keyList) {
				
				if(keyed.Key == key) {
					return true;
				}
				
			}
			
			// Search Blank Keys
			foreach (IKeyed keyed in _blankKeyList) {
				
				if(keyed.Key == key) {
					return true;
				}
				
				
			}*/
			
			return false;
			
		}

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <returns>The key.</returns>
		/// <param name="key">Key.</param>


		static public IKeyed GetKey(string key) {
	
			try {
				IKeyed iKeyed = _keyedDictionary [key];
				if(iKeyed != null) {
				
					return iKeyed;

				}
			}
			catch {
//				Debug.Log ("Could not retrieve item from dictionary");
			}


			// Search Key List
			foreach (IKeyed keyed in _keyList) {
				
				if(keyed.Key == key) {
					return keyed;
				}
				
			}

			// Search Blank Keys
			// TODO: This should come first
			foreach (IKeyed keyed in _blankKeyList) {
			
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

//			Debug.Log ("Key Created");
			AddKey (this);

			/*
			if (this.Key == null || this.Key == "") {

				// If this was created before the key was blank then add this to the list of blank keys to be handled later
				_blankKeyList.Add (this);
				_keyList.Add(this);
			}
			else {
			
				// Add to the dictionary
				AddKeyedToDictionary(this);

			}*/

			// Raised the keyed object created event
			OnKeyedObjectCreated (this);

		}

		static public void AddKey(IKeyed item) {

			if (item == null) {
				Debug.LogWarning("Attempting to add key but item is null");
								return;}

			_keyList.Add (item);

			if (item.Key == null || item.Key == "") {
			
				_blankKeyList.Add(item);
			}
			else {
				AddKeyedToDictionary(item);
			}
		
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
				
					// No big deal, I don't expect to be able to cast to all types
				}


			}

			return list;
		}

		/// <summary>
		/// Releases unmanaged resources and performs other cleanup operations before the <see cref="WhatPumpkin.Keyed"/> is
		/// reclaimed by garbage collection.
		/// </summary>

		~Keyed() {
		
			//Debug.Log ("Key Destroyed");
			//Debug.Log ("Key Destroyed: " + Key);

			if (Destroyed != null) {
			//	Debug.Log ("Invoking Destroyed");
				Destroyed(this, null);
			}

			OnKeyedObjectDestroyed (this); // TODO: May be able to remove this


		}

		/// <summary>
		/// Raises the keyed object created event.
		/// </summary>

	
		static public void OnKeyedObjectCreated(IKeyed keyedObject) {

			ArgumentReceiver.TryInit ();
			if (KeyedObjectCreated != null) {KeyedObjectCreated (keyedObject, null);}

			// Register to the destroyed event to handle objects being destroyed, making sure they are removed from all lists
			keyedObject.Destroyed += HandleDestroyed;
		}

		/// <summary>
		/// Raises the keyed object destroyed event.
		/// </summary>

		static public void OnKeyedObjectDestroyed(IKeyed keyedObject) {
		
			if(KeyedObjectDestroyed != null) {KeyedObjectDestroyed(keyedObject, null);} // TODO: I may be able to remove this


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
		/// Clears the blank keys list of keys that are no longer blank and then add them to the dictionary.
		/// </summary>

		static void UpdateBlankKeysList() {
		
			if (_blankKeyList != null && _blankKeyList.Count > 0) {
			
				foreach(IKeyed iKeyedObject in _blankKeyList) {
				
					// If the key is no longer blank then add to the dictionary and remove from this list
					if(iKeyedObject.Key != null && iKeyedObject.Key != "") {
					
						AddKeyedToDictionary(iKeyedObject);
						_blankKeyList.Remove(iKeyedObject);
					}

				}
			}

		}

		/// <summary>
		/// Adds the keyed to dictionary.
		/// </summary>
		/// <param name="keyed">Keyed.</param>

		static void AddKeyedToDictionary(IKeyed keyed) {

			if(!_keyedDictionary.ContainsKey(keyed.Key)) {
				_keyedDictionary.Add(keyed.Key, keyed);
			}
			else {
				//Debug.LogWarning("Found Duplicate Key: " + keyed.Key);
			}
		
		}

		/// <summary>
		/// Handles destroyed keyed objects.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>

		static void HandleDestroyed (object sender, EventArgs e)
		{


			IKeyed iKeyed;

			
		


			try {
				iKeyed = (IKeyed)sender;
			}
			catch (InvalidCastException invalidcastexception) {
			
				Debug.Log ("Could not cast the object " + sender + " to a keyed object.");
				throw(invalidcastexception);

			}

			if (iKeyed != null) {

				// Remove from dictionary
			//	Debug.Log ("Key not null");

				try {
				
				//	Debug.Log ("Attempting To Remove from dictionary");
					_keyedDictionary.Remove(iKeyed.Key);
				
				}
				catch{
				
				//	Debug.Log ("Could not remove " + iKeyed.Key + " from the dictionary");
				}

				if(_keyList.Contains(iKeyed)) {
					_keyList.Remove(iKeyed);
			//		Debug.Log (iKeyed.Key + " removed");
				}
				if(_blankKeyList.Contains(iKeyed)) {_blankKeyList.Remove(iKeyed);}




			}
		}




	}
}