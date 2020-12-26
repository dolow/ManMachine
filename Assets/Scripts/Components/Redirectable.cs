using UnityEngine;

public class Redirectable : MonoBehaviour
{
    public delegate Redirectee GetRedirectee(Redirector redirector);

    public GetRedirectee OnRequestRedirectee = null;

    public Redirectee RequestRedirectee(Redirector redirector)
    {
        return this.OnRequestRedirectee?.Invoke(redirector);
    }
}
