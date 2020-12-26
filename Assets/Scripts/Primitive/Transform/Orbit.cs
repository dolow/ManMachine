using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform target = null;
    public float speed = 1.0f;
    public Vector3 distance = Vector3.zero;

    void Update()
    {
        if (this.target == null)
        {
            return;
        }

        this.transform.RotateAround(this.target.position, this.distance, Time.deltaTime * this.speed);
        this.transform.LookAt(this.target.position);
    }
}
