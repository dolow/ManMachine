using UnityEngine;

public class Redirector : MonoBehaviour
{
    public Redirectee redirectee = null;

    private Vector3 DefaultDirection()
    {
        return this.transform.TransformPoint(this.transform.forward);
    }

    public void Redirect(Redirectable redirectable)
    {
        this.Redirect(redirectable, this.DefaultDirection());
    }
    public void Redirect(Redirectable redirectable, Vector3 direction)
    {
        if (this.redirectee != null)
        {
            this.redirectee.RequestRedirect(redirectable, this, direction);
        }
        else
        {
            Redirectee redirectee = redirectable.RequestRedirectee(this);
            if (redirectee != null)
            {
                redirectee.RequestRedirect(redirectable, this, direction);
            }
        }
    }
}
