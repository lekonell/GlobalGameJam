using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum UIEvent
    {
        Click,
        Drag,
    }

    public enum MouseEvent
    {
        Press,
        Click,
    }

    public enum CameraMode
    {
        QuarterView,
    }

    public enum Layer
    {
        Player = 3,
        Ground = 6,
        Platform = 7,
        Enemy = 8,
    }

    public enum Scene
    {
        Unknown,
        Main,
        Tree,
        Root,
        InGameA,
        InGameB,
        Ending,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }
}
