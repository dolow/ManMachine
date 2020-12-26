using UnityEngine;

public class Key : MonoBehaviour
{
    void Awake()
    {
        Unlocker unlocker = this.GetComponent<Unlocker>();
        if (unlocker == null)
        {
            return;
        }

        unlocker.OnUnlocked = this.OnUnlocked;
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player == null)
        {
            return;
        }

        LinearTransform linear = this.gameObject.GetComponentInChildren<LinearTransform>();
        if (linear == null)
        {
            return;
        }

        linear.Begin();
        linear.repeat = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player == null)
        {
            return;
        }

        LinearTransform linear = this.gameObject.GetComponentInChildren<LinearTransform>();
        if (linear == null)
        {
            return;
        }

        linear.End();
        linear.repeat = false;
    }

    private void OnUnlocked(Unlockable unlockable)
    {
        UserFeedbackable feedbackable = this.GetComponent<UserFeedbackable>();
        if (feedbackable == null)
        {
            return;
        }

        feedbackable.Feedback(FeedbackType.Unlock);
    }
}
