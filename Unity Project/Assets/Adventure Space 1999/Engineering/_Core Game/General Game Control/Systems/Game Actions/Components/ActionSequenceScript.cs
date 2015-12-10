using UnityEngine;
using System.Collections.Generic;
using WhatPumpkin.ScriptingLanguage;

namespace WhatPumpkin.Actions.Sequences {

	[System.Serializable]

	public class ActionSequenceScriptData {

	
		[SerializeField] string _conditions; // What conditions need to be met in order to perform script
		[SerializeField] string _frequency; // How frequent can this script be performed
		[TextArea(3,50)] [SerializeField] string _script; // The script that will be parsed into action sequences

		public string Conditions { get { return _conditions; } }
		public string Frequency { get { return _frequency; } }
		public string Script { get { return _script; } }


		/// <summary>
		/// Generates an action sequence from the script data and other properties.
		/// </summary>
		/// <returns>The action sequence.</returns>

		public ActionSequence GenerateActionSequence() {

			return ActionSequenceScript.GetActionSequenceFromScript<ActionSequence>(_script, _conditions, _frequency);
		}

		public ActionSequenceType GenerateActionSequence<ActionSequenceType>() 
		where ActionSequenceType : ActionSequence, new()
		{
			
			return ActionSequenceScript.GetActionSequenceFromScript<ActionSequenceType>(_script, _conditions, _frequency);
		}
	}

	/// <summary>
	/// Takes a group of text and breaks it down into an Action Sequence Type
	/// </summary>

	public class ActionSequenceScript : MonoBehaviour {

		#region constants

		/// <summary>
		/// The default separator of actions
		/// </summary>

		const string DEFAULT_ACTIONS_SEPARATOR = ";";

		/// <summary>
		/// The separator for assigning parameters to an action
		/// </summary>

		const string DEFAULT_ACTION_ASSIGNMENT_SEPARATOR = "=";

		#endregion

		#region fields

		[SerializeField] string _conditions = "";

		[SerializeField] string _frequency = "";


		/// <summary>
		/// Blocks (groupings) of action data
		/// </summary>

		[SerializeField] ActionSequenceScriptData [] _actionGroups;

		/// <summary>
		/// For debuging purposes - this shows the actionscript that is generated 
		/// </summary>

		[SerializeField] ActionSequence GeneratedAS = null;

		#endregion
	

		#region methods

		void Start() {
		
			GeneratedAS = GenerateActionSequence<ActionSequence>();

		}

		/// <summary>
		/// Generates an action sequence based on the script data.
		/// </summary>
		/// <returns>The action sequence.</returns>

		public ActionSequenceType GenerateActionSequence<ActionSequenceType>()
		where ActionSequenceType : ActionSequence, new()
		{

		
			ActionSequenceType returnSequence = new ActionSequenceType();
			returnSequence.SetProperties(_conditions,_frequency);

			foreach(ActionSequenceType actionSequence in GetActionSequences<ActionSequenceType>()) {
			
				// Get an action sequence
				ActionSequence.Combine<ActionSequenceType>(ref returnSequence, actionSequence);

			
			}

			// Return the generated sequence
			return returnSequence;

		
		}

		/// <summary>
		/// Gets a collection of action sequences based on scripts.
		/// </summary>
		/// <returns>The action sequences.</returns>

		IEnumerable<ActionSequenceType> GetActionSequences<ActionSequenceType>() 
		where ActionSequenceType : ActionSequence, new()
		{

			foreach(var item in _actionGroups) {
			
				yield return item.GenerateActionSequence<ActionSequenceType>();
			}
		

		}

		/// <summary>
		/// Gets the action sequence from script. 
		/// You may also force conditions which will be applied to each action that is generated
		/// </summary>
		/// <returns>The action sequence from script.</returns>
		/// <param name="script">Script.</param>
		/// <param name="conditions">Conditions.</param>
		/// <param name="freqeuncy">Freqeuncy.</param>

		static public ActionSequenceType GetActionSequenceFromScript<ActionSequenceType>(
			string script,
			string conditions = "",
			string freqeuncy = ""
			) where ActionSequenceType : ActionSequence, new()
		
			{
		
			// Remove all blank spaces from the script
			string cleanScript = script.Replace(" ", "");
			// Remove all line breaks
			cleanScript = cleanScript.Replace('\n'.ToString(),"");

			// Get a list of action sequences as strings
			string [] s_actionSequence = GetActions(cleanScript); 
			
			// Create a new action sequence
			// Using the general conditions and frequency defined in the field of this 
			// type
			// The actions may have separate conditions and frequency defined
			// in the parameters of this method
			ActionSequenceType actionSequence = new ActionSequenceType();

			// Add actions to the action sequence based on script
			foreach(string action_script in s_actionSequence) {

				actionSequence.AddAction(ParseActionFromString(action_script, conditions, freqeuncy));
				
			}

			// Return the sequence
			return actionSequence;

		}


		public static Action ParseActionFromString(string script, 
		                                           string conditions = "",
		                                           string frequency = "",
		                                           string separator = DEFAULT_ACTION_ASSIGNMENT_SEPARATOR) {
		
			// This should get two values on the left and right side of the separator for the assignment
			string [] values = script.Split(separator.ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);

			// Return null if values is not exactly two.
			if(values.Length != 2) { return null;}

			// Getting the actions and paramaters. A redundant step but I'll do this for clairty.
			string sActionType = values[0];
			string sParameters = values[1];

			return new Action(sActionType, sParameters, conditions, frequency);


		}

		/// <summary>
		/// Gets actions from a script using a char as a separator.
		/// </summary>
		/// <returns>The actions.</returns>
		/// <param name="script">Script.</param>

		public static string [] GetActions(string script, string separator = DEFAULT_ACTIONS_SEPARATOR) {
		
			return script.Split(separator.ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);
		}

		#endregion

		
		/*
		public static string GetPredicateFromNextCondition(string script) {
		
			// Get the first predicate
			string conditionalKeyword = "IF";
			int ifIndex = script.IndexOf(conditionalKeyword);
			int thenIndex = script.IndexOf("{");
			int endIndex = script.IndexOf("}");
			///////
			string predicate = script.Remove(thenIndex);
			predicate = predicate.Remove(ifIndex, conditionalKeyword.ToCharArray().Length);
			// Remove all spaces and line breaks
			predicate = predicate.Replace(" ", "");
			predicate = predicate.Replace('\n'.ToString(), "");

			string codeBlock = "";

			int index = 0;

			for(int i = thenIndex; i < endIndex; i++) {
			
				codeBlock.Insert(index, script.ToCharArray()[i].ToString());
				index++;
			}


			Debug.Log("Code Block: " + codeBlock);


			return predicate;

		}*/
	}
}
