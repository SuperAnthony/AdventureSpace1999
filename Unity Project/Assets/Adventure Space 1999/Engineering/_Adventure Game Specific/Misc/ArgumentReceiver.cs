#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 27, 2015
#endregion 

#region using

using UnityEngine;

using System;
using System.Collections.Generic;

using WhatPumpkin.Actions;
#endregion

namespace WhatPumpkin { 

	public abstract class ArgumentReceiver  {

		#region static fields
		
		/// <summary>
		/// The collection.
		/// </summary>
		
		static  ArgumentReceiver [] _collection;
		
		/// <summary>
		/// Was this initizlied
		/// </summary>
		
		static bool _initialized = false;
		
		#endregion

		#region static properties
		
		static public bool Initialzied { get { return _initialized; } }
		
		#endregion


		#region fields

		/// <summary>
		/// Did this instance search for and add entity game objects to it's list of valid keys yet?
		/// </summary>

		bool _addedEntityGameObjects = false;

		#endregion

		#region properties

		/// <summary>
		/// Gets a value indicating whether this instance has unlimited arguments.
		/// </summary>
		/// <value><c>true</c> if this instance has unlimited arguments; otherwise, <c>false</c>.</value>

		protected abstract bool HasUnlimitedArguments {get ; }


		/// <summary>
		/// Gets the max arguments if the instance does not have unlimited arguments.
		/// </summary>
		/// <value>The max arguments.</value>

		protected abstract int MaxArguments { get; }

		/// <summary>
		/// Gets the minimum number of arguments. 1 argument by default.
		/// </summary>
		/// <value>The minimum number of arguments.</value>

		public virtual int MinArguments { get { return 1; } }

		/// <summary>
		/// Gets the valid arguments.
		/// </summary>
		/// <value>The valid arguments.</value>
		
		protected virtual List<IKeyed> ValidArguments { get {return null;}}
		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		
		public abstract string Name { get; }
		
		/// <summary>
		/// The acceptable types. In general, the order at which the type is found is the order of the argument, if a type accepts infinite arguments then the last type is used.
		/// </summary>
		
		protected virtual Type [] ValidTypes { get { return null; } }


		#endregion

		#region static methods

		/// <summary>
		/// Gets the type of the argument receiver.
		/// </summary>
		/// <returns>The argument receiver type.</returns>
		/// <param name="name">Name.</param>
		
		static public ArgumentReceiver GetArgumentReceiverType(string name) {
			
			foreach (ArgumentReceiver argumentReceiver in _collection) {
				
				if(argumentReceiver.Name == name) {
					return argumentReceiver;
				}
			}
			
			return null;
			
		}

		/// <summary>
		/// Tries to initialize this type. When initialized the Argument receiver will add Argument recievers to the collection.
		/// </summary>
		/// <returns><c>true</c>, if init was tryed, <c>false</c> otherwise.</returns>
		
		static public bool TryInit() {
			
			if (_initialized == false) {
				_collection = new ArgumentReceiver[] {new SetNarratorText (), new Activate(), new SetVar()};
				_initialized = true;
				
				return true;
			}
			
			return false;
		}

	

		#endregion



		#region methods

		/// <summary>
		/// Releases unmanaged resources and performs other cleanup operations before the
		/// <see cref="WhatPumpkin.ArgumentReceiver"/> is reclaimed by garbage collection.
		/// </summary>

		~ArgumentReceiver() {

            // Unregister Key Created and Key Destroyed events
           // Keyed.KeyEvent -= HandleKeyEvent;
			
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="WhatPumpkin.ArgumentReceiver"/> class.
		/// </summary>
		
		public ArgumentReceiver() {

            // Register Key Created and Key Destroyed events
           // Keyed.KeyEvent += HandleKeyEvent;
		
		}

		
		
		/// <summary>
		/// Gets the allowed arguments.
		/// </summary>
		/// <returns>The allowed arguments.</returns>
		/// <param name="atArgument">At argument.</param>
		
		public virtual List<IKeyed> GetAllowedArguments (int atArgument) {

			Debug.Log ("Get Allowed Arguments: " + atArgument);
			
			// TODO: Handle Multiple Arguments
			//if(!IsValidArgumentNumber(atArgument)){return null;}
			
			if(!_addedEntityGameObjects) {
				
				foreach (Sgrid.EntityInfo entity in GameObject.FindObjectsOfType<Sgrid.EntityInfo>()) {
					
					if (entity != null) { 
						
						try{
							
							foreach (Component component in entity.GetComponents<Component>()) {
								
								if(component.GetType().GetInterface(ValidTypes[0].ToString()) != null) {
									
									ValidArguments.Add((IKeyed)entity);
								}
							}
							
						}
						catch (Exception e) {
							
							Debug.LogException(e);
							
							
						}
						
						
						
					}
					
				}
				
				_addedEntityGameObjects = true;
			}
			
			
			return ValidArguments;
			
		}
		
		/// <summary>
		/// Determines whether atArgument is at valid argument number.
		/// </summary>
		
		public virtual bool IsValidArgumentNumber(int atArgument) {
			
			if (HasUnlimitedArguments) {
				
				// User may have as many arguments as desired
				return true;
			}
			
			return atArgument <= MaxArguments - 1;
		}


		/// <summary>
		/// Handles the key created.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
	
		/*
		protected virtual void HandleKeyCreated(object sender, EventArgs e) {
			
			// Make sure neccessary conditions are met
			IKeyed keyedObject = (IKeyed)sender;
			if(keyedObject == null && ValidTypes != null && ValidTypes.Length > 0){return;}
			
			// Check to see if this object implements the required type
			if(ValidTypes != null && ValidTypes.Length > 0 && keyedObject != null &&  keyedObject.GetType().GetInterface(ValidTypes[0].ToString()) != null) {
				
				ValidArguments.Add(keyedObject);
				
			}
			
		}


		
		/// <summary>
		/// Handles the key destroyed event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		
		protected virtual void HandleKeyDestroyed (object sender, EventArgs e) {
			
			// Make sure neccessary conditions are met
			IKeyed keyedObject = (IKeyed)sender;
			if(keyedObject == null && ValidTypes != null && ValidTypes.Length > 0){return;}
			
			// Check to see if this object implements the required type
			if(keyedObject.GetType().GetInterface(ValidTypes[0].ToString()) != null) {
				
				ValidArguments.Remove(keyedObject);
				
			}

			
		}*/

		#endregion





	}
}