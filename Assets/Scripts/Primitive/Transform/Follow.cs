using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target = null;

    public Vector3 targetOffset = Vector3.zero;
    public Vector3 distance = Vector3.zero;

    public float moveSpeed = 0.1f;
    public float rotateSpeed = 0.1f;

    public void SwitchOffsetX()
    {
        this.targetOffset.x = -this.targetOffset.x;
    }

    private void Update()
    {
        if (!this.target)
        {
            return;
        }

        Vector3 localMoveDestination = distance + this.targetOffset;
        Vector3 localLookDestintion  = Vector3.forward + this.targetOffset;
        
        Vector3 moveDest = this.target.TransformPoint(localMoveDestination);
        Vector3 lookDest = this.target.TransformPoint(localLookDestintion);

        Quaternion rotateDest = Quaternion.LookRotation(lookDest - this.transform.position);

        Vector3 nextPos = Vector3.Lerp(this.transform.position, moveDest, this.moveSpeed);
        Quaternion nextLook = Quaternion.Lerp(this.transform.rotation, rotateDest, this.rotateSpeed);

        this.transform.position = nextPos;
        this.transform.rotation = nextLook;
    }
}
