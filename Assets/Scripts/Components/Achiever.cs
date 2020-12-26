using UnityEngine;

public class Achiever : MonoBehaviour
{
    public Achievee achievee = null;

    public void Achieve(Achievable achievable)
    {
        if (this.achievee != null)
        {
            this.achievee.RequestAchieve(achievable, this);
        }
        else
        {
            Achievee achievee = achievable.RequestAchievee(this);
            if (achievee != null)
            {
                achievee.RequestAchieve(achievable, this);
            }
        }
    }
}
