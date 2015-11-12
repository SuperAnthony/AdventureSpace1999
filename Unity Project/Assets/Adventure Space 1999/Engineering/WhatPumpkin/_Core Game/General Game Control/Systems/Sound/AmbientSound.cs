#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Sergio I. Nizama
// Date Created - June 3rd, 2015
#endregion 

using UnityEngine;
using System.Collections;


namespace WhatPumpkin.Sound
{
    [System.Serializable]
    public class AmbientSound : SFX
    {
        /// <summary>
        /// Gets the key name of the ambient sound type.
        /// </summary>
        public override string Key { get { return _key; } }

        #region Constructors

        public AmbientSound(SgridAudioSource audioSource)
        {
            AudioSource = audioSource;
        }

        public AmbientSound(SgridAudioSource audioSource, AudioClip audioClip) 
            : this(audioSource)
        {
            _audioClip = audioClip;
        }

        public AmbientSound(SgridAudioSource audioSource, AudioClip audioClip, string key)
            : this(audioSource, audioClip)
        {
            _key = key;
        }

        #endregion

        /// <summary>
        /// Unsubscribe from all events.
        /// </summary>
        protected override void UnsubscrbieEvents()
        {
            SoundManager.Instance.FadeOutAmbient -= FadeOut;
            base.UnsubscrbieEvents();
        }

        /// <summary>
        /// Plays the audio clip.
        /// </summary>
        public override void Play()
        {
            if (AudioSource == null)
                AudioSource = GameObject.Find(_key).GetComponent<SgridAudioSource>();
            base.Play();
            SoundManager.Instance.FadeOutAmbient += FadeOut;
        }

        public override void StopFade()
        {
            base.StopFade();
        }

        /// <summary>
        /// Stops audio clip playback.
        /// </summary>
        public override void Stop()
        {
            base.Stop();
            SoundManager.Instance.FadeOutAmbient -= FadeOut;
        }
    }
}