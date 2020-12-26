using UnityEngine;

public class OverlookCamera : MonoBehaviour
{
    public void ShowAchievedAgent(Agent agent)
    {
        Orbit orbit = this.GetComponent<Orbit>();
        if (orbit == null)
        {
            return;
        }

        orbit.enabled = true;

        orbit.target = agent.transform;
    }
}
