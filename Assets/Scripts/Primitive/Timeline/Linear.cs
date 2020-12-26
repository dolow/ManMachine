using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Linear : MonoBehaviour
{
    public float duration = 0.0f;
    public bool reversed = false;
    public bool repeat = false;
    public bool autoPlay = false;

    protected float passedDuration = 0.0f;

    abstract protected void UpdateValue();

    private void Awake()
    {
        if (this.autoPlay && this.reversed)
        {
            this.End();
        }
        else
        {
            this.Begin();
        }
    }

    private void Update()
    {
        this.UpdateDuration(Time.deltaTime);
        this.UpdateValue();
    }

    public bool IsFinished()
    {
        return (this.passedDuration <= 0.0f || this.passedDuration >= this.duration);
    }

    public void Begin()
    {
        this.passedDuration = 0.0f;
    }

    public void End()
    {
        this.passedDuration = this.duration;
    }

    // Update is called once per frame
    protected void UpdateDuration(float dt)
    {
        if (this.reversed)
        {
            if (this.passedDuration < 0.0f)
            {
                if (!this.repeat)
                {
                    return;
                }
                this.passedDuration = this.duration;
            }

            this.passedDuration -= dt;
        }
        else
        {
            if (this.passedDuration > this.duration)
            {
                if (!this.repeat)
                {
                    return;
                }
                this.passedDuration = 0.0f;
            }

            this.passedDuration += dt;
        }
    }
}
