using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    public delegate void Spawned(Spawnable spawnable, Spawnee spawnee);

    public Spawnee spawnee = null;
    public int maxConcurrentSpawnees = 3;
    public Vector3 offset = Vector3.zero;

    private int survivedSpawneeCount = 0;

    public void Spawn(Spawnable spawnable)
    {
        if (this.survivedSpawneeCount >= this.maxConcurrentSpawnees)
        {
            return;
        }

        Vector3 rot = this.transform.rotation.eulerAngles;
        Quaternion quot = Quaternion.Euler(0.0f, rot.y, 0.0f);
        // can not request to unexisting entity
        Spawnee instance = Instantiate<Spawnee>(this.spawnee, this.transform.position + this.offset, quot);
        instance.Spawn(this, spawnable);

        this.survivedSpawneeCount++;
    }

    public void OnSpwneeDestroyed(Spawnee spawnee)
    {
        this.survivedSpawneeCount--;
    }
}
