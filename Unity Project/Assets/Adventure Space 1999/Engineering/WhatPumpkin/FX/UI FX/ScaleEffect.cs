#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Sergio Nizama
// Date Created - August 24, 2015
#endregion 

using UnityEngine;

namespace WhatPumpkin.FX
{
    public class ScaleEffect : Effect
    {
        /// <summary>
        /// Scale up animation curve.
        /// </summary>
        [SerializeField] private AnimationCurve _scaleUpCurve;

        /// <summary>
        /// Scale down animation curve.
        /// </summary>
        [SerializeField] private AnimationCurve _scaleDownCurve;

        /// <summary>
        /// Flag that indicated whether the UI element is initially visible (scale at 0,0,0) or not.
        /// </summary>
        [SerializeField] private bool _initiallyVisible;

        /// <summary>
        /// The default sacle value of the UI element it should be at when scaling complete. This is typically at 1,1,1.
        /// </summary>
        private Vector3 _normalScale;

        /// <summary>
        /// Time of the last keyframe of the animation curve. Used for keep track of time along curve with Time.time.
        /// </summary>
        private float _effectEndTime;

        /// <summary>
        /// Flag that indicates to scale up or down.
        /// </summary>
        private bool _grow;

        /// <summary>
        /// Time at which scaling up will end. Used for deactivating.
        /// </summary>
        private float _scaleUpEndTime;

        /// <summary>
        /// Time at which scaling down will end. Used for deactivating.
        /// </summary>
        private float _scaleDownEndTime;

        [SerializeField] private bool _scaleXAxis;
        [SerializeField] private bool _scaleYAxis;

        #region Properties
        public AnimationCurve ScaleUpCurve
        {
            get { return _scaleUpCurve; }
        }

        public Vector3 NormalScale
        {
            get { return _normalScale; }
        }

        public AnimationCurve ScaleDownCurve
        {
            get { return _scaleDownCurve; }
        }
        #endregion

        void Awake()
        {
            //gets the original scale value of the UI element.
            _normalScale = gameObject.transform.localScale;
            if (!_initiallyVisible)
            {
                if (_scaleXAxis && !_scaleYAxis) gameObject.transform.localScale = new Vector3(0f, 1f, 1f);
                else if (_scaleYAxis && !_scaleXAxis) gameObject.transform.localScale = new Vector3(1f, 0f, 1f);
                else gameObject.transform.localScale = Vector3.zero;
            }
        }

        // Use this for initialization
        private void Start()
        {
            _scaleUpEndTime = _scaleUpCurve[_scaleUpCurve.length - 1].time;
            _scaleDownEndTime = _scaleDownCurve[_scaleDownCurve.length - 1].time;
        }

        // Update is called once per frame
        private void Update()
        {
            if (_isActive)
            {
                float timeAlongCurve = 0f;
                float valueAlongCurve = 0f;
                if (_grow)
                {
                    //The time along the curve calculated with Time.time to make it work properly with update function.
                    timeAlongCurve = (_scaleUpCurve[_scaleUpCurve.length - 1].time) -
                                           (_effectEndTime - Time.time);
                    valueAlongCurve = _scaleUpCurve.Evaluate(timeAlongCurve);
                }
                else
                {
                    //The time along the curve calculated with Time.time to make it work properly with update function.
                    timeAlongCurve = (_scaleDownCurve[_scaleDownCurve.length - 1].time) -
                                           (_effectEndTime - Time.time);
                    valueAlongCurve = _scaleDownCurve.Evaluate(timeAlongCurve);
                }
                //The value along the curve based on the time along the curve.
                if (_scaleYAxis && !_scaleXAxis) ChangeYScale(valueAlongCurve, timeAlongCurve);
                else if (_scaleXAxis && !_scaleYAxis) ChangeXScale(valueAlongCurve, timeAlongCurve);
                else ChangeScale(valueAlongCurve, timeAlongCurve);
                //ChangeScale(valueAlongCurve, timeAlongCurve);
            }
        }

        public override void Activate()
        {
            _isActive = true;
        }

