using UnityEngine;

public class TurnSwitch : MonoBehaviour
{
    private const string audioCacheName = "turnswitch";

    void Awake()
    {
        Rotator rotator = this.GetComponent<Rotator>();
        if (rotator == null)
        {
            return;
        }

        rotator.OnRotated = this.OnRotated;
    }

    private void Start()
    {
        AudioClip clip = Resources.Load<AudioClip>("NoRedistributionLicense/Audio/disk-takeout1");
        AudioCache.GetInstance().AddCache(audioCacheName, clip);
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

    private void OnRotated(Rotatable rotatable, float degreee)
    {
        UserFeedbackable feedbackable = this.GetComponent<UserFeedbackable>();
        if (feedbackable == null)
        {
            return;
        }

        feedbackable.Feedback(FeedbackType.Rotate);
        // TODO: UserFeedbackable
        AudioCache.GetInstance().OneShot(audioCacheName);
    }
}
