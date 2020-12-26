using UnityEngine;

public class Spawnee : MonoBehaviour
{
    public delegate void Exec(Spawner spawner, Spawnable spawnable);
    public Exec OnSpawned;

    private Spawner spawner = null;

    public void Spawn(Spawner spawner, Spawnable spawnable)
    {
        this.spawner = spawner;
        this.OnSpawned?.Invoke(spawner, spawnable);
    }

    private void OnDestroy()
    {
        if (this.spawner != null)
        {
            this.spawner.OnSpwneeDestroyed(this);
        }
    }
}
