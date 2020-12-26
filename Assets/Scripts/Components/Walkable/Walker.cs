using UnityEngine;

[System.Serializable]
public struct Movement
{
    public float moveFront;
    public float moveRight;
    public float rotateVolume;

    public Movement(float front, float right, float rotate)
    {
        this.moveFront = front;
        this.moveRight = right;
        this.rotateVolume = rotate;
    }

    public bool IsMoving()
    {
        return this.moveFront > 0.0f ||  this.moveRight > 0.0f || this.rotateVolume > 0.0f;
    }

    public void Stop()
    {
        this.moveFront = 0.0f;
        this.moveRight = 0.0f;
        this.rotateVolume = 0.0f;
    }
}

public abstract class Walker : MonoBehaviour
{
    public const string AnimatorStateWalking = "Walking";
    public const string AnimatorStateRunning = "Running";
    public const string AnimatorStateCollapse = "Collapse";

    public virtual Movement GetMovement()
    {
        return new Movement();
    }

    public virtual void Stop()
    {
        Animator animator = this.GetComponent<Animator>();
        if (animator)
        {
            animator.SetBool(AnimatorStateWalking, false);
            animator.SetBool(AnimatorStateRunning, false);
        }
    }
    public virtual void Move(float front, float right, float rot)
    {
        Animator animator = this.GetComponent<Animator>();
        if (animator)
        {
            animator.SetBool(AnimatorStateWalking, true);
            animator.SetBool(AnimatorStateRunning, false);
        }
    }
    public virtual void Run(float front, float right, float rot)
    {
        Animator animator = this.GetComponent<Animator>();
        if (animator)
        {
            animator.StopPlayback();
            animator.SetBool(AnimatorStateWalking, false);
            animator.SetBool(AnimatorStateRunning, true);
        }
    }
    public virtual void MoveFront(float front)
    {
        Animator animator = this.GetComponent<Animator>();
        if (animator)
        {
            animator.SetBool(AnimatorStateWalking, true);
            animator.SetBool(AnimatorStateRunning, false);
        }
    }
    public virtual void MoveRight(float right)
    {
        Animator animator = this.GetComponent<Animator>();
        if (animator)
        {
            animator.SetBool(AnimatorStateWalking, true);
            animator.SetBool(AnimatorStateRunning, false);
        }
    }
    public virtual void MoveRotate(float rotate)
    {
        Animator animator = this.GetComponent<Animator>();
        if (animator)
        {
            animator.SetBool(AnimatorStateWalking, true);
            animator.SetBool(AnimatorStateRunning, false);
        }
    }
    public virtual void Collapse()
    {
        Animator animator = this.GetComponent<Animator>();
        if (animator)
        {
            animator.StopPlayback();
            animator.SetBool(AnimatorStateWalking, false);
            animator.SetBool(AnimatorStateRunning, false);
            animator.SetTrigger(AnimatorStateCollapse);
        }
    }
}
