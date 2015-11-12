#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - March 27, 2015
#endregion 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WhatPumpkin.Actions;


namespace WhatPumpkin {

	/// <summary>
	/// Argument receivers collection.
	/// </summary>

	public class ArgumentReceiversCollection : MonoBehaviour {

		/// <summary>
		/// The argument receiver types.
		/// </summary>

		static IArgumentReceiver [] _argumentReceiverTypes = new IArgumentReceiver [] {new SetNarAllowedArgs(), new  ActivateAllowedArgs (), new TalkAllowedArgs(),
																						new MoveToTarAllowedArgs(), new DisableAllowedArgs(), new EnableAllowedArgs(),
																						new SetVarAllowedArgs ()};  
	
		/// <summary>
		/// Gets the type of the argument receiver.
		/// </summary>
		/// <returns>The argument receiver type.</returns>
		/// <param name="name">Name.</param>

		static public IArgumentReceiver GetArgumentReceiverType(string name) {
		
			foreach (IArgumentReceiver argumentReceiver in _argumentReceiverTypes) {
			
				if(argumentReceiver.Name == name) {
					return argumentReceiver;
				}
			}

			return null;

		}

	}

	/// <summary>
	/// Interface for type that can receive arguments.
	/// </summary>

	public interface IArgumentReceiver {

		string Name { get; }
	

		/// <summary>
		/// Gets the allowed arguments.
		/// </summary>
		/// <returns>The allowed arguments.</returns>
		/// <param name="atArgument">At which argument in the signature.</param>

		IList<IKeyed> GetAllowedArguments (int atArgument);

		/// <summary>
		/// Determines whether atArgument is at valid argument number.
		/// </summary>

		bool IsValidArgumentNumber (int atArgument); 

	}

	/// <summary>
	/// SetNar's allowed arguments.
	/// </summary>

	public class SetNarAllowedArgs : IArgumentReceiver {

		/// <summary>
		/// Gets the name based on the action type.
		/// </summary>
		/// <value>The name.</value>

		public string Name { get { return SetNarratorText.NAME;} }

		/// <summary>
		/// Gets the allowed arguments.
		/// </summary>
		/// <returns>The allowed arguments.</returns>
		/// <param name="atArgument">At which argument in the signature.</param>

		public IList<IKeyed> GetAllowedArguments (int atArgument) {
		
			List<IKeyed> allowedArguments = new List<IKeyed>();

			if (atArgument == 1) {

				// Add all of the allowed arguments to the allowed arguments list
				if(GameController.MessageManager != null && GameController.MessageManager.NarratorTextCollection != null) {

					foreach(IKeyed keyedObj in GameController.MessageManager.NarratorTextCollection) {
						allowedArguments.Add(keyedObj);
					}

				}

				// Add all of the allowed arguments to the allowed arguments list
				if(GameController.MessageManager != null && GameController.MessageManager.BarkTextCollection != null) {
					
					foreach(IKeyed keyedObj in GameController.MessageManager.BarkTextCollection) {
						allowedArguments.Add(keyedObj);
					}
					
				}
			
			}

			// Return the abstracted list
			return (IList<IKeyed>)allowedArguments;
		
		}

		/// <summary>
		/// Determines whether atArgument is at valid argument number.
		/// </summary>

		public bool IsValidArgumentNumber(int atArgument) {
		
			// Only one argument is allowed

			return atArgument == 1;
		
		}

		public SetNarAllowedArgs() {

			// Constructor
		
		}

	}

	/// <summary>
	/// Activate's allowed arguments.
	/// </summary>

	public class ActivateAllowedArgs : IArgumentReceiver {
		
		/// <summary>
		/// Gets the name based on the action type.
		/// </summary>
		/// <value>The name.</value>
		
		public string Name { get { return Activate.NAME;} }
		
		/// <summary>
		/// Gets the allowed arguments.
		/// </summary>
		/// <returns>The allowed arguments.</returns>
		/// <param name="atArgument">At which argument in the signature.</param>
		
		public IList<IKeyed> GetAllowedArguments (int atArgument) {
			// Return all game objects
			return GameObject.FindObjectsOfType<Entities.Entity> ();
		}
		
		/// <summary>
		/// Determines whether atArgument is at valid argument number.
		/// </summary>
		
		public bool IsValidArgumentNumber(int atArgument) {
			// User may have as many arguments as desired
			return true;
		}
		
		public ActivateAllowedArgs() {}
		
	}

	/// <summary>
	/// Talk's allowed arguments.
	/// </summary>

	public class TalkAllowedArgs : IArgumentReceiver {
		
		/// <summary>
		/// Gets the name based on the action type.
		/// </summary>
		/// <value>The name.</value>
		
