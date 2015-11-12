#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - February 18, 2014
#endregion 

#region using
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin.NodeControllers {

	public class ControlledProp : NodeControlledObject  {

		// TODO: Can get rid of this

		/// <summary>
		/// Invokes the control method.
		/// </summary>
		/// <param name="methodName">Method name.</param>

		protected override void InvokeControlMethod(string methodName) {

			if (methodName != "") {
			
				SendMessage(methodName);
			}

		
		}

		void Enable() {
		
			this.gameObject.SetActive (true);
		
		}

		void Disable() {
			this.gameObject.SetActive (false);
		}


		/// <summary>
		/// Hide this instance.
		/// </summary>

		void Hide() {

			if (this.GetComponent<Renderer>() != null) {
				GetComponent<Renderer>().enabled = false;
			}
		
		}

		/// <summary>
		/// Show this instance.
		/// </summary>

		void Show() {

			if (this.GetComponent<Renderer>() != null) {
				GetComponent<Renderer>().enabled = true;
			}

		
		}


	}
}