using UnityEngine;

public abstract class AGameInteractionInterface : MonoBehaviour
{
    protected int intention = 0;

    protected Vector3 moveDirection = Vector3.zero;
    public Vector3 MoveDirection
    {
        get
        {
            return this.moveDirection;
        }
        protected set
        {
            this.moveDirection = value;
        }
    }

    protected float rotation = 0.0f;
    public float Rotation
    {
        get
        {
            return this.rotation;
        }
        protected set
        {
            this.rotation = value;
        }
    }

    public bool HasIntent(InteractionSemantic semantic)
    {
        return (this.intention & (int)semantic) == (int)semantic;
    }
    public bool HasIntent(int semantics)
    {
        return (this.intention & semantics) > 0;
    }

    public void RemoveIntent(InteractionSemantic semantic)
    {
        this.intention &= ~(int)semantic;
    }
    public void RemoveIntent(int semantics)
    {
        this.intention &= ~semantics;
    }

    public void ClearIntent()
    {
        this.intention = 0;
    }

    protected void AddIntent(InteractionSemantic semantic)
    {
        this.intention |= (1 << (int)semantic);
    }
    protected void AddIntent(int semantics)
    {
        this.intention |= semantics;
    }
    protected void ClearMovement()
    {
        this.moveDirection = Vector3.zero;
        this.rotation = 0.0f;
    }
}