        public override void Deactivate()
        {
            _effectEndTime = 0f;
            _isActive = false;
            FinishPlaying();
        }

        /// <summary>
        /// Scales up the gameobject's localScale using the _scaleUpCurve.
        /// </summary>
        public void ScaleUp()
        {
            //The time at which it should stop calculating/changing scale. Evaluated with Time.time into consideration.
            _isActive = false;
            _effectEndTime = Time.time + _scaleUpCurve[_scaleUpCurve.length - 1].time;
            _grow = true;
            Activate();
        }

        /// <summary>
        /// Scales down the gameobject's localScale using the _scaleDownCurve.
        /// </summary>
        public void ScaleDown()
        {
            if (_scaleDownCurve == null) InstantScaleToZero();
            //The time at which it should stop calculating/changing scale. Evaluated with Time.time into consideration.
            else
            {
                _isActive = false;
                _effectEndTime = Time.time + _scaleDownCurve[_scaleDownCurve.length - 1].time;
                _grow = false;
                Activate();
            }
        }

        /// <summary>
        /// Toggles between scaling up and scaling down based on the current scale state.
        /// </summary>
        public void OnOffScale()
        {
            
            if (transform.localScale.x >= (_scaleUpCurve[_scaleUpCurve.length - 1].value*_normalScale.x))
            {
                ScaleDown();
            } else if (transform.localScale.x <= (_scaleUpCurve[0].value*_normalScale.x))
            {
                ScaleUp();
            }
        }

        /// <summary>
        /// Sets the gameobjects scale to zero instantly rather than gradually using the animation curves.
        /// </summary>
        public void InstantScaleToZero()
        {
            gameObject.transform.localScale = Vector3.zero;
            Deactivate();
            
        }

        public override void Play()
        {
            Activate();
        }

        public override void Play(string[] parameters)
        {
            base.Play(parameters);
        }

        /// <summary>
        /// Changes the scale of the UI element based on the value on the animation curve.
        /// </summary>
        /// <param name="valueOnCurve">The value at timeOnCurve used for calculating scale.</param>
        /// <param name="timeOnCurve">The time value along the curve. Used for checking when to deactivate scaling effect.</param>
        private void ChangeScale(float valueOnCurve, float timeOnCurve)
        {
            Vector3 newScale = new Vector3(_normalScale.x* valueOnCurve, _normalScale.y*valueOnCurve, _normalScale.z* valueOnCurve);
            transform.localScale = newScale;
            if (_grow)
            {
                if (timeOnCurve >= _scaleUpEndTime)
                {
                    //gameObject.transform.localScale = _normalScale;
                    Deactivate();
                }
            }
            else
            {
                if (timeOnCurve >= _scaleDownEndTime)
                {
                    //gameObject.transform.localScale = Vector3.zero;
                    Deactivate();
                }
            }
        }

        private void ChangeYScale(float valueOnCurve, float timeOnCurve)
        {
            Vector3 newScale = new Vector3(_normalScale.x * 1f, _normalScale.y * valueOnCurve, _normalScale.z * 1f);
            transform.localScale = newScale;
            if (_grow)
            {
                if (timeOnCurve >= _scaleUpEndTime)
                {
                    //gameObject.transform.localScale = _normalScale;
                    Deactivate();
                }
            }
            else
            {
                if (timeOnCurve >= _scaleDownEndTime)
                {
                    //gameObject.transform.localScale = Vector3.zero;
                    Deactivate();
                }
            }
        }

        private void ChangeXScale(float valueOnCurve, float timeOnCurve)
        {
            Vector3 newScale = new Vector3(_normalScale.x * valueOnCurve, _normalScale.y * 1f, _normalScale.z * 1f);
            transform.localScale = newScale;
            if (_grow)
            {
                if (timeOnCurve >= _scaleUpEndTime)
                {
                    //gameObject.transform.localScale = _normalScale;
                    Deactivate();
                }
            }
            else
            {
                if (timeOnCurve >= _scaleDownEndTime)
                {
                    //gameObject.transform.localScale = Vector3.zero;
                    Deactivate();
                }
            }
        }
    }
}