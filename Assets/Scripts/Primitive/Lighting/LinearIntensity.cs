using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearIntensity : Linear
{
    public float from = 1.0f;
    public float to = 1.0f;

    protected override void UpdateValue()
    {
        Light light = this.GetComponent<Light>();
        if (light == null)
        {
            return;
        }

        float newIntensity = Mathf.Lerp(this.from, this.to, this.passedDuration / this.duration);
        light.intensity = newIntensity;
    }
}
