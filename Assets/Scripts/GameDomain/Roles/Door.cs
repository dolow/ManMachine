using UnityEngine;

public class Door : MonoBehaviour
{
    private const string audioCacheName = "door";

    public bool defaultOpen = false;

    void Awake()
    {
        LinearTransform linear = this.GetComponent<LinearTransform>();
        if (linear != null)
        {
            linear.reveresed = !this.defaultOpen;

            linear.transformScale = true;

            Vector3 scale = this.transform.localScale;
            Vector3 position = this.transform.localPosition;

            linear.scaleFrom = scale;
            linear.scaleTo = new Vector3(0.0f, scale.y, scale.z);

            linear.transformPosition = true;
            linear.localPosition = true;
            linear.positionFrom = position;
            linear.positionTo = this.transform.position + (this.transform.right * this.transform.localScale.x * 0.5f);

            if (this.defaultOpen)
            {
                linear.End();
            }
            else
            {
                linear.Begin();
            }
        }

        Unlockee unlockee = this.GetComponent<Unlockee>();
        unlockee.OnUnlock += this.Toggle;
    }

    private void Start()
    {
        AudioClip clip = Resources.Load<AudioClip>("NoRedistributionLicense/Audio/train-door2");
        AudioCache.GetInstance().AddCache(audioCacheName, clip);
    }

    public void Open()
    {
        LinearTransform linear = this.GetComponent<LinearTransform>();
        if (linear != null)
        {
            linear.reveresed = true;
            // TODO: UserFeedbackable
            AudioCache.GetInstance().OneShot(audioCacheName);
        }
    }

    public void Close()
    {
        LinearTransform linear = this.GetComponent<LinearTransform>();
        if (linear != null)
        {
            linear.reveresed = false;
            // TODO: UserFeedbackable
            AudioCache.GetInstance().OneShot(audioCacheName);
        }
    }

    private void Toggle(Unlockable unlockable, Unlocker unlocker)
    {
        LinearTransform linear = this.GetComponent<LinearTransform>();
        if (linear == null)
        {
            return;
        }

        if (linear.IsFinished())
        {
            linear.reveresed = !linear.reveresed;
        }

        // TODO: UserFeedbackable
        AudioCache.GetInstance().OneShot(audioCacheName);
    }
}
