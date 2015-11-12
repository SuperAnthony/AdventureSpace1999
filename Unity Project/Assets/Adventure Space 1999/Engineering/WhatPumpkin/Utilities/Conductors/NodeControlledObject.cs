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


	/// <summary>
	/// Node controlled object.
	/// </summary>


	public abstract class NodeControlledObject : MonoBehaviour {

		public enum ControllingVariable {XRot,YRot,ZRot,XPos,YPos,ZPos,XScale,YScale,ZScale}

		#region fields


		[SerializeField] protected ControllingVariable _controllingVariable = ControllingVariable.XPos;
	
		/// <summary>
		/// The object that is controlling this instance
		/// </summary>

		[SerializeField] protected Transform _controller; 

		/// <summary>
		/// The control switches.
		/// </summary>

		[SerializeField] protected ControlSwitch [] _controlSwitches;

		/// <summary>
		/// The _previous value.
		/// </summary>

		float _previousValue = 0F;


		#endregion

		#region properties


		float CurrentValue {
				get { 
			
				// TODO: All
				if(_controller != null) {
			
					if(_controllingVariable == ControllingVariable.XPos) {
						return _controller.transform.position.x;
					}

				}

				return 0F;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="WhatPumpkin.NodeControllers.NodeControlledObject"/> value changed.
		/// </summary>
		/// <value><c>true</c> if value changed; otherwise, <c>false</c>.</value>

		bool ValueChanged { get { return _previousValue != CurrentValue; } }

		#endregion



		#region methods


		protected void Update() {

			if (ValueChanged) {

				InvokeControlMethod(GetControlMethodName(CurrentValue));
			}

			UpdatePreviousValue ();
		
		}

		/// <summary>
		/// Invokes the control method.
		/// </summary>
		/// <param name="methodName">Method name.</param>

		protected abstract void InvokeControlMethod(string methodName);

		/// <summary>
		/// Gets the control method name by value.
		/// </summary>
		/// <returns>The control method.</returns>
		/// <param name="value">Value.</param>

		protected string GetControlMethodName(float value) {
		
			foreach (ControlSwitch controlSwitch in _controlSwitches) {
				if(controlSwitch.Value == value) {

					return controlSwitch.MethodName;
				
				}
			}

			return "";
		
		}

		/// <summary>
		/// Updates the previous value.
		/// </summary>

		void UpdatePreviousValue() {
		
			_previousValue = CurrentValue;
		
		}

		#endregion


	}
}
