#region copyright (c) 2014 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - December 21, 2014
#endregion 


#region using
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
#endregion

// TODO: A lot of these searches can be done better simply by using the split method 

namespace WhatPumpkin.ScriptingLanguage {
	
	/// <summary>
	/// Used for parsing scripts.
	/// </summary>

	public class Scripting  {

		#region constants

		const string TAG_START = "[[";
	

		const string TAG_END = "]]";



		/// <summary>
		/// The maximum number of arguments a script will allow.
		/// </summary>

		public const int MAX_ARGUMENTS = 20; 

		/// <summary>
		/// The maximum number of conditions to search for before break
		/// </summary>

		public const int MAX_CONDITIONS = 30;

		#endregion

		#region static methods

		/// <summary>
		/// Parses the escape sequences.
		/// </summary>
		/// <returns>The escape sequences.</returns>
		/// <param name="text">Text.</param>

		static public string ParseEscapeSequences(string text) {

			string parsedText = "";

			parsedText = GetVarValueFromString (text);

			return parsedText;
		}

		static string GetVarValueFromString(string text) {


			//string []  = text.Split (TAG_START.ToCharArray (), System.StringSplitOptions.RemoveEmptyEntries);
			//text.

			return "";


		}

		/// <summary>
		/// Gets the arguments from a string.
		/// </summary>
		/// <returns>The arguments.</returns>
		/// <param name="arguments">Arguments.</param>

		static public List<string>  GetArguments (string arguments) {

			List<string> args = new List<string> ();

			int maxArguments = 20;
			int nArguments = 0;

		
			while(arguments != null && arguments != "") {
			
				nArguments++;
				if(nArguments > maxArguments){break;}
				// Get the next argument
				string argument = GetLeftOfIndicator (arguments, ',');
				// Add it to the list
				args.Add(argument);
				// Remove the argument from the arguments string
				string remove = argument+','; 
		

				// Determine how many characters will need to be removed
				int length = remove.Length;

				// Will go over by one when there are no more arguments an a useless , was appended
				if(length != null && arguments != null && arguments != "" && length > arguments.Length) {
					length = arguments.Length;
				}

				if(length != null) {
					arguments = arguments.Remove(0, length);
				}

			}

			return args;

		}

		/// <summary>
		/// Are the conditions met.
		/// </summary>
		/// <returns><c>true</c>, if condition was met <c>false</c> otherwise.</returns>
		/// <param name="conditions">Conditions.</param>
	

		static public bool AreConditionsMet(string conditions) {

//			Debug.Log ("Are Conditions Met");
		
			// If there are no conditions then all conditions are met, return true
			if(conditions == null || conditions == "") {return true;}

			string [] orConditions = GetOrConditions (conditions);
			/*
			foreach (string c in orConditions) {
			
				Debug.Log("Or Condition Set: " + c);
			
			}*/

			// Search through each "or" condition and check to see if it's met
			foreach (string conditionSet in orConditions) {
			
				if(IsConditionSetMet(conditionSet)){


					//Debug.Log (conditionSet + " set returned true");
					// If any of the sets of condition sets are met then we can return true
					return true;
				}
			
			}

			//Debug.Log ("Neither Set Returned True.  Are Conditions Met Returning False");

			return false;


		}

		static bool IsConditionSetMet(string conditions) {

//			Debug.Log ("Is Condition Set Met: " + conditions);

			//  Get a list of conditions from the conditions string
			List<string> conditionList;
			conditionList = Scripting.GetConditions (conditions);
			
			// Search through the list and return false if any of the conditions are not met
			foreach (string condition in conditionList) {
				
				if(Scripting.IsConditionMet(condition) == false) {
					return false;
				}
				
			}
			
			// Since no conditions returned false then all conditions are met
			return true;
		}


		/// <summary>
		/// Determines if value represents a boolean
		/// </summary>
		/// <returns><c>true</c> if is boolean the specified value; otherwise, <c>false</c>.</returns>
		/// <param name="value">Value.</param>

