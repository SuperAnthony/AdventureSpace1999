#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - July 14, 2015
#endregion 

#region using
using System;
using UnityEngine;
#endregion

namespace WhatPumpkin.FX {

	public abstract class Effect : MonoBehaviour {

		static public Effect GetPrimaryEffect(GameObject gameObject) {
		
			foreach (Effect fx in gameObject.GetComponents<Effect>()) {
			
				if(fx.IsPrimaryEffect) {
					return fx;
				}
			
			}

			return null;
		
		}

		/// <summary>
		/// Occurs when finished playing.
		/// </summary>
		
		public event EventHandler FinishedPlaying;

        /// <summary>
        /// Occurs while the effect is active.
        /// </summary>
	    public event EventHandler OnPlayingEffect;

		/// <summary>
		/// The _primary effect.
		/// </summary>

		[SerializeField] protected bool _primaryEffect = false;

		/// <summary>
		/// Does this effect activate other effects when played or started?
		/// </summary>

		[SerializeField] protected bool _activatesAttachedEffects;

		/// <summary>
		/// Does this effect deactivate other effects when deactivated?
		/// </summary>

		[SerializeField] protected bool _deactivatesAttachedEffects;

		/// <summary>
		/// Is this effect active
		/// </summary>

		protected bool _isActive = false;

		/// <summary>
		/// Gets a value indicating whether this <see cref="WhatPumpkin.FX.Effect"/> primary effect.
		/// </summary>
		/// <value><c>true</c> if primary effect; otherwise, <c>false</c>.</value>

		public bool IsPrimaryEffect { get { return _primaryEffect; } }

		/// <summary>
		/// Gets a value indicating whether this instance is active.
		/// </summary>
		/// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>

		public bool IsActive { get { return _isActive; } }

		/// <summary>
		/// Activate this instance.
		/// </summary>

		public abstract void Activate();

		/// <summary>
		/// Deactivate this instance.
		/// </summary>

		public abstract void Deactivate();

		/// <summary>
		/// Play this instance.
		/// </summary>

		public virtual void Play() {
		
			Activate ();

		}

		/// <summary>
		/// Play with the specified parameters.
		/// By default it will run the play method with no parameters
		/// </summary>
		/// <param name="parameters">Parameters.</param>

		public virtual void Play (string[] parameters) {
		
			Play ();

		}

		/// <summary>
		/// Activates this effects and then attempts to activate all other effects if ActivatesAttachedEffects is true
		/// </summary>
		/// <returns><c>true</c>, if activate all was tryed, <c>false</c> otherwise.</returns>

		public virtual bool  TryActivateAll() {
			
			Activate ();
			
			// Then activate all other effects if this is a controller effect
			if(_activatesAttachedEffects){ 
				ActivateAttachedEffects (this);
				return true;
			}
			else {
				return false;
			}
			
		}

	    protected void EffectPlaying()
	    {
	        if (OnPlayingEffect != null) OnPlayingEffect(this, null);
	    }

		protected void FinishPlaying() {
		
			// Raise the finished playing event
			if (FinishedPlaying != null) {FinishedPlaying(this, null);}
		
		}

		/// <summary>
		/// Activates the attached effects.
		/// </summary>

		public void ActivateAttachedEffects(Effect controlledBy) {
		
			// Do not allow this effect to activate if the controller is not actually a component that is attached
			bool foundEffect = false;
			foreach(Effect effect in this.GetComponents<Effect>()) {
			
				if(effect == controlledBy) {
				
					foundEffect = true;
					break;
				}
			}
			if (!foundEffect) {
				return;	
			}

			// Activate attached components
			foreach(Effect effect in this.GetComponents<Effect>()) {
				
				if(effect != controlledBy) {
					effect.Activate();
				}
			}

		
		}

	}
}