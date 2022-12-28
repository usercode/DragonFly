// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;

namespace DragonFly.Razor.Base;

public class SavingEventArgs : EventArgs
{
    public SavingEventArgs()
    {
        CanSave = true;
    }

    public bool CanSave { get; set; }
}
