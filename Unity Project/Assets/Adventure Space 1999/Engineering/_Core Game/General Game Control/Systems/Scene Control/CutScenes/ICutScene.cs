#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Anthony Paul Albino
#endregion 

using UnityEngine;
using System.Collections.Generic;

namespace WhatPumpkin { 

    public interface ICutScene {

        /// <summary>
        /// All cutscenes should have unique key names
        /// </summary>

        string Key { get; }

        /// <summary>
        /// Gets the origin for the cutscene.
        /// </summary>
        /// <value>The origin.</value>

        Transform Origin { get; }

        /// <summary>
        /// Gets a value indicating whether this instance has started playing.
        /// This is dependent on the IsPlaying property having been referenced
        /// </summary>
        /// <value><c>true</c> if this instance has started playing; otherwise, <c>false</c>.</value>

        bool HasStartedPlaying { get; } 

        /// <summary>
        /// Gets a value indicating whether this instance has next cut scene.
        /// </summary>
        /// <value><c>true</c> if this instance has next cut scene; otherwise, <c>false</c>.</value>


        bool HasNextCutScene { get; }

        /// <summary>
        /// Gets the next cut scene.
        /// </summary>
        /// <value>The next cut scene.</value>

        string NextCutScene { get; }

        /// <summary>
        /// Gets the CS animation.
        /// </summary>
        /// <value>The CS animation.</value>

        string CSAnimation { get; }


        /// <summary>
        /// Gets the actors.
        /// </summary>
        /// <value>The actors.</value>
        /// 
        List<IActor> Actors { get; }

        /// <summary>
        /// Play the cutscene
        /// </summary>

        void Play();

    }
}
