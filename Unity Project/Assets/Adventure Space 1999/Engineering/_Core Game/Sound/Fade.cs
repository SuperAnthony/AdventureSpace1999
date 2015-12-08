#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Sergio Nizama
// Date Created -June 5th 2015
#endregion
using UnityEngine;

namespace WhatPumpkin.Sound
{
    /// <summary>
    /// Contains 2 animation curves corresponding to a fade-out and fade-in behavior.
    /// Mainly used for objects that implement IFadeable.
    /// </summary>

    [System.Serializable]
    public class Fade : Keyed
    {
        /// <summary>
        /// The curve representing a fade-out.
        /// </summary>
        [SerializeField]private AnimationCurve _fadeOutCurve;
        
        /// <summary>
        /// The curve representing a fade-in.
        /// </summary>
        [SerializeField] private AnimationCurve _fadeInCurve;

#region Constructors
        public Fade()
        {
            _fadeOutCurve = new AnimationCurve();
            _fadeInCurve = new AnimationCurve();
            _key = "";
        }

        public Fade(string key)
        {
            _fadeOutCurve = new AnimationCurve();
            _fadeInCurve = new AnimationCurve();
            _key = key;
        }

        public Fade(AnimationCurve fadeOutCurve, AnimationCurve fadeInCurve)
        {
            _fadeOutCurve = fadeOutCurve;
            _fadeInCurve = fadeInCurve;
        }

        public Fade(AnimationCurve fadeOutCurve, AnimationCurve fadeInCurve, string key)
        {
            _fadeOutCurve = fadeOutCurve;
            _fadeInCurve = fadeInCurve;
            _key = key;
        }
#endregion

#region Properties
        public AnimationCurve FadeOutCurve
        {
            get { return _fadeOutCurve; }
            set { _fadeOutCurve = value; }
        }

        public AnimationCurve FadeInCurve
        {
            get { return _fadeInCurve; }
            set { _fadeInCurve = value; }
        }

        public override string Key { get { return _key; } }
#endregion
        
        /// <summary>
        /// Sets the key of this fade type.
        /// Used mainly in fade editor/soundmanager.
        /// </summary>
        /// <param name="key">Key name corresponding to this fade type.</param>
        public void SetKey(string key)
        {
            _key = key;
        }
        
    }
}