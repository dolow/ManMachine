using UnityEngine;

public partial class InteractionMediator : MonoBehaviour
{
    public void SetUIInteractionRegistry(UIInteractionRegistry registry)
    {
        this.uiInteraction.Registry = registry;
    }

    private void AwakeUIInteraction()
    {
        this.uiInteraction.OnButtonPressed += this.PressButton;
    }

    private void PressButton(UIInteraction interaction, UIInteractionRegisterer registerer)
    {
        if (registerer.buttonId == 1)
        {
            this.switchMainCameraTrigger = true;
        }
        else if (registerer.buttonId == 2)
        {
            this.switchShoulderCameraSideTrigger = true;
        }
        else if (registerer.buttonId == 3)
        {
            this.restartLevel = true;
        }
    }
}
