using UnityEngine;

public class Agent : Actor
{
    private static string audioRedirecteeCacheName = "directee";

    public float moveSpeed = 1.0f;

    private float collapseDistanceWithActor = 0.75f;

    private void Awake()
    {
        Redirectable redirectable = this.GetComponent<Redirectable>();
        if (redirectable != null)
        {
            redirectable.OnRequestRedirectee += this.ReturnSelf;
        }

        Redirectee redirectee = this.GetComponent<Redirectee>();
        if (redirectee != null)
        {
            redirectee.Redirect += this.Redirect;
            redirectable.OnRequestRedirectee += this.ReturnSelf;
        }

        Spawnee spawnee = this.GetComponent<Spawnee>();
        if (spawnee != null)
        {
            spawnee.OnSpawned += this.OnSpawned;
        }
    }

    protected override void Start()
    {
        base.Start();

        AudioClip clip = Resources.Load<AudioClip>("NoRedistributionLicense/Audio/machine-hit11");
        AudioCache.GetInstance().AddCache(audioRedirecteeCacheName, clip);
    }

    private void OnTriggerStay(Collider other)
    {
        if (this.state == State.Collapse)
        {
            return;
        }

        Actor actor = other.gameObject.GetComponent<Agent>();

        if (actor != null && !GameObject.ReferenceEquals(this.gameObject, actor.gameObject))
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

    private Redirectee ReturnSelf(Redirector redirector)
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

    private void OnSpawned(Spawner spawner, Spawnable spawnable)
    {
        AutoDestroyDistance autoDestroy = this.GetComponent<AutoDestroyDistance>();
        if (autoDestroy == null)
        {
            autoDestroy = this.gameObject.AddComponent<AutoDestroyDistance>();
        }

        autoDestroy.baseTransform = spawner.transform;
        autoDestroy.distance = 10.0f;

        Walker walker = this.GetComponent<CharacterControllerWalker>();
        walker.MoveFront(this.moveSpeed);
    }
}
