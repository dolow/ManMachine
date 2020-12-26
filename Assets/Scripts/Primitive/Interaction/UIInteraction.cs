using System;
using UnityEngine;

/// <summary>
/// UIInteractionRegistry and UIInteractionRegister handles UI interaction.
/// UIInteractionRegisterer receives any interactions and UIInteractionRegistry treats interactions as primitive.
/// Primitive interactions are passed to UIInteraction then broadcasted to its user.
/// </summary>
public class UIInteraction : MonoBehaviour
{
    public delegate void PressButton(UIInteraction interaction, UIInteractionRegisterer registerer);

    public PressButton OnButtonPressed = null;

    public UIInteractionRegistry Registry
    {
        get {
            return this.registry;
        }
        set
        {
            if (this.registry == null)
            {
                this.registry = value;
            }
        }
    }

    private UIInteractionRegistry registry = null;

    public void Update()
    {
        if (this.Registry == null)
        {
            return;
        }
        
        // button press
        {
            foreach (UIInteractionRegisterer registerer in this.Registry.PressedButtons) {
                this.OnButtonPressed?.Invoke(this, registerer);
            }
            this.Registry.ClearPressedButtons();
        }
    }
}
