using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeat : MonoBehaviour
{
    public Vector3 fromPosition = Vector3.zero;
    public Vector3 toPosition = Vector3.zero;
    public float duration = 0.0f;
    public bool local = true;

    private float passedDuration = 0.0f;

    void Update()
    {
        this.passedDuration += Time.deltaTime;
        if (this.passedDuration > this.duration)
        {
            this.passedDuration = 0.0f;
        }

        Vector3 newPos = Vector3.Lerp(this.fromPosition, this.toPosition, this.passedDuration / this.duration); ;

        if (this.local)
        {
            this.transform.localPosition = newPos;
        } else
        {
            this.transform.position = newPos;
        }
        
    }
}
