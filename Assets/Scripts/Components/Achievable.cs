using UnityEngine;

public class Achievable : MonoBehaviour
{
    public delegate Achievee GetAchievee(Achiever achiever);

    public GetAchievee OnRequestAchievee = null;

    public Achievee RequestAchievee(Achiever achiever)
    {
        return this.OnRequestAchievee?.Invoke(achiever);
    }
}
