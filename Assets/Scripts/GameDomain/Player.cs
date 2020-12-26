using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    private static string audioRedirecteeCacheName = "directee";

    private Vector3 initialPosition = Vector3.zero;

    public enum ActionableType
    {
        Unlock,
        Redirect,
        Spawn,
        Rotate
    }

    private Dictionary<ActionableType, GameObject> actionables = new Dictionary<ActionableType, GameObject>();

    private void Awake()
    {
        this.initialPosition = this.transform.position;

        Redirectable redirectable = this.GetComponent<Redirectable>();
        if (redirectable != null)
        {
            redirectable.OnRequestRedirectee += this.ReturnSelf;
        }

        Redirectee redirectee = this.GetComponent<Redirectee>();
        if (redirectee != null)
        {
            redirectee.Redirect += this.Redirect;
        }
    }

    protected override void Start()
    {
        base.Start();

        AudioClip clip = Resources.Load<AudioClip>("NoRedistributionLicense/Audio/machine-hit11");
        AudioCache.GetInstance().AddCache(audioRedirecteeCacheName, clip);
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

    public void Respawn()
    {
        this.transform.position = this.initialPosition;

        CharacterController controller = this.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = true;
        }

        CapsuleCollider collider = this.GetComponent<CapsuleCollider>();
        if (collider != null)
        {
            collider.enabled = true;
        }

        this.state = State.Idle;
    }

    public void TryAction()
    {
        if (this.actionables.ContainsKey(ActionableType.Unlock))
        {
            GameObject go = this.actionables[ActionableType.Unlock];
            Unlocker unlocker = go.GetComponent<Unlocker>();
            unlocker.Unlock(this.GetComponent<Unlockable>());
        }
        else if (this.actionables.ContainsKey(ActionableType.Rotate))
        {
            GameObject go = this.actionables[ActionableType.Rotate];
            Rotator rotator = go.GetComponent<Rotator>();
            TurnSwitch turnSwitch = rotator.GetComponent<TurnSwitch>();
            if (turnSwitch == null)
            {
                // unknown rotator
                return;
            }
            rotator.Rotate(this.GetComponent<Rotatable>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        this.AddActionableIfEligible<Unlockable, Unlocker>(other.gameObject, ActionableType.Unlock);
        this.AddActionableIfEligible<Rotatable, Rotator>(other.gameObject, ActionableType.Rotate);
    }

    private void OnTriggerExit(Collider other)
    {
        this.RemoveActionableIfEligible<Unlockable, Unlocker>(other.gameObject, ActionableType.Unlock);
        this.RemoveActionableIfEligible<Rotatable, Rotator>(other.gameObject, ActionableType.Rotate);
    }

    private bool AddActionableIfEligible<Capability, Effectability>(GameObject worker, ActionableType type)
    {
        Capability c = this.GetComponent<Capability>();
        if (c == null)
        {
            return false;
        }

        Effectability e = worker.GetComponent<Effectability>();
        if (e == null)
        {
            return false;
        }
        
        this.actionables.Add(type, worker);

        return true;
    }
    private bool RemoveActionableIfEligible<Capability, Effectability>(GameObject worker, ActionableType type)
    {
        Capability c = this.GetComponent<Capability>();
        if (c == null)
        {
            return false;
        }

        Effectability e = worker.GetComponent<Effectability>();
        if (e == null)
        {
            return false;
        }

        this.actionables.Remove(type);

        return true;
    }
}
