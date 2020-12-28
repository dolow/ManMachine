using UnityEngine;

public class Goal : MonoBehaviour
{
    private void Awake()
    {
        Achievee achievee = this.GetComponent<Achievee>();
        if (achievee != null)
        {
            achievee.Achieve += this.Achieve;
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        GameObject targetGameObject = collider.gameObject;
        Achievable achievable = targetGameObject.GetComponent<Achievable>();
        if (achievable == null)
        {
            return;
        }

        Achiever achiever = this.GetComponent<Achiever>();
        if (achiever == null)
        {
            return;
        }

        achiever.Achieve(achievable);
    }

    private void OnTriggerExit(Collider collider)
    {
        GameObject targetGameObject = collider.gameObject;
        Achievable achievable = targetGameObject.GetComponent<Achievable>();
        if (achievable == null)
        {
            return;
        }

        Achiever achiever = this.GetComponent<Achiever>();
        Achievee achievee;
        if (achiever.achievee != null)
        {
            achievee = achiever.achievee;
        }
        else
        {
            achievee = achievable.RequestAchievee(achiever);
        }

        if (achievee != null)
        {
            achievee.CancelAchieve(achiever);
        }
    }

    private void Achieve(Achievable achievable, Achiever achiever)
    {
        
    }
}
