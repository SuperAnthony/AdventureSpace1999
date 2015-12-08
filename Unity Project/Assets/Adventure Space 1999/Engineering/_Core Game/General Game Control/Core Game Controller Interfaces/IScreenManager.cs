using UnityEngine;
using System.Collections;

namespace WhatPumpkin
{

    public interface IScreenManager
    {

        /// <summary>
        /// Open the screen of a given key
        /// </summary>
        /// <param name="key"></param>
        
        void OpenScreen(string key);

        /// <summary>
        /// Close the screen of a given key
        /// </summary>
        /// <param name="key"></param>

        void CloseScreen(string key);

        /// <summary>
        /// Method to perform if user hits the escape key
        /// </summary>
        /// <param name="method"></param>

        void ReceiveEscapeDelegate(System.Action method);
    }
}