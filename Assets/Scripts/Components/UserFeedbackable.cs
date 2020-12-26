using UnityEngine;

public class UserFeedbackable : MonoBehaviour
{
    public UserFeedbackRegistry registry = null;

    // Start is called before the first frame update
    public void Feedback(FeedbackType type)
    {
        if (this.registry == null)
        {
            return;
        }

        this.registry.Feedback(type);
    }
}