		static internal bool IsBoolean(string value) {
		
			return (value.ToUpper() == "FALSE") || value.ToUpper() == "TRUE";
		
		}


		/// <summary>
		/// Gets the boolean value.
		/// </summary>
		/// <returns><c>true</c>, if boolean value was gotten, <c>false</c> otherwise.</returns>
		/// <param name="value">Value.</param>

		static public bool GetBooleanValue(string value) {
		
			return value.ToUpper () == "TRUE" || value == "1";
		
		}

		/// <summary>
		/// Determines if a condition is met using the specified string condition.
		/// </summary>
		/// <returns><c>true</c> if a condition is met using the specified condition; otherwise, <c>false</c>.</returns>
		/// <param name="condition">Condition.</param>

		static public bool IsConditionMet(string condition) {
		
			// If there is no conditions then return true
			if(condition == null || condition == ""){return true;}

			string conditionOperator = ""; // ==, !=, <, >

			// Determine the type of condition | This is very hakey 
			if(condition.Contains("==")){conditionOperator = "==";}
			else if(condition.Contains("!=")){conditionOperator = "!=";}
			else if(condition.Contains("<")){conditionOperator = "<";}
			else if(condition.Contains(">")){conditionOperator = ">";}
			else if(condition.Contains("<=")){conditionOperator = "<=";}
			else if(condition.Contains(">=")){conditionOperator = ">=";}


			if (conditionOperator == "") {
				return true;
			}

			// Get variable name from conditions
			//string variableName = "";
			string variableName = GetVariableNameFromCondition (condition, conditionOperator);
			// Get the string value from the condition
			string s_value = GetValueFromCondition (condition, conditionOperator);
			// Initialize the potential float value
			float value = 0;

		
			// Is this a unique variable

			if (UniqueVarHandling.HasUniqueQualities (variableName)) {

				//Debug.Log("Has Unique Var.");
			
				// Handle
				return UniqueVarHandling.HandleCondition(variableName, s_value);
			
			}

			//Debug.Log ("Does not have unique var");


			// Check to see if the value is a string value, number or boolean value
			if(IsBoolean(s_value)) {

				//Debug.Log("Boolean");
				// Get the boolean value from the string value
				bool bValue = GetBooleanValue(s_value);

				// Handle the boolean value
				if(conditionOperator == "=="){return bValue == DialogueLua.GetVariable(variableName).AsBool;}
				if(conditionOperator == "!="){return bValue != DialogueLua.GetVariable(variableName).AsBool;}

			}
			else if(float.TryParse(s_value, out value)) {

				// Handle number value
				if(conditionOperator == "=="){
					return value == DialogueLua.GetVariable(variableName).AsFloat;
				}
				if(conditionOperator == "!=")
				{
					Debug.Log(DialogueLua.GetVariable(variableName).AsFloat);

					return value != DialogueLua.GetVariable(variableName).AsFloat;
				}
				if(conditionOperator == "<"){return value < DialogueLua.GetVariable(variableName).AsFloat;}
				if(conditionOperator == ">"){return value > DialogueLua.GetVariable(variableName).AsFloat;}
				if(conditionOperator == "<="){return value <= DialogueLua.GetVariable(variableName).AsFloat;}
				if(conditionOperator == ">="){return value >=DialogueLua.GetVariable(variableName).AsFloat;}


				// Is the value correct?
		
			}
			else {

				// Handle string value

				if(conditionOperator == "=="){
					return s_value == DialogueLua.GetVariable(variableName).AsString;
				}
				if(conditionOperator == "!=")
				{

					return s_value != DialogueLua.GetVariable(variableName).AsString;
				}



			}

			// Default return true
			return true;

		}

		static internal bool IsNumberValue(string sValue) {
		
			float fValue = 0;
			return float.TryParse (sValue, out fValue);
		}

