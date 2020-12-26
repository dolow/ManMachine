using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Attach this component for each individual UI elements.
/// UIInteractionRegistry should be set statically.
/// </summary>
public class UIInteractionRegisterer : MonoBehaviour
{
    public int buttonId = -1;
    public UIInteractionRegistry registry = null;

    public void Awake()
    {
        Button button = this.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(this.OnButtonPressed);
        }
    }

    public void OnButtonPressed()
    {
        
        if (this.registry != null)
        {
            this.registry.OnButtonPressed(this);
        }
    }
}
