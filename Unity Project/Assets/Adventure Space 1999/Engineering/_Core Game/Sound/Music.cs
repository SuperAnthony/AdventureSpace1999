#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Sergio I. Nizama
// Date Created - June 3rd, 2015
#endregion 

using UnityEngine;


namespace WhatPumpkin.Sound
{
    /// <summary>
    /// Sound type containing audio clip corresponding to music/bgm
    /// </summary>

    [System.Serializable]
    public class Music : SFX
    {

#region Constructors
        public Music() { }

        public Music(AudioClip audioClip, string key)
        {
            _audioClip = audioClip;
            _key = key;
        }
#endregion

        /// <summary>
        /// Unsubscribes from all events.
        /// </summary>
        protected override void UnsubscrbieEvents()
        {
            SoundManager.Instance.FadeOutMusic -= FadeOut;
            base.UnsubscrbieEvents();
        }

        /// <summary>
        /// Plays the audio clip.
        /// </summary>
        public override void Play()
        {
            if (AudioSource == null)
            {
                AudioSource = SoundManager.Instance.MusicAudioSource;
                if (AudioSource == null)
                {
                    Debug.Log("Sound Manager is Missing Sgrid Audio Sources for Music sound type.");
                    return;
                }
            }
            base.Play();
            SoundManager.Instance.FadeOutMusic += FadeOut;
        }

        /// <summary>
        /// Prepares private fields for fading of sound.
        /// </summary>
        /// <param name="fadeCurve">The curve that contains the fade values.</param>
        /// <param name="fadeCurveType">The type of fade the curve represents; True: Fade-out; False: Fade-in.</param>
        public override void BeginFade(AnimationCurve fadeCurve, bool fadeCurveType)
        {
            //If it's fade out and currently not playing, it'll play the audio clip.
			if (fadeCurveType && AudioSource == null)
            {
                Play();
            }
            if (fadeCurveType == false)
            {
                AudioSource = SoundManager.Instance.MusicAudioSource;
                AudioSource.AudioSource.volume = 0.0f;
            }
            _fadeCurveType = fadeCurveType;
            _fadeCurve = fadeCurve;
			_fadeEndTime = UnityEngine.AudioSettings.dspTime + fadeCurve[fadeCurve.length - 1].time;
            _isFading = true;
            EventManager.OnUpdate += StartFade;
            StartFade();
        }


        protected override void StartFade()
        {
            base.StartFade();
        }

        public override void Stop()
        {
            base.Stop();
            SoundManager.Instance.FadeOutMusic -= FadeOut;
        }

        public override void StopFade()
        {
            base.StopFade();
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        public override string Key { get { return _key;} }
    }
}
