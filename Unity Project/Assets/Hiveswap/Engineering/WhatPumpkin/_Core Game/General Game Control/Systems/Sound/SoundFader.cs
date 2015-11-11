#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Sergio I. Nizama
// Date Created - June 3rd, 2015
#endregion

using System;
using UnityEngine;
using System.Collections;


namespace WhatPumpkin.Sound
{
    //TODO: Currently not being in used.

    /// <summary>
    /// Controls fading behavior of sound.
    /// </summary>
    public class SoundFader : MonoBehaviour
    {
        #region Fields

        private bool _toggleFadeOutTimeComparison = false;

        private bool _toggleFadeInTimeComparison = false;
        
        private bool _isFading = false;

        private double _fadeOutEndTime;

        private double _fadeInEndTime;

        public Fade FadeSettings;

//        [SerializeField] AnimationCurve _fadeOutCurve;
//        [SerializeField] AnimationCurve _fadeInCurve;

        private AudioSource _audioSource;

        [SerializeField] private AudioClip track2;

        #endregion

        #region Properties

        public bool IsFading
        {
            get { return _isFading; }
        }

        #endregion

        #region Methods

        private void Start()
        {
            
        }

        public void SubscribeToEvent()
        {
            EventManager.OnUpdate += StartFade;
        }

        public void UnsubscribeFromEvent()
        {
            EventManager.OnUpdate -= StartFade;
        }

        public void BeginFade(AudioClip audio1, AudioClip audio2, Fade fadeSettings, AudioSource audioSource)//probably don't need audio1 if it's already playing.
        {
            track2 = audio2;
//            _fadeOutCurve = fadeSettings.FadeOutCurve;
//           _fadeInCurve = fadeSettings.FadeInCurve;
            _audioSource = audioSource;
            SubscribeToEvent();
        }

        public void StartFade()
        {
            Debug.Log(Time.time);
            if (_toggleFadeOutTimeComparison == false)
            {
                _toggleFadeOutTimeComparison = true;
                _fadeOutEndTime = AudioSettings.dspTime + System.Convert.ToDouble(FadeSettings.FadeOutCurve[FadeSettings.FadeOutCurve.length - 1].time);
            }
            var tempFadeOutTime = AudioSettings.dspTime;
            if (tempFadeOutTime <= _fadeOutEndTime)
            {
                FadeOut(FadeSettings.FadeOutCurve[FadeSettings.FadeOutCurve.length - 1].time - (System.Convert.ToSingle(_fadeOutEndTime - tempFadeOutTime)));
            }
            else
            {
                if (_toggleFadeInTimeComparison == false)
                {
                    _toggleFadeInTimeComparison = true;
                    _fadeInEndTime = AudioSettings.dspTime + System.Convert.ToDouble(FadeSettings.FadeInCurve[FadeSettings.FadeInCurve.length - 1].time);
                    _audioSource.Stop();
                    _audioSource.clip = track2;
                    _audioSource.Play();
                }
                var tempFadeInTime = AudioSettings.dspTime;
                if (tempFadeInTime <= _fadeInEndTime)
                {
                    FadeIn(FadeSettings.FadeInCurve[FadeSettings.FadeInCurve.length - 1].time - (System.Convert.ToSingle(_fadeInEndTime - tempFadeInTime)));
                }
                if (tempFadeInTime >= _fadeInEndTime)
                {
                    UnsubscribeFromEvent();
                }
            }
        }

        public void FadeIn(float time)
        {
            _audioSource.volume = FadeSettings.FadeInCurve.Evaluate(time);
        }

        public void FadeOut(float time)
        {
            _audioSource.volume = FadeSettings.FadeOutCurve.Evaluate(time);
        }

        public void StopFade()
        {
            UnsubscribeFromEvent();
        }
/*

        public void FadeTest()//just for testing purposes.
        {
            _audioSource = FindObjectOfType<SgridAudioSource>();
            if (_audioSource == null)
            {
                Debug.Log("Could not grab Audio Source.");
            }
            else
            {
                Debug.Log(_audioSource + " Found!");
                BeginFade(track1, track2, FadeSettings, _audioSource);//Just for testing...
            }
        }

        public void PlayTest()
        {
            //SgridAudioSource = FindObjectOfType<SgridAudioSource>();
            _audioSource.clip = track1;
            _audioSource.Play();
        }
*/

        #endregion

        /*public void CommenceFade()
        {
            _isFading = true;
        }*/

        /*private void Update()
        {
            if (_isFading == true)
            {
                if (_toggleFadeOutTimeComparison == false)
                {
                    _toggleFadeOutTimeComparison = true;
                    _fadeOutEndTime = AudioSettings.dspTime + System.Convert.ToDouble(fadeOutCurve[fadeOutCurve.length - 1].time);
                    //Debug.Log(fadeOutEndTime);
                }
                var tempFadeOutTime = AudioSettings.dspTime;
                if (tempFadeOutTime <= _fadeOutEndTime)
                {
                    FadeOut(fadeOutCurve[fadeOutCurve.length - 1].time - (System.Convert.ToSingle(_fadeOutEndTime - tempFadeOutTime)));
                }
                else
                {
                    if (_toggleFadeInTimeComparison == false)
                    {
                        _toggleFadeInTimeComparison = true;
                        _fadeInEndTime = AudioSettings.dspTime + System.Convert.ToDouble(fadeInCurve[fadeInCurve.length - 1].time);
                        Audio1.Stop();
                        Audio2.PlayDelayed(.015f);
                    }
                    var tempFadeInTime = AudioSettings.dspTime;
                    if (tempFadeInTime <= _fadeInEndTime)
                    {
                        FadeIn(fadeInCurve[fadeInCurve.length - 1].time - (System.Convert.ToSingle(_fadeInEndTime - tempFadeInTime)));
                    } 
                }
            }
        }*/
    }
}
