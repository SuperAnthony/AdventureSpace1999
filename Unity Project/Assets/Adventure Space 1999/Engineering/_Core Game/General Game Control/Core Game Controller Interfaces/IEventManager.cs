using System;

namespace WhatPumpkin
{

    public interface IApplicationState
    {
        event Action ApplicationClose;
        event Action ApplicationStart;
        event Action OnUpdate;
    }
}