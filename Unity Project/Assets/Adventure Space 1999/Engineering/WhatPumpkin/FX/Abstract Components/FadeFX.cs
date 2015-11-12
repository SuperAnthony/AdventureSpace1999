using UnityEngine;
using System.Collections;

namespace WhatPumpkin.FX
{
    public abstract class FadeFX : Effect
    {
        /// <summary>
        /// The animation curve that corresponds to the fading behavior/animation.
        /// </summary>
        [SerializeField] protected AnimationCurve _fadeCurve;
        
        /// <summary>
        /// Gets the _fadeCurve.
        /// </summary>
        public virtual AnimationCurve FadeCurve { get {return _fadeCurve;} }

        /// <summary>
        /// The duration of the effect in seconds.
        /// </summary>
        protected float _duration;
        /// <summary>
        /// Amount of time the effect has been active for.
        /// </summary>
        protected float _elapesedTime;
        /// <summary>
        /// The original color values of the 
        /// </summary>
        protected Color _originalColor;

        void Awake()
        {
            
        }

        // Use this for initialization
		protected void Start()
        {
            _duration = _fadeCurve[_fadeCurve.length - 1].time;
        }

        // Update is called once per frame
       protected void Update()
        {
            if (_isActive)
            {
                _elapesedTime += Time.smoothDeltaTime;
                var timeOnCurve = _fadeCurve.Evaluate(_elapesedTime);
                if (_duration > _elapesedTime)
                {
                    ChangeAlphaValue(timeOnCurve);
                } else Deactivate();
                
            }
        }
        

        /// <summary>
        /// Use this on a loop to implement fading.
        /// </summary>
        /// <param name="alphaValue"></param>
        protected abstract void ChangeAlphaValue(float alphaValue);

        /// <summary>
        /// Activates the effect.
        /// </summary>
        public override void Activate()
        {
            _elapesedTime = 0f;
            _isActive = true;
        }

        /// <summary>
        /// Use this to implement making the fading item completely transparent.
        /// </summary>
        protected abstract void MakeInvisible();

        /// <summary>
        /// Deactivate the effect.
        /// </summary>
        public override void Deactivate()
        {
            _isActive = false;
            _elapesedTime = 0f;
        }

       
    }
}

