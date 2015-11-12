#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Sergio Nizama
// Date Created - June 10th, 2015
#endregion 

using UnityEngine;

namespace WhatPumpkin.Sound
{
    /// <summary>
    /// Used for achieving the MonoBehavior Update function while in the editor mode,
    /// so that fading can be played back during editor mode to test out sound.
    /// TODO: This work around is very memroy intensive!
    /// </summary>

    [ExecuteInEditMode]
    public class SoundFadeEditorHelper : MonoBehaviour
    {
        /// <summary>
        /// The curve process for fading the volume of the sound.
        /// </summary>
        private AnimationCurve _fadeCurve;

        /// <summary>
        /// Used for triggering different logic depending whether it's a fade-out or fade-in curve.
        /// Fade-Out: Will play the sound immediately.
        /// Fade-In: Will play sound on when time reaches first keyframe's time.
        /// </summary>
        private bool _fadeCurveType;

        /// <summary>
        /// The time in seconds that fading will complete.
        /// </summary>
        private double _fadeEndTime;

        /// <summary>
        /// The audio clip that is played back during the fade.
        /// </summary>
        private AudioClip _audioClip;

        /// <summary>
        /// The audio source used to control the volume of the audio clip during fading.
        /// </summary>
        private AudioSource _audioSource;

        /// <summary>
        /// Trigger used to start fading and end fading.
        /// </summary>
        private bool _isFading = false;

        /// <summary>
        /// Trigger used avoid multiple Play() call to fade-in audio clip.
        /// </summary>
        private bool _isFadingIn = false;

        /// <summary>
        /// Get/set the audio clip to be faded.
        /// </summary>
        public AudioClip SampleAudioClip
        {
            get { return _audioClip; }
            set { _audioClip = value; }
        }

        /// <summary>
        /// Get/set the audio source that controls the volume for fading.
        /// </summary>
        public AudioSource SampleAudioSource
        {
            get { return _audioSource; }
            set { _audioSource = value; }
        }

#if UNITY_EDITOR

        [ExecuteInEditMode]
        void Start()
        {
            //_audioSource = gameObject.GetComponent<AudioSource>();
        }

        /// <summary>
        /// Does the fade processing work.
        /// </summary>
        [ExecuteInEditMode]
        void Update()
        {
            if (_isFading)
            {
                var timeAlongCurve = AudioSettings.dspTime;
                if (_fadeEndTime >= timeAlongCurve)
                {
                    _audioSource.volume = _fadeCurve.Evaluate(_fadeCurve[_fadeCurve.length - 1].time - (System.Convert.ToSingle(_fadeEndTime - timeAlongCurve)));
                }
                if (_fadeCurveType == false)
                {
                    if ((_fadeCurve[0].time - 0.001f) <= (_fadeCurve[_fadeCurve.length - 1].time) - System.Convert.ToSingle(_fadeEndTime - timeAlongCurve) && _isFadingIn == false)
                    {
                        _isFadingIn = true;
                        _audioSource.Play();
                    }
                }
                if (_audioSource.volume <= 0.0f && _fadeCurveType == true)
                {
                    StopFadeTest();
                    _audioSource.Stop();
                }
                if (_audioSource.volume >= 1.0f && _fadeCurveType == false)
                {
                    StopFadeTest();
                }
            }
        }

        /// <summary>
        /// Stops fading by triggering _isFading to false. Resets triggers to default and stops the audio source.
        /// </summary>
        [ExecuteInEditMode]
        public void StopFadeTest()
        {
            _isFading = false;
            _isFadingIn = false;
        }

        /// <summary>
        /// Starts fading by triggering _isFading to true.
        /// </summary>
        /// <param name="fadeCurve">The animation curve that determines how the sound will fade.</param>
        /// <param name="audioFade">The audio clip that will be faded.</param>
        /// <param name="fadeType">Trigger: True for fade-out and False for fade-in.</param>
        [ExecuteInEditMode]
        public void TestFade(AnimationCurve fadeCurve, AudioClip audioFade, bool fadeType)
        {
            _audioSource = gameObject.GetComponent<AudioSource>();
            _fadeCurve = fadeCurve;
            _fadeCurveType = fadeType;
            _fadeEndTime = AudioSettings.dspTime + fadeCurve[fadeCurve.length - 1].time;
            _audioSource.clip = audioFade;
            if (_fadeCurveType) _audioSource.Play();
            _isFading = true;
        }
#endif
    }


}