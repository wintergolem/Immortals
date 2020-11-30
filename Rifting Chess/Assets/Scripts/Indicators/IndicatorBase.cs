using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IndicatorBase : MonoBehaviour
{

    public abstract void StartAnim();

    public int unlockDigit = -1; //change in editor, -1 will throw error
    public int unlockKey = -1; //set in Start() function
    protected bool unlocked = false;
    public bool IsUnlocked()
    {
        return unlocked;
    }
    public void SetLock( bool setTo )
    {
        unlocked = setTo;
    }
    void Start()
    {
        unlockKey = 1 << unlockDigit;
    }

    void Update()
    {
        
    }
}
