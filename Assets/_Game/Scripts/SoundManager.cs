using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private bool isSoundOn;
    private bool isVibrationOn;

    public bool IsSoundOn
    {
        get => isSoundOn;
        set => isSoundOn = value;
    }
    
    public bool IsVibrationOn
    {
        get => isVibrationOn;
        set => isVibrationOn = value;
    }
}
