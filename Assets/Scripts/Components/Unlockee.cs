using UnityEngine;

public class Unlockee : MonoBehaviour
{
    public delegate void Exec(Unlockable unlockable, Unlocker unlocker);
    public Exec OnUnlock = null;

    public void Unlock(Unlockable unlockable, Unlocker unlocker)
    {
        this.OnUnlock?.Invoke(unlockable, unlocker);
    }
}