		public string Name { get { return Talk.NAME;} }
		
		/// <summary>
		/// Gets the allowed arguments.
		/// </summary>
		/// <returns>The allowed arguments.</returns>
		/// <param name="atArgument">At which argument in the signature.</param>
		
		public IList<IKeyed> GetAllowedArguments (int atArgument) {
			// Return all game objects
			return GameObject.FindObjectsOfType<Entities.Entity> ();
		}
		
		/// <summary>
		/// Determines whether atArgument is at valid argument number.
		/// </summary>
		
		public bool IsValidArgumentNumber(int atArgument) {
			// User may have as many arguments as desired
			return true;
		}
		
		public TalkAllowedArgs() {}
		
	}

	/// <summary>
	/// MoveToTar's allowed arguments.
	/// </summary>
	
	public class MoveToTarAllowedArgs : IArgumentReceiver {
		
		/// <summary>
		/// Gets the name based on the action type.
		/// </summary>
		/// <value>The name.</value>
		
		public string Name { get { return MoveToTarget.NAME;} }
		
		/// <summary>
		/// Gets the allowed arguments.
		/// </summary>
		/// <returns>The allowed arguments.</returns>
		/// <param name="atArgument">At which argument in the signature.</param>
		
		public IList<IKeyed> GetAllowedArguments (int atArgument) {
			// Return all game objects
			return GameObject.FindObjectsOfType<Sgrid.Markers.Target> ();
		}
		
		/// <summary>
		/// Determines whether atArgument is at valid argument number.
		/// </summary>
		
		public bool IsValidArgumentNumber(int atArgument) {
			// User may have as many arguments as desired
			return true;
		}
		
		public MoveToTarAllowedArgs() {}
		
	}

	/// <summary>
	/// Disable's allowed arguments.
	/// </summary>

	public class DisableAllowedArgs : IArgumentReceiver {
		
		/// <summary>
		/// Gets the name based on the action type.
		/// </summary>
		/// <value>The name.</value>
		
		public string Name { get { return Disable.NAME;} }
		
		/// <summary>
		/// Gets the allowed arguments.
		/// </summary>
		/// <returns>The allowed arguments.</returns>
		/// <param name="atArgument">At which argument in the signature.</param>
		
		public IList<IKeyed> GetAllowedArguments (int atArgument) {
			// Return all game objects
			return GameObject.FindObjectsOfType<Entities.Entity> ();
		}
		
		/// <summary>
		/// Determines whether atArgument is at valid argument number.
		/// </summary>
		
		public bool IsValidArgumentNumber(int atArgument) {
			// User may have as many arguments as desired
			return true;
		}
		
		public DisableAllowedArgs() {}
		
	}

	/// <summary>
	/// Enable's allowed arguments.
	/// </summary>

	public class EnableAllowedArgs : IArgumentReceiver {
		
		/// <summary>
		/// Gets the name based on the action type.
		/// </summary>
		/// <value>The name.</value>
		
		public string Name { get { return Enable.NAME;} }
		
		/// <summary>
		/// Gets the allowed arguments.
		/// </summary>
		/// <returns>The allowed arguments.</returns>
		/// <param name="atArgument">At which argument in the signature.</param>
		
		public IList<IKeyed> GetAllowedArguments (int atArgument) {
			// Return all game objects
			return GameObject.FindObjectsOfType<Entities.Entity> ();
		}
		
		/// <summary>
		/// Determines whether atArgument is at valid argument number.
		/// </summary>
		
		public bool IsValidArgumentNumber(int atArgument) {
			// User may have as many arguments as desired
			return true;
		}
		
		public EnableAllowedArgs() {}
		
	}

	/// <summary>
	/// Set variable's allowed arguments.
	/// </summary>

	public class SetVarAllowedArgs : IArgumentReceiver {
		
		/// <summary>
		/// Gets the name based on the action type.
		/// </summary>
		/// <value>The name.</value>
		
		public string Name { get { return SetVar.NAME;} }
		
		/// <summary>
		/// Gets the allowed arguments.
		/// </summary>
		/// <returns>The allowed arguments.</returns>
		/// <param name="atArgument">At which argument in the signature.</param>
		
		public IList<IKeyed> GetAllowedArguments (int atArgument) {
			// Return the list of variables
			//return (IList<IKeyed>)GameController.Instance.GameVariables;
			return null;
		}
		
		/// <summary>
		/// Determines whether atArgument is at valid argument number.
		/// </summary>
		
		public bool IsValidArgumentNumber(int atArgument) {
			// TODO:
			return true;
		}
		
		public SetVarAllowedArgs() {}
		
	}
}
