#region copyright (c) 2015 What Pumpkin Studio
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - May 28, 2015
#endregion 

using System;

namespace WhatPumpkin
{

    /// <summary>
    /// I game variable control.
    /// </summary>

    public interface IGameVariableControl
    {

        /// <summary>
        /// Resets all to default.
        /// </summary>

        void ResetAllToDefault();

        /// <summary>
        /// Parses the assignment script.
        /// </summary>
        /// <param name="script">Script.</param>

        void ParseAssignmentScript(string script);

        /// <summary>
        /// Assign a variable
        /// </summary>
        /// <param name="variableName"></param>
        /// <param name="value"></param>

        void AssignVariable(string variableName, string value);

        /// <summary>
        /// Occurs When a variable is changed
        /// </summary>


        event EventHandler<VariableEventArgs> SetVariable;
    }


    public class VariableEventArgs : System.EventArgs
    {

        string _variableName;

        string _variableValue;

        public string VariableName { get { return _variableName; } }

        public string VariableValue { get { return _variableValue; } }

        public VariableEventArgs(string name, string value)
        {

            _variableName = name;
            _variableValue = value;

        }


    }
}