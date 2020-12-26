using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Each gameobject with UIInteractionRegisterer directs its registry.
/// If any interaction event like pressing button occurs, they send message to this.
/// UIInteractionRegistry treat them as primitive interaction and pass through UIInteraction.
/// </summary>
public class UIInteractionRegistry : MonoBehaviour
{
    protected Dictionary<int, UIInteractionRegisterer> pressedButtons = new Dictionary<int, UIInteractionRegisterer>();

    public IEnumerable<UIInteractionRegisterer> PressedButtons
    {
        get
        {
            foreach (KeyValuePair<int, UIInteractionRegisterer> entry in this.pressedButtons)
            {
                yield return entry.Value;
            }
        }
        private set { }
    }

    public void ClearPressedButtons()
    {
        this.pressedButtons.Clear();
    }

    public void OnButtonPressed(UIInteractionRegisterer registerer)
    {
        int id = registerer.GetInstanceID();
        if (!this.pressedButtons.ContainsKey(id))
        {
            this.pressedButtons.Add(id, registerer);
        }
    }
}
