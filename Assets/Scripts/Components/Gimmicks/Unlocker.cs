using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlocker : MonoBehaviour
{
    public delegate void Unlocked(Unlockable unlockable);

    public Unlocked OnUnlocked = null;
    public Unlockee unlockee = null;

    public void Unlock(Unlockable unlockable)
    {
        this.unlockee.RequestUnlock(unlockable, this);
        this.OnUnlocked?.Invoke(unlockable);
    }
}
