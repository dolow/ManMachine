using System.Collections.Generic;
using UnityEngine;

public class Achievee : MonoBehaviour
{
    public delegate void Exec(Achievable achievable, Achiever achiever);
    public Exec Achieve = null;

    public int requiredAchieveRequestCount = 10;
    private Dictionary<int, int> achieverRequestCounts = new Dictionary<int, int>();

    public void RequestAchieve(Achievable achievable, Achiever achiever)
    {
        int instanceId = achiever.gameObject.GetInstanceID();
        if (!this.achieverRequestCounts.ContainsKey(instanceId))
        {
            this.achieverRequestCounts.Add(instanceId, 0);
        }

        this.achieverRequestCounts[instanceId]++;

        if (this.achieverRequestCounts[instanceId] >= this.requiredAchieveRequestCount )
        {
            this.achieverRequestCounts.Remove(instanceId);

            this.Achieve?.Invoke(achievable, achiever);
        }
    }

    public void CancelAchieve(Achiever achiever)
    {
        int instanceId = achiever.gameObject.GetInstanceID();
        if (this.achieverRequestCounts.ContainsKey(instanceId))
        {
            this.achieverRequestCounts.Remove(instanceId);
        }
    }
}
