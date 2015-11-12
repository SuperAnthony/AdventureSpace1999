#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 15, 2015
#endregion 

#region using
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin {

	/// <summary>
	/// Utilities for assisting with comon mono behaviour tasks
	/// </summary>

	public sealed class MonoBehaviorUtils {

		#region methods

		/// <summary>
		/// Returns a component attached to the sender. 
		/// If that component is not found on the sender, the method will forcibly attach that component to the sender and then retrun it.
		/// </summary>
		/// <returns>The forced component.</returns>
		/// <param name="sender">Sender.</param>
		/// <typeparam name="ComponentType">The 1st type parameter.</typeparam>

		static public ComponentType GetForcedComponent<ComponentType>(GameObject sender) where ComponentType : Component {
		
			ComponentType component = sender.GetComponent<ComponentType> ();

			if (component == null) {
			
				// Attach the component to the sender and return it
				return sender.AddComponent<ComponentType>();
			
			}

			return component;

		
		}


		// Prevent instantiation

		private MonoBehaviorUtils() {
		
		}

		#endregion

	}
}