		static string [] GetOrConditions(string conditions) {

			string oper = "||";

			return conditions.Split(oper.ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);
		
		}

		/// <summary>
		/// Gets a list of conditions from a string.
		/// </summary>
		/// <returns>The conditions.</returns>
		/// <param name="conditions">Conditions.</param>

		static public List<string> GetConditions(string conditions) {
		
			List<string> conditionsList = new List<string>();

			// Remove any spaces from the conditions string
			if(conditions != null) {
				conditions.Replace (" ", "");
			}

			// Get every condition, when a condition is found, remove that condition from the string

			int i = 0;

			while (conditions != "") {
			
				// Add the first condition in the list by searching for the 
				// first string left of the first comma
				string condition = GetLeftOfIndicator(conditions, ','); // Change this to split
				conditionsList.Add (condition);
			
				// First try to remove the condition with the comma appended
				conditions = conditions.Replace(condition+",","");

				// If there is no comma then it should mean that it is the 
				// last condition and we will remove that as well
				conditions = conditions.Replace(condition,"");
				// Once that happens the conditions string should be blank and the 
				// loop should exit

				//Debug.Log ("Condition: " + condition);

				// Break from this loop if it loops too much
				i++;
				if(i > MAX_CONDITIONS){break;}
			}
		
			return conditionsList;
		}

		/// <summary>
		/// Gets the variable name from an assignment.
		/// This method returns the string preceeding the first coma 
		/// </summary>
		/// <returns>The variable name from assignment.</returns>
		/// <param name="assignment">Assignment.</param>

		static public string GetVariableNameFromAssignment(string assignment) {

			// Remove all spaces from the assignment
			assignment = assignment.Replace (" ", "");

			// Return the variable by getting the string left of the first comma
			return GetLeftOfIndicator (assignment, ',');

		}

		/// <summary>
		/// Gets the variable name from condition.
		/// This method returns the first string preceeding the first =
		/// </summary>
		/// <returns>The variable name from condition.</returns>
		/// <param name="condition">Condition.</param>

		static public string GetVariableNameFromCondition(string condition, string charOperator = "==") {
		
			// TODO: Use split

			// Remove all spaces from the conditions
			condition = condition.Replace (" ", "");

//			Debug.Log ("charOperator: " + charOperator);

			return GetLeftOfIndicator (condition, charOperator[0]);

		}

		/// <summary>
		/// Gets the value from assignment.
		/// </summary>
		/// <returns>The value from assignment.</returns>
		/// <param name="assignment">Assignment.</param>
		static public string GetValueFromAssignment(string assignment) {

			return assignment.Replace (GetVariableNameFromAssignment (assignment) + ",", "");


		}

		/// <summary>
		/// Gets the value from condition.
		/// </summary>
		/// <returns>The value from condition.</returns>
		/// <param name="condition">Condition.</param>
		static public string GetValueFromCondition(string condition, string conditionOperator = "==") {

			return condition.Replace (GetVariableNameFromCondition (condition,conditionOperator) + conditionOperator, "");
		
		}

		/// <summary>
		/// Gets string that is left of the indicator.
		/// </summary>
		/// <returns>The left of indicator.</returns>
		/// <param name="script">Script.</param>
		/// <param name="indicator">Indicator.</param>

		// TODO: Create a new namespace change this to internal
		// TODO: I dont need this, i should just use split

		static public string GetLeftOfIndicator(string script, char indicator) {
			
			string s = ""; // String that will be returned

			if(script != null) {

			// Search through each char in the script
			for (int i = 0; i < script.Length; i++) {
				
					// Check to see that we haven't reached the indicator, likely a comma
					if(script[i] != indicator) {
						// If we have not then append this char to the s string
						s+=script[i].ToString();
					}
					else {
						// if we have then break
						break;
					}
				}
			}
			// If there is no indicator found then it should return the entire string
			return s;
		}

		#endregion
	}
}