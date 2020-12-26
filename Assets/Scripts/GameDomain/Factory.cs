using UnityEngine;

public class Factory : MonoBehaviour
{
    public int durationFrames = 180;

    private int passedFrames = 0;
    private int obstacles = 0;

    void Awake()
    {
        this.GetComponent<Spawner>();
    }

    void Update()
    {
        Spawner spawner = this.GetComponent<Spawner>();
        if (spawner == null)
        {
            return;
        }
        Spawnable spawnable = this.GetComponent<Spawnable>();
        if (spawnable == null)
        {
            return;
        }

        if (this.durationFrames <= 0)
        {
            return;
        }

        if (this.passedFrames == this.durationFrames)
        {
            if (this.obstacles <= 0)
            {
                spawner.Spawn(spawnable);
            }

            this.passedFrames = 0;
        }

        this.passedFrames++;
    }

    private void OnTriggerEnter(Collider collider)
    {
        this.obstacles++;
    }

    private void OnTriggerExit(Collider collider)
    {
        this.obstacles--;
    }
}
