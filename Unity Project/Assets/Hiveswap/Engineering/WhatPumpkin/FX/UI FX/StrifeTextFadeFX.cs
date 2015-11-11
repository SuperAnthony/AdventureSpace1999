using System;
using UnityEngine;

namespace WhatPumpkin.FX
{
    /// <summary>
    /// Fades the strife text from transparent to opaque based on the strife text moving effect.
    /// </summary>
    [RequireComponent(typeof(MoveFX))]
    public class StrifeTextFadeFX : FadeFX
    {
        /// <summary>
        /// The strife text to fade.
        /// </summary>
        private UnityEngine.UI.Text _strifeText;
        
        void Awake()
        {
            _strifeText = this.GetComponent<UnityEngine.UI.Text>();
            _originalColor = _strifeText.color;
            this.GetComponent<MoveFX>().OnPlayingEffect += HandleMovingObject;
            this.GetComponent<MoveFX>().FinishedPlaying += HandleFinishMovingObject;
            
            MakeInvisible();
            
        }

        void Start()
        {
            
            /*this.GetComponent<MoveFX>().OnPlayingEffect += HandleMovingObject;
            this.GetComponent<MoveFX>().FinishedPlaying += HandleFinishMovingObject;*/
        }

        void Update() { }

        void OnDestroy()
        {
            this.GetComponent<MoveFX>().OnPlayingEffect -= HandleMovingObject;
            this.GetComponent<MoveFX>().FinishedPlaying -= HandleFinishMovingObject;
        }

        /*/// <summary>
        /// Not implemented.
        /// </summary>
        public override void Activate()
        {
            throw new NotImplementedException("Not implemented. Effect is \"activated\" via the MoveFX events.");
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        public override void Deactivate()
        {
            throw new NotImplementedException("Not implemented. Effect is \"deactivated\" via the MoveFX events.");
        }*/

        /// <summary>
        /// Changes the alpha value of the strife text.
        /// Use this on a loop to implement fading.
        /// </summary>
        /// <param name="alphaValue">The alpha value from 0 to 1.</param>
        protected override void ChangeAlphaValue(float alphaValue)
        {
            _strifeText.color = new Color(_originalColor.r, _originalColor.g, _originalColor.b, alphaValue);
        }

        /// <summary>
        /// Makes the strife text transparent.
        /// </summary>
        protected override void MakeInvisible()
        {
            _strifeText.color = new Color(_originalColor.r, _originalColor.g, _originalColor.b, 0f);
        }

        /// <summary>
        /// Handles active MoveFX.
        /// </summary>
        /// <param name="sender">The object with MoveFX</param>
        /// <param name="e">Null</param>
        private void HandleMovingObject(object sender, EventArgs e)
        {
            var movingObject = sender as MoveFX;
            if (movingObject != null)
            {
                var ratio = movingObject.RatioToDestination;
                if (ratio < 0) ratio = ratio * -1f;
                ChangeAlphaValue(ratio);
            }
            else Debug.Log("Not subscribed to moving object.");
        }

        /// <summary>
        /// Handles deactivating MoveFX effect.
        /// </summary>
        /// <param name="sender">The object with MoveFX</param>
        /// <param name="e">Null</param>
        private void HandleFinishMovingObject(object sender, EventArgs e)
        {
            var movingObject = sender as MoveFX;
            if (movingObject != null)
            {
                if (movingObject.IsSetToRetreat)
                {
                    MakeInvisible();
                }
            }
        }
    }
}