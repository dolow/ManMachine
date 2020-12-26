using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLight : MonoBehaviour
{
    void Awake()
    {
        UserFeedbacker feedbacker = this.GetComponent<UserFeedbacker>();
        if (feedbacker != null)
        {
            feedbacker.OnFeedback += this.Flash;
        }
    }

    void Flash(FeedbackType type)
    {
        if (type != FeedbackType.Unlock && type != FeedbackType.Rotate)
        {
            return;
        }

        LinearIntensity linear = this.GetComponent<LinearIntensity>();
        if (linear == null)
        {
            return;
        }

        linear.Begin();
    }
}
