using UnityEngine;
using System.Collections;
using WhatPumpkin;

namespace WhatPumpkin
{
    /// <summary>
    /// Script used for setting the in-game sound volumes through the menu.
    /// Attach this to each volume slider.
    /// </summary>
    public class MenuVolumeControl : MonoBehaviour
    {
        private UnityEngine.UI.Slider _slider;

        [SerializeField] private UnityEngine.UI.Text _valueIndicator;

        /// <summary>
        /// Used to indicate which sound volume the slider controls.
        /// </summary>
        private enum SoundTypes
        {
            Ambient,
            Music,
            SFX,
            Master
        }

        /// <summary>
        /// The choice.
        /// </summary>
        [SerializeField] private SoundTypes _soundChoice;

	    // Use this for initialization
	    void Start ()
	    {
	        _slider = GetComponent<UnityEngine.UI.Slider>();
            _slider.onValueChanged.AddListener(delegate {ChangeVolume();});
	    }

        private void ChangeVolume()
        {
            var constrainedSliderValue = _slider.value/100f;
            switch (_soundChoice)
            {
                case SoundTypes.Ambient:
                    SoundManager.Instance.AmbientVolume = constrainedSliderValue;
                    FindObjectOfType<MenuPause>().PlayAmbientSample();
                    break;
                case SoundTypes.Music:
                    SoundManager.Instance.MusicVolume = constrainedSliderValue;
                    break;
                case SoundTypes.SFX:
                    SoundManager.Instance.SFXVolume = constrainedSliderValue;
                    break;
                case SoundTypes.Master:
                    SoundManager.Instance.MasterVolume = constrainedSliderValue;
                    break;
            }
            SoundManager.Instance.OnVolumeChange();
            UpdateValueIndicator();
        }

        private void UpdateValueIndicator()
        {
            _valueIndicator.text = _slider.value.ToString() + "%";
        }
        
    }
}
