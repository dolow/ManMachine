using UnityEngine;

public class AutoDestroyDistance : MonoBehaviour
{
    public Transform baseTransform = null;
    public float distance = 5.0f;

    void Update()
    {
        if (this.baseTransform == null)
        {
            return;
        }

        if (Vector3.Distance(baseTransform.position, this.transform.position) > distance)
        {
            Destroy(this.gameObject);
        }
    }
}
