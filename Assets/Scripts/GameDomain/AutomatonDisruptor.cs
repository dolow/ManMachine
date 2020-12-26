using UnityEngine;


/// <summary>
/// TODO: consider specify Wall
/// </summary>
public class AutomatonDisruptor : MonoBehaviour
{
    public float collaplseDistance = 0.25f;

    private Collider GetCollider()
    {
        Collider collider;

        collider = this.GetComponent<MeshCollider>();
        if (collider != null)
        {
            return collider;
        }
        collider = this.GetComponent<BoxCollider>();
        if (collider != null)
        {
            return collider;
        }
        collider = this.GetComponent<CapsuleCollider>();
        if (collider != null)
        {
            return collider;
        }
        collider = this.GetComponent<SphereCollider>();
        if (collider != null)
        {
            return collider;
        }
        collider = this.GetComponent<TerrainCollider>();
        if (collider != null)
        {
            return collider;
        }
        collider = this.GetComponent<WheelCollider>();
        if (collider != null)
        {
            return collider;
        }

        return null;
    }

    private void OnTriggerStay(Collider other)
    {
        WatchDog watchdog = other.GetComponent<WatchDog>();
        Agent agent = other.GetComponent<Agent>();
        if (watchdog == null && agent == null)
        {
            return;
        }
        
        Collider collider = this.GetCollider();

        if (watchdog != null && watchdog.state != Actor.State.Collapse)
        {
            if (collider.bounds.SqrDistance(watchdog.transform.position) < this.collaplseDistance)
            {
                watchdog.Collapse();
            }
        }

        if (agent != null && agent.state != Actor.State.Collapse)
        {
            if (collider.bounds.SqrDistance(agent.transform.position) < this.collaplseDistance)
            {
                agent.Collapse();
            }
        }
    }
}
