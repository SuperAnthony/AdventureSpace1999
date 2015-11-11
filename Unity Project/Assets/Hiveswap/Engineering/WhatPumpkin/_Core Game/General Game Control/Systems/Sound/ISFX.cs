#region copyright (c) 2015 What Pumpkin Studios
// Copyright (c) 2015 What Pumpkin Studios
// Author - Sergio I. Nizama
// Date Created - June 3rd, 2015
#endregion 

using UnityEngine;

namespace WhatPumpkin.Sound
{

    /// <summary>
    /// Interface for all custom sound types.
    /// </summary>
    public interface ISFX : IKeyed, IPerform
    {
        AudioClip AudioClip { get; }

        void SetKey(string key);

        void SetAudioClip(AudioClip audioClip);
    }
}