using System;
using UnityEngine;
using Random = System.Random;

namespace WhatPumpkin.Sound
{

    /// <summary>
    /// Random SFX sound type. Acts just like SFXGO but with random audio clip played at playback.
    /// </summary>
    [Serializable]
    public class RandomSFXGO : SFXGO
    {
        /// <summary>
        /// Collection of audio clips associated with the random sfx.
        /// </summary>
        [SerializeField]
        private AudioClip[] _audioClips;

        /// <summary>
        /// Getter of the random audio clips collection.
        /// </summary>
        public AudioClip[] AudioClips { get { return _audioClips; } }

        //Necessary for sound manager to work properly.
        void Start()
        {
            _audioClip = _audioClips[0];
        }

        /// <summary>
        /// Unsubscribe from all events.
        /// </summary>
        protected override void UnsubscribeEvents()
        {
            SoundManager.Instance.FadeOutRandomSFX -= FadeOut;
            _sgridAudioSource.FinishedPlayback -= UnsubscribeEvents;
        }

        /// <summary>
        /// Plays the audio clip and subscribe to SoundManager event.
        /// </summary>
        public override void Play()
        {
            PlayRandomAudioClip();
            SoundManager.Instance.FadeOutRandomSFX += FadeOut;
        }

        /// <summary>
        /// Plays back a random audio clip from the collection of audio clip.
        /// </summary>
        private void PlayRandomAudioClip()
        {
            var randomAudioClipPicker = new Random();
            var audioClipsIndex = randomAudioClipPicker.Next(0, AudioClips.Length);
            _sgridAudioSource.AudioSource.clip = AudioClips[audioClipsIndex];
            _sgridAudioSource.FinishedPlayback += UnsubscribeEvents;
            _sgridAudioSource.Play();
        }

        /// <summary>
        /// Stops the fading of the audio clip volume.
        /// </summary>
        public override void StopFade()
        {
            //Debug.Log("Stopped Fading.");
            _isFading = false;
            EventManager.OnUpdate -= StartFade;
            SoundManager.Instance.FadeOutRandomSFX -= FadeOut;
        }

#if UNITY_EDITOR

        /// <summary>
        /// Used under editor mode to assign audio clips to sound type.
        /// </summary>
        /// <param name="clips">The audio clips to add.</param>
        [ExecuteInEditMode]
        public void AddAudioClips(params AudioClip[] clips)
        {
            _audioClips = new AudioClip[clips.Length];
            _audioClips = clips;
        }

        /// <summary>
        /// Used under editor mode when creating random audio clips for the scene.
        /// </summary>
        /// <param name="clips"></param>
        [ExecuteInEditMode]
        public void SetAudioClips(AudioClip[] clips)
        {
            _audioClips = clips;
        }

        /// <summary>
        /// Used under editor mode to set key of newly created random sfx.
        /// </summary>
        /// <param name="key">The key name of the random sfx.</param>
        [ExecuteInEditMode]
        public override void SetKey(string key)
        {
            base.SetKey(key);
        }

        /// <summary>
        /// Used under editor mode to assign just one audio clip to the sound type.
        /// Redudant since this sound type should always have at least 2 audio clip.
        /// </summary>
        /// <param name="audioClip">The audio clip to add.</param>
        [ExecuteInEditMode]
        public override void SetAudioClip(AudioClip audioClip)
        {
            AddAudioClips(audioClip);
        }


#endif
        
    }
}