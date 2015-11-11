#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - January 22, 2015
#endregion 

#region using 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Reflection;
#endregion

namespace WhatPumpkin.Conductors {

	[CustomEditor(typeof(Conductor))]
	
	public class CustomConductorInspector : Editor {

		/// <summary>
		/// The selected component.
		/// </summary>

		int _selectedComponent;


		/// <summary>
		/// The selected method.
		/// </summary>

		int _selectedMethod;


		/// <summary>
		/// Gets the conductable element from a list of conductables.
		/// </summary>
		/// <returns>The conductable element.</returns>
		/// <param name="component">Component.</param>
		/// <param name="conductableList">Conductable list.</param>

		int GetConductableElement(IConductable conductable, IConductable [] conductableList) {

			for(int i = 0; i < conductableList.Length; i++) {
				if(conductable == conductableList[i]) {
					return i;
				}
			}

			return 0;
		}

		/// <summary>
		/// Gets the list of conductables attached.
		/// </summary>
		/// <returns>The conductable list.</returns>
		/// <param name="conductor">Conductor.</param>

		string [] GetComponentList(Conductor conductor) {

			// Get all of the components of the type component
			//IConductable [] _components = conductor.GetComponents(typeof(IConductable)) as IConductable;

			IConductable [] _conductables;
			string [] componentList;

			// Get list of conductables
			_conductables = GetConductablesFromComponentList (conductor.GetComponents<Component> ());


			if(_conductables != null) {
				// Get all component names
				componentList = new string[_conductables.Length];
				
				for(int i = 0; i < _conductables.Length; i++) {
					componentList[i] = _conductables[i].ToString();
				}
			
				return componentList;
			
			}
			else {

				// TODO: This is temp
				componentList = new string[1];

				componentList[0] = (conductor.GetComponent(typeof(IConductable)) as IConductable).ToString();
			
				return componentList;

			}

			// Otherwise
			/*
			componentList = new string[1];
			componentList [0] = "Add component that implements IConductable";

			return componentList;*/


		}

		/// <summary>
		/// The inspector gui 
		/// </summary>

		public override void OnInspectorGUI () {
			
			// Draw the default inspector
			DrawDefaultInspector ();

			// Get the selected conductor
			Conductor conductor = Selection.activeGameObject.GetComponent<Conductor> ();
			
			// Draw the component select input
			DrawComponentSelectInput (conductor);

			// Draw the method input
			//DrawMethods (conductor.GetComponents<Component>()[_selectedComponent], conductor);
			
			
			
		}

		/// <summary>
		/// Draws the methods.
		/// </summary>
		/// <param name="selectedComponent">Selected component.</param>

		void DrawMethods(Component selectedComponent, Conductor conductor) {
		

			// Get the method list
			string [] methodsList = new string[selectedComponent.GetType ().GetMethods (BindingFlags.Public|BindingFlags.Instance|BindingFlags.DeclaredOnly).Length]; 

			for (int i = 0; i < methodsList.Length; i++) {

				methodsList[i] = selectedComponent.GetType ().GetMethods (BindingFlags.Public|BindingFlags.Instance|BindingFlags.DeclaredOnly)[i].Name;

			}

			_selectedMethod = EditorGUILayout.Popup (_selectedMethod, methodsList);

			// Designate the method for the conductor
			conductor.DesignatedMethodName = methodsList [_selectedMethod];

		
		}

		/// <summary>
		/// Gets the conductables from component list.
		/// </summary>
		/// <returns>The conductables from component list.</returns>
		/// <param name="components">Components.</param>


		IConductable [] GetConductablesFromComponentList(Component [] components) {
		
			List<IConductable> conductablesList = new List<IConductable>();

			foreach (Component component in components) {
			
				IConductable conductable = component as IConductable;

				if(conductable != null) {
					conductablesList.Add (conductable);
				}

			}

			// Store in temp array and pass values - yea, there are many better ways to do this, but it works

			IConductable [] conductableArray = new IConductable[conductablesList.Count];
		
			for (int i = 0; i < conductablesList.Count; i++) {
				conductableArray[i] = conductablesList[i];
			}

			return conductableArray;

		}

		/// <summary>
		/// Draws the component select input drop down menu.
		/// </summary>

		void DrawComponentSelectInput(Conductor conductor) {

			EditorGUILayout.BeginHorizontal ();

			if (conductor != null) {

				// Get the component list from the conductor
				string [] componentList = GetComponentList(conductor);

				GUILayout.Label("Select Component: ");


				// Draw the component selection
				_selectedComponent = GetConductableElement(conductor.ConductedActor, GetConductablesFromComponentList(conductor.GetComponents<Component>()));
				_selectedComponent = EditorGUILayout.Popup(_selectedComponent, componentList);
				
			}

			EditorGUILayout.EndHorizontal ();
		
		}


	}
}
