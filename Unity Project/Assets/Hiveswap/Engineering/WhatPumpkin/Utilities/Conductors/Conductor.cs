#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - April 1, 2014
#endregion 

#region using
using UnityEngine;
using System.Collections;
#endregion

namespace WhatPumpkin.Conductors {

	public class Conductor : MonoBehaviour {

		public enum ConductingVariable {XRot,YRot,ZRot,XPos,YPos,ZPos,XScale,YScale,ZScale}

		#region delegates and events

		// NOTE: Using delegates in place of references or points since I cannot direclty reference a value type
		
		delegate float ReturnValue();

		/// <summary>
		/// The conductor value.
		/// </summary>

		ReturnValue conductorValue;

		/// <summary>
		/// Occurs when value changed.
		/// </summary>

		public event System.Action ValueChanged;

		#endregion

		#region fields

		/// <summary>
		/// The variable in the conductor doing the conducting.
		/// </summary>

		[SerializeField] ConductingVariable _conductingVariable = ConductingVariable.XPos;
		
		/// <summary>
		/// The node that in charge of the conducting 
		/// </summary>
		
		[SerializeField] protected Transform _conductingNode; 

		/// <summary>
		/// The conductor value in the previous frame.
		/// </summary>

		float _previousValue = 0F;
		
		/// <summary>
		/// Gets a value indicating whether this instance has value changed.
		/// </summary>
		/// <value><c>true</c> if this instance has value changed; otherwise, <c>false</c>.</value>
		
		bool HasValueChanged { get { return _previousValue != conductorValue(); } }

		/// <summary>
		/// The conducted actor.
		/// </summary>

		public IConductable ConductedActor;


		/// <summary>
		/// Gets or sets the name of the designated method.
		/// The designated method is the method that is invoked by the conducted object whenever a change occurs to the conductors conductorValue
		/// </summary>
		/// <value>The name of the designated method.</value>

		public string DesignatedMethodName { get; set; }

		#endregion

		#region methods
	
		/// <summary>
		/// When this instance starts.
		/// </summary>


		void Start() {SetConductingVariable ();}

		/// <summary>
		/// Sets the conducting variable. The control node may conduct with any of the transform values
		/// </summary>

		void SetConductingVariable() {
		
			switch (_conductingVariable) {
			
			case ConductingVariable.XPos:
				conductorValue = GetXPos;
				break;

			case ConductingVariable.YPos:
				conductorValue = GetYPos;	
				break;

			case ConductingVariable.ZPos:
				conductorValue = GetZPos;	
				break;

			case ConductingVariable.XRot:
				conductorValue = GetXRot;	
				break;

			case ConductingVariable.YRot:
				conductorValue = GetYRot;	
				break;

			case ConductingVariable.ZRot:
				conductorValue = GetZRot;
				break;

			case ConductingVariable.XScale:
				conductorValue = GetXScale;	
				break;

			case ConductingVariable.YScale:
				conductorValue = GetYScale;	
				break;

			case ConductingVariable.ZScale:
				conductorValue = GetZScale;
				break;
			default:	
				conductorValue = GetXPos;
				break;


			}
		}

		/// <summary>
		/// Raises the value changed event.
		/// </summary>

		void OnValueChanged() {
		
			// Invoke the conducted objects method (the one designnated to be invoked on change)
			InvokeConductedMethod();

			// If any instance subscribed to the value change method then invoke it
			if (ValueChanged != null) {
				ValueChanged.Invoke();
			}


		
		}

		/// <summary>
		/// On Update.
		/// </summary>

		protected void Update() {

			// Check to see if the value has changed this frame
			if (HasValueChanged) {OnValueChanged();}

			// Keep track of the old value
			UpdatePreviousValue ();
			
		}

		/// <summary>
		/// Keep track of the previous value in order to determine whether or not the value has changed.
		/// </summary>
		
		void UpdatePreviousValue() {
			
			_previousValue = conductorValue();
			
		}

		/// <summary>
		/// Invokes the conducted object's designated method.
		/// </summary>


		void InvokeConductedMethod() {
		
			ConductedActor.ReceiveConduct(conductorValue());

		}

		/// <summary>
		/// Gets the X position.
		/// </summary>
		/// <returns>The X position.</returns>
		float GetXPos() {return this.transform.position.x;}

		/// <summary>
		/// Gets the Y position.
		/// </summary>
		/// <returns>The Y position.</returns>

		float GetYPos() {return this.transform.position.y;}

		/// <summary>
		/// Gets the Z position.
		/// </summary>
		/// <returns>The Z position.</returns>

		float GetZPos() {return this.transform.position.z;}

		/// <summary>
		/// Gets the X rot.
		/// </summary>
		/// <returns>The X rot.</returns>

		float GetXRot() {return this.transform.rotation.x;}

		/// <summary>
		/// Gets the Y rot.
		/// </summary>
		/// <returns>The Y rot.</returns>

		float GetYRot() {return this.transform.rotation.y;}

		/// <summary>
		/// Gets the Z rot.
		/// </summary>
		/// <returns>The Z rot.</returns>

		float GetZRot() {return this.transform.rotation.z;}

		/// <summary>
		/// Gets the X scale.
		/// </summary>
		/// <returns>The X scale.</returns>

		float GetXScale() {return this.transform.localScale.x;}

		/// <summary>
		/// Gets the Y scale.
		/// </summary>
		/// <returns>The Y scale.</returns>

		float GetYScale() {return this.transform.localScale.y;}

		/// <summary>
		/// Gets the Z scale.
		/// </summary>
		/// <returns>The Z scale.</returns>

		float GetZScale() {return this.transform.localScale.z;}

		#endregion

	}
}
