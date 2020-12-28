using UnityEngine;

public class Rotator : MonoBehaviour
{
    public delegate void Rotated(Rotatable rotatable, float degree);

    public Rotated OnRotated = null;
    public Rotatee rotatee = null;

    public float defaultRotateDegree = -90.0f;

    public void Rotate(Rotatable rotatable)
    {
        this.Rotate(rotatable, this.defaultRotateDegree);
    }

    public void Rotate(Rotatable rotatable, float degree)
    {
        this.rotatee.RequestRotate(rotatable, this, degree);
        this.OnRotated?.Invoke(rotatable, degree);
    }
}
