// Date 1/14/15

#region using
using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using WhatPumpkin.HiveSwap;
using WhatPumpkin.ScriptingLanguage;
using WhatPumpkin.HiveSwap.GameControl;
#endregion

// TODO: This is too reliant on the scene manager

// TODO: This works but it's messy - seriously Ant, clean it up

namespace WhatPumpkin.Data.XML {

	/// <summary>
	/// XML parser. Used to locate data in an xml node.
	/// </summary>

	// Change to something that represents that this is a parser for or xml google spreadsheets format
	// TODO: Rename - GoogleDocToXMLFormatParser

	public class GoogleDocToXMLFormatParser {

		#region static fields
		static List<string> _variableNames = new List<string>();
		#endregion

		#region static methods

		static public void CollectVariableNames(XmlNode xmlNode) {
		
			// Clear the variable names list
			_variableNames.Clear ();

			// Get the data from the first node called row 
			XmlNode firstRow = XMLParser.FindNodeByName (xmlNode, "row");


			// Search through each child node of the first row to get 
			foreach (XmlNode childNode in firstRow) {
			

				// Add the variable name to the list
				_variableNames.Add (childNode.InnerXml);
			
			}
		}


		/// <summary>
		/// Parse the specified receiver and xmlNode.
		/// </summary>
		/// <param name="receiver">Receiver.</param>
		/// <param name="xmlNode">Xml node.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>

		static public void Parse<T>(out T [] receiver, XmlNode xmlNode) where T : new () {
		
			// Generate a list of variable names based on the xml node
			CollectVariableNames (xmlNode);

			// Get a list of fields from the type
			Type type = typeof(T);
			FieldInfo[] fieldCollection = type.GetFields (BindingFlags.Public | 
			                                         BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);

			// Assign the new receiver 
			receiver = new T[XMLParser.FindNodesByName(xmlNode, "row").Count - 1];

			//Debug.Log ("Receiver length: " + receiver.Length);

			// Get all the rows
			int rowNumber = 0;
			foreach (XmlNode row in XMLParser.FindNodesByName(xmlNode, "row")) {
			
				// Skip the first row because that row only contains the keys
				// Debug.Log (row.Name);

				if(rowNumber > 0) {

					// Create a new item type for each row
					T item = new T();

					// Get the row's child nodes which will contain our variable data
					int columnNumber = 0;
					foreach(XmlNode cell in row.ChildNodes) {
						//Debug.Log (cell.Name + " " + " " + _variableNames[columnNumber] + ": " + cell.InnerXml);

						// Try to add the value to the new item
						FieldInfo fieldInfo = GetFieldInfoByName(_variableNames[columnNumber], fieldCollection);

						SetFieldValue<T>(fieldInfo, item, cell.InnerXml.ToString(), fieldCollection, columnNumber);


						columnNumber++;
					}

					// Add the item to the reciever list
					receiver[rowNumber - 1] = item; // The row number minus 1 corresponds to proper index
			
				}
				rowNumber++;
			}
		
		}


		static FieldInfo GetSubFieldInfo<T>(FieldInfo fieldInfo, T item, FieldInfo [] fieldCollection, int column ) {
		
			string subObjectFieldName = "";
			// Get new field info
			string fieldName = Scripting.GetLeftOfIndicator(_variableNames[column], '-');
			fieldInfo = GetFieldInfoByName(fieldName, fieldCollection);
			
			// Get the sub object field info
			subObjectFieldName = _variableNames[column];
			subObjectFieldName = subObjectFieldName.Replace(fieldName+'-', "");


			FieldInfo subField = item.GetType ().GetField (subObjectFieldName, BindingFlags.Public | 
			                                               BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);

			//Debug.Log (subField.Name);

			return subField;

		}


