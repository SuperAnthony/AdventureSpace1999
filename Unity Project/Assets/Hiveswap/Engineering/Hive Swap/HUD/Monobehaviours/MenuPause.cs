using UnityEngine;
using System.Collections;


namespace WhatPumpkin
{
    /// <summary>
    /// Script used for toggling in-game menu screen and plays the menu music.
    /// </summary>
    public class MenuPause : MonoBehaviour
    {
        [SerializeField]
        private Switch _menuScreen;

        [SerializeField]
        private AudioSource _pauseMusicAudioSource;
        [SerializeField]
        private AudioSource _pauseAmbientAudioSource;
        [SerializeField]
        private AudioSource _pauseSFXAudioSource;

        /*[SerializeField]
        private UnityEngine.UI.Slider _musicSlider;
        [SerializeField]
        private UnityEngine.UI.Slider _ambientSlider;
        [SerializeField]
        private UnityEngine.UI.Slider _sfxSlider;*/

        /*[SerializeField]
        private UnityEngine.UI.Toggle _masterToggle;
        [SerializeField]
        private UnityEngine.UI.Toggle _musicToggle;
        [SerializeField]
        private UnityEngine.UI.Toggle _ambientToggle;
        [SerializeField]
        private UnityEngine.UI.Toggle _sfxToggle;*/
        
        // Use this for initialization
        void Start()
        {
            /*_musicSlider.onValueChanged.AddListener(delegate {AdjustSampleMusicVolume();});
            _ambientSlider.onValueChanged.AddListener(delegate {PlayAmbientSample();});
            _sfxSlider.onValueChanged.AddListener(delegate {AdjustSampleSFXVolume();});*/
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Pauses the ingame audio and enables the Menu HUD
        /// </summary>
        public void OpenMenu()
        {
            PauseAudio();
            _menuScreen.SwitchActiveState();
            _pauseMusicAudioSource.Play();
        }

        /// <summary>
        /// Resumes the ingame audio and deactivated the Menu HUD
        /// </summary>
        public void CloseMenu()
        {
            _pauseMusicAudioSource.Stop();
            ResumeAudio();
            _menuScreen.Deactivate();
        }

        private void PauseAudio()
        {
            SoundManager.Instance.PauseAudioPlayback();
        }

        private void ResumeAudio()
        {
            SoundManager.Instance.ResumeAudioPlayback();
        }

        /*private void AdjustSampleMusicVolume()
        {
            var constrainedSliderValue = (_musicSlider.value / 100f) * SoundManager.Instance.MasterVolume;
            _pauseMusicAudioSource.volume = constrainedSliderValue;
        }*/

        public void PlayAmbientSample()
        {
            if (!_pauseAmbientAudioSource.isPlaying)
            {
                _pauseAmbientAudioSource.Play();
            }
        }

        /*private void AdjustSampleSFXVolume()
        {
            var constrainedSliderValue = (_sfxSlider.value / 100f) * SoundManager.Instance.MasterVolume;
            _pauseSFXAudioSource.volume = constrainedSliderValue;
        }*/
        
        public void PlaySFXSample()
        {
            _pauseSFXAudioSource.Play();
        }
    }
}

