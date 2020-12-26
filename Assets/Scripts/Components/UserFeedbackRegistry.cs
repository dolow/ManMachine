using System.Collections.Generic;
using UnityEngine;

public enum FeedbackType
{
    Unlock,
    Rotate
}

public class UserFeedbackRegistry : MonoBehaviour
{
    public List<UserFeedbacker> unlockFeedbackers = new List<UserFeedbacker>();
    public List<UserFeedbacker> rotateFeedbackers = new List<UserFeedbacker>();

    // Start is called before the first frame update
    public void Feedback(FeedbackType type)
    {
        List<UserFeedbacker> feedbackers = null;
        switch (type)
        {
            case FeedbackType.Unlock:
                {
                    feedbackers = this.unlockFeedbackers;
                    break;
                }
            case FeedbackType.Rotate:
                {
                    feedbackers = this.rotateFeedbackers;
                    break;
                }
        }

        if (feedbackers != null)
        {
            for (int i = 0; i < feedbackers.Count; i++)
            {
                feedbackers[i].RequestFeedback(type);
            }
        }
    }
}
