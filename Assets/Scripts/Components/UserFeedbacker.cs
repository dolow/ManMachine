using UnityEngine;

public class UserFeedbacker : MonoBehaviour
{
    public delegate void Feedback(FeedbackType type);

    public Feedback OnFeedback = null;

    // Start is called before the first frame update
    public void RequestFeedback(FeedbackType type)
    {
        this.OnFeedback?.Invoke(type);
    }
}
