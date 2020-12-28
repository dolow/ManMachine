using UnityEngine;

public class Rotatee : MonoBehaviour
{
    public delegate void Exec(Rotatable rotatable, Rotator rotator, float degree);
    public Exec Rotate = null;

    public void RequestRotate(Rotatable rotatable, Rotator rotator, float degree)
    {
        this.Rotate?.Invoke(rotatable, rotator, degree);
    }
}
