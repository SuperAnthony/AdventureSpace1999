#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2014 What Pumpkin Studios
// Author - Anthony Paul Albino
// Date Created - January, 20 2015
#endregion 

using UnityEngine;
using System.Collections;

namespace WhatPumpkin
{
    public interface IActor
    {
        /// <summary>
        /// Play an actor's animation clip relative to an origin point
        /// </summary>
        /// <param name="_animationClip"></param>
        /// <param name="origin"></param>
      
        void Play(string _animationClip, Transform origin);

        void Stop();

    }
}
