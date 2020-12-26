using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearTransform : MonoBehaviour
{
    public bool transformPosition = false;
    public bool localPosition = false;
    public Vector3 positionFrom = Vector3.zero;
    public Vector3 positionTo = Vector3.zero;

    public bool transformRotation = false;
    public bool localRotation = false;
    public Vector3 rotationFrom = Vector3.zero;
    public Vector3 rotationTo = Vector3.zero;

    public bool transformScale = false;
    public Vector3 scaleFrom = Vector3.zero;
    public Vector3 scaleTo = Vector3.zero;

    public float duration = 0.0f;

    public bool reveresed = false;
    public bool repeat = false;
    
    private float passedDuration = 0.0f;

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

    void Update()
    {
        if (this.reveresed) {
            if (this.passedDuration < 0.0f)
            {
                if (!this.repeat)
                {
                    return;
                }
                this.passedDuration = this.duration;
            }

            this.passedDuration -= Time.deltaTime;
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

            this.passedDuration += Time.deltaTime;
        }
        
        if (this.transformPosition)
        {
            Vector3 newPos = Vector3.Lerp(this.positionFrom, this.positionTo, this.passedDuration / this.duration);
            if (this.localPosition)
            {
                this.transform.localPosition = newPos;
            }
            else
            {
                this.transform.position = newPos;
            }
        }

        if (this.transformRotation)
        {
            Vector3 newRotation = Vector3.Lerp(this.rotationFrom, this.rotationTo, this.passedDuration / this.duration);
            
            if (this.localRotation)
            {
                this.transform.localRotation = Quaternion.Euler(newRotation);
            }
            else
            {
                this.transform.rotation = Quaternion.Euler(newRotation);
            }
        }

        if (this.transformScale)
        {
            Vector3 newScale = Vector3.Lerp(this.scaleFrom, this.scaleTo, this.passedDuration / this.duration);
            this.transform.localScale = newScale;
        }
    }
}
