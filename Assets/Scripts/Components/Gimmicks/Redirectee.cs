using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redirectee : MonoBehaviour
{
    public delegate void Exec(Redirectable redirectable, Redirector redirector, Vector3 newDirection);

    public Exec Redirect = null;

    public int requiredRedirectRequestCount = 10;
    public int redirectWait = 60;

    private Dictionary<int, int> redirectorRequestCounts = new Dictionary<int, int>();
    private int redirectRefill = 0;

    void Update()
    {
        if (this.redirectRefill > 0)
        {
            this.redirectRefill--;
        }
    }

    public void RequestRedirect(Redirectable redirectable, Redirector redirector, Vector3 direction)
    {
        int instanceId = redirector.gameObject.GetInstanceID();
        if (!this.redirectorRequestCounts.ContainsKey(instanceId))
        {
            this.redirectorRequestCounts.Add(instanceId, 0);
        }

        this.redirectorRequestCounts[instanceId]++;

        if (this.redirectorRequestCounts[instanceId] >= this.requiredRedirectRequestCount && this.redirectRefill <= 0)
        {
            this.redirectorRequestCounts.Remove(instanceId);

            this.Redirect?.Invoke(redirectable, redirector, direction);

            this.redirectRefill = this.redirectWait;
        }
    }

    public void CancelRedirect(Redirector redirector)
    {
        int instanceId = redirector.gameObject.GetInstanceID();
        if (this.redirectorRequestCounts.ContainsKey(instanceId))
        {
            this.redirectorRequestCounts.Remove(instanceId);
        }
    }
}
