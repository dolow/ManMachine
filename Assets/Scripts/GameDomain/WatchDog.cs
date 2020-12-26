using UnityEngine;

public class WatchDog : Actor
{
    private static string audioRedirecteeCacheName = "directee";

    public float chaseSpeed = 5.0f;

    private float collapseDistanceWithActor = 0.75f;

    protected override void Start()
    {
        base.Start();

        AudioClip clip = Resources.Load<AudioClip>("NoRedistributionLicense/Audio/machine-hit11");
        AudioCache.GetInstance().AddCache(audioRedirecteeCacheName, clip);
    }

    private void OnTriggerEnter(Collider other)
    {
        Actor actor = other.gameObject.GetComponent<Agent>();
        if (actor == null)
        {
            actor = other.gameObject.GetComponent<Player>();
        }

        if (actor == null)
        {
            return;
        }

        if (this.state == State.Idle)
        {
            this.state = State.Chasing;
            this.Run(this.chaseSpeed, 0.0f, 0.0f);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (this.state != State.Chasing)
        {
            return;
        }

        Actor actor = other.gameObject.GetComponent<Agent>();
        if (actor == null)
        {
            actor = other.gameObject.GetComponent<Player>();
        }

        if (actor != null)
        {
            if (Vector3.Distance(other.transform.position, this.transform.position) < this.collapseDistanceWithActor)
            {
                if (actor.state != State.Collapse)
                {
                    actor.Collapse();
                }
                this.Collapse();
                return;
            }
        }
    }

    Redirectee ReturnSelf(Redirector redirector)
    {
        return this.GetComponent<Redirectee>();
    }

    private void Redirect(Redirectable redirectable, Redirector redirector, Vector3 newDirection)
    {
        Vector3 lookPos = new Vector3(newDirection.x, this.transform.position.y, newDirection.z);

        this.transform.LookAt(lookPos);

        // TODO: UserFeedbackable
        AudioCache.GetInstance().OneShot(audioRedirecteeCacheName);
    }
}
