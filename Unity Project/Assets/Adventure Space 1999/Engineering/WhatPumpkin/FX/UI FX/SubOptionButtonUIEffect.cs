using UnityEngine;
using System.Collections;

namespace WhatPumpkin.FX
{
    /// <summary>
    /// Abstract class used for buttons such as the character select to perform the sub-option button effect associated with the main "drawer" button.
    /// Inherit from this to implement different sequential UI effect for the sub-options.
    /// </summary>
    public abstract class SubOptionButtonUIEffect : MonoBehaviour
    {
        /// <summary>
        /// The closed state image of the main button. This is active when the sub-options are not visible.
        /// </summary>
        [SerializeField] protected UnityEngine.UI.Graphic _closedState;

        /// <summary>
        /// The opened state image of the main button. This is active when the sub-options are visible.
        /// </summary>
        [SerializeField] protected UnityEngine.UI.Graphic _openedState;

        /// <summary>
        /// The sub-option buttons that is affected by the ui effect. They must each have the appropriate effect script attach to it.
        /// </summary>
        [SerializeField] protected UnityEngine.UI.Button[] _subOptionButtons;

        /// <summary>
        /// The main option button that triggers the sub-options display.
        /// </summary>
        protected UnityEngine.UI.Button _mainButton;

        void Awake()
        {
            
        }

        // Use this for initialization
        void Start()
        {
            _openedState.enabled = false;
            _mainButton = GetComponent<UnityEngine.UI.Button>();
            _mainButton.targetGraphic = _closedState;
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Override this to implement different effects for the sub-options.
        /// </summary>
        public abstract void DisplaySubOptions();
    }
}

