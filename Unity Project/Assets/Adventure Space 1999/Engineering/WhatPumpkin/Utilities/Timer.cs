// Copyright (c) 2015 What Pumpkin Studios
// Author - Sergio Nizama
// Date Created - May 1st, 2015

using System;
using UnityEngine;
using WhatPumpkin;

public class Timer
{
    public event Action TimerComplete;

    float remainingTime;
    //float timerSpeed;
    float timerAnchor;

    public float ElapsedTime
    {
        get { return Time.time - timerAnchor; }
    }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public Timer() { }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="timerDuration">Deafult countdown duration of the timer.</param>
    public Timer(float timerDuration)
    {
        remainingTime = timerDuration;
    }

    /// <summary>
    /// Starts the timer countdown with the default countdown value assigned.
    /// </summary>
    public void StartTimer()
    {
        timerAnchor = Time.time + remainingTime;
        EventManager.OnUpdate += CountDown;
    }


    /// <summary>
    /// Starts timer with countdown value provided via argument and not the default countdown value.
    /// </summary>
    /// <param name="timerDuration"></param>
    public void StartTimer(float timerDuration)
    {
        remainingTime = timerDuration;
        timerAnchor = Time.time + remainingTime;
        EventManager.OnUpdate += CountDown;
    }

    public void StopTimer()
    {
        EventManager.OnUpdate -= CountDown;
        timerAnchor = 0f;
    }

    public void CountDown()
    {

        if (timerAnchor <= Time.time)
        {
            OnTimerComplete();
        }
    }

    void OnTimerComplete()
    {
        if (TimerComplete != null) TimerComplete.Invoke();
        StopTimer();
    }

    public float CurrentCountdownTime 
    { 
        get 
        {
            if (timerAnchor > 0)
            {
                return timerAnchor - Time.time; 
            }
            else
            {
                return 0;
            }
        } 
    }
}