		static bool SetFieldValue<T>(FieldInfo fieldInfo, T item, object val, FieldInfo [] fieldCollection, int column) {

//			Debug.Log ("Set Field Value");

			string fieldName = "";
			bool hasSubfield = false;


			if(fieldInfo == null) {
				fieldName = Scripting.GetLeftOfIndicator(_variableNames[column], '-');
				fieldInfo = GetFieldInfoByName(fieldName, fieldCollection);
				// If we got field info by peforming this then it should certainly mean
				// that there is a subfield
				hasSubfield = true;

			}


			// Attempt to add to the field if the val is not blank
			if(val.ToString() != "")
			{
				// These may not exist

				//FieldInfo subFieldInfo = null;

				// Check for the type of field (first check to see if the type is a collection)
				try{ // Try to handle this as a collection
					// Is this a list collection

					if( fieldInfo != null  && fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(List<>)) {

						//Debug.Log ("******Generic List******");
						//Debug.Log ("Try");
					
						// Get the list info
						IList list = (IList)fieldInfo.GetValue(item);
						// Get the element type in the list
						Type elementType = (Type)fieldInfo.FieldType.GetGenericArguments()[0];
						

						

						if(elementType == typeof(System.String)) { // is it a string

							//Debug.Log ("Element is of type string");
						
							try {
								list.Add(val.ToString());
							}
							catch (Exception e) {
								Debug.LogError ("Could not add string to the list, did you create a new list?");
								//Debug.LogError ("Could not add string to list");
								//Debug.LogException(e);
							}
						}
						if (HasTypeInHierarchy(elementType, typeof(UnityEngine.Component))) { // is the object a type of component


							// Find the object in the scene and add to the list using the correct component type
							list.Add (GameController.SceneManager.FindSceneObject( val.ToString()).GetComponent(elementType));

							// TODO: Handle when the object can't be found - do we create a new one of this type - I don't think that should ever be the case
							
						}
						else { 



							// Check to see if elements exists, if not create
							if(list.Count == 0){AddNewItemToList(list, elementType);}

							//Debug.Log ("Has sub field? " + hasSubfield);

							// If this field has a sub field then get it
							if(hasSubfield) {fieldInfo = GetSubFieldInfo(fieldInfo, list[list.Count - 1], fieldCollection, column);}
							// Now we should certainly have the correct field info
							if(fieldInfo != null) {

								// If the value is not null then add a new item to the list
								if(fieldInfo.GetValue(list[list.Count - 1]) != null) {

									AddNewItemToList(list, elementType);
									// AddNewItemToList(list, val.ToString());
								
								}

								fieldInfo.SetValue(list[list.Count - 1], val.ToString());	


							
							}
							else {
								//Debug.LogError("Could not locate a field or sub field when parsing list data");
								return false;
							}
						}

						// Now that the element is added, we can add the value to the field



						//Debug.Log ("**********************************************");


					}
				}
				catch(ArgumentOutOfRangeException e) {
					Debug.LogException(e);
				}
				catch(InvalidOperationException e){

					//Debug.Log ("Not a collection - continue");
					//Debug.LogException(e);
					// Does the field exist

					// TODO: Handle this properly

					if(fieldInfo == null) { 



						// If not then it is likely that this field refers to a field of a field (subFieldInfo)
						fieldInfo = GetSubFieldInfo(fieldInfo, item, fieldCollection, column);
						
					} // End - Does the field exist *********************//


					try {				

						// Is this a component type
						if (HasTypeInHierarchy(fieldInfo.FieldType, typeof(UnityEngine.Component))) { // is the object a type of component
							

							fieldInfo.SetValue(item, GameController.SceneManager.FindSceneObject( val.ToString()).GetComponent(fieldInfo.FieldType));
							
						}
						else {
							// String value
							fieldInfo.SetValue(item, val);

							// TODO: float, int, etc.
						}
					}
					catch {
						//Debug.Log ("Could not parse " + fieldInfo.Name);
						//Debug.Log("DID NOT SET VALUE FOR: " + val.ToString());
						return false;
					}
				}
				catch(Exception e) {
					// This should not happen
					// TODO: This exception is being caught when handling narrator and verb text and I'm not sure why, though it appears to all be working
					/*
					Debug.LogError(e + "... It's likely that you need logic to handle the object type");
			
					Debug.Log ("Item: " + item.ToString());
					Debug.Log ("Value: " + val.ToString());*/
				}
			}

			return true;
		
		}

		// For adding item types

		static void AddNewItemToList(IList list, Type elementType) {
		
			// Add a new instance of the element type
			try {
				// Try to add an element to the list
				list.Add(Activator.CreateInstance(elementType));


			}
			catch (MissingMethodException e) {
				Debug.LogError("Could not add the instance to the list " + list.ToString() + " of the type " + elementType + 
				               "because " + elementType + " does not havea a default constructor which take no parameters"); 
			}
			catch (Exception e){
				// Catch exceptions if there are any
				Debug.LogError("Was unable to add a new instance of the type " + elementType + " to the list " + list.ToString());
				Debug.LogException(e);
			}
		
		}



		/// <summary>
		/// Determines if the type 'type'  has the type 'target' in the hierarchy.
		/// </summary>
		/// <returns><c>true</c> if has type in hierarchy the specified type target; otherwise, <c>false</c>.</returns>
		/// <param name="type">Type.</param>
		/// <param name="target">Target.</param>

		static bool HasTypeInHierarchy(Type type, Type target ) {
		
			// TODO: This could be a useful method that belongs some place else

			// Return true if it has the type
			if (type == target) {
				return true;
			}

			// If not and the typ has a base type, then set the current type to the base type and search again
			if (type.BaseType != null) {
			
				type = type.BaseType;
				// Check again
				return HasTypeInHierarchy(type, target);
			}


			return false;
		
		}

		// Add values

		static void AddNewItemToList(IList list, string value) {
		
			list.Add (value);

		}

		static FieldInfo GetFieldInfoByName(string name, FieldInfo [] fieldInfoCollection) {

			foreach (FieldInfo fieldInfo in fieldInfoCollection) {
			
				if(fieldInfo.Name.ToString() == name)
				{
					return fieldInfo;
				}
			
			}

			return null;

		}

		#endregion
	}

}
