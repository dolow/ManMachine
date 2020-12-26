using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    public enum State
    {
        Any,
        Idle,
        Chasing,
        Collapse
    }

    protected static string audioCollapseCacheName = "collapse";

    protected State _state = State.Idle;
    public State state
    {
        get { return this._state; }
        protected set { this._state = value; }
    }

    private float collaptedDuration = 0.0f;
    private float collapseAnimateDuration = 1.5f;

    protected virtual void Start()
    {
        AudioClip clip = Resources.Load<AudioClip>("NoRedistributionLicense/Audio/down1");
        AudioCache.GetInstance().AddCache(audioCollapseCacheName, clip);
    }

    private void Update()
    {
        switch (this.state)
        {
            case State.Collapse:
                {
                    this.collaptedDuration += Time.deltaTime;
                    if (this.collaptedDuration < this.collapseAnimateDuration)
                    {
                        break;
                    }
                    Player player = this.GetComponent<Player>();
                    if (player == null)
                    {
                        Destroy(this.gameObject);
                    }
                    else
                    {
                        player.Respawn();
                    }
                    break;
                }
        }
    }

    public void Walk(float front, float right, float rotate)
    {
        CharacterControllerWalker walkable = this.GetComponent<CharacterControllerWalker>();
        if (walkable == null)
        {
            return;
        }

        Animator animator = this.GetComponent<Animator>();
        if (animator != null)
        {
            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
            if (info.IsName(Walker.AnimatorStateCollapse))
            {
                return;
            }
        }

        // TODO: control option like; keyoard A/D for rotate

        walkable.Move(front, right, rotate);
    }
    public void Run(float front, float right, float rotate)
    {
        CharacterControllerWalker walkable = this.GetComponent<CharacterControllerWalker>();
        if (walkable == null)
        {
            return;
        }

        Animator animator = this.GetComponent<Animator>();
        if (animator != null)
        {
            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
            if (info.IsName(Walker.AnimatorStateCollapse))
            {
                return;
            }
        }

        // TODO: control option like; keyoard A/D for rotate

        walkable.Run(front, right, rotate);
    }

    public void Stop()
    {
        CharacterControllerWalker walkable = this.GetComponent<CharacterControllerWalker>();
        if (walkable == null)
        {
            return;
        }

        walkable.Stop();
    }

    public void Collapse()
    {
        this.state = State.Collapse;

        CharacterController controller = this.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
        }

        CapsuleCollider collider = this.GetComponent<CapsuleCollider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        CharacterControllerWalker walkable = this.GetComponent<CharacterControllerWalker>();

        if (this.GetComponent<Player>() == null)
        {

            if (walkable == null)
            {
                Destroy(this);
                return;
            }
        }

        if (walkable != null)
        {
            walkable.Collapse();
            // TODO: UserFeedbackable
            AudioCache.GetInstance().OneShot(audioCollapseCacheName);
        }
    }
}