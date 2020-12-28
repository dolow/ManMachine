using UnityEngine;

public class TurnTable : MonoBehaviour
{
    private void Awake()
    {
        Rotatee rotatee = this.GetComponent<Rotatee>();
        if (rotatee != null)
        {
            rotatee.Rotate += this.Rotate;
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        GameObject targetGameObject = collider.gameObject;
        Redirectable redirectable = targetGameObject.GetComponent<Redirectable>();
        if (redirectable == null)
        {
            return;
        }

        Redirector redirector = this.GetComponent<Redirector>();
        if (redirector == null)
        {
            return;
        }

        Vector3 diff = this.transform.position - collider.transform.position;
        redirector.Redirect(redirectable, this.transform.position - diff + this.transform.forward);
    }
    
    private void OnTriggerExit(Collider collider)
    {
        GameObject targetGameObject = collider.gameObject;
        Redirectable redirectable = targetGameObject.GetComponent<Redirectable>();
        if (redirectable == null)
        {
            return;
        }

        Redirector redirector = this.GetComponent<Redirector>();
        if (redirector.redirectee != null)
        {
            redirector.redirectee.CancelRedirect(redirector);
        }
        else
        {
            Redirectee redirectee = redirectable.RequestRedirectee(redirector);
            if (redirectee != null)
            {
                redirectee.CancelRedirect(redirector);
            }
        }
    }

    private void Rotate(Rotatable rotatable, Rotator rotator, float degree)
    {
        Vector3 euler = this.transform.rotation.eulerAngles;
        euler.y += degree;
        this.transform.rotation = Quaternion.Euler(euler);
    }
}
