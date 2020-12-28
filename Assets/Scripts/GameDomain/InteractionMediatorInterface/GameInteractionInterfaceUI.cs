public class GameInteractionInterfaceUI : AGameInteractionInterface
{
    private UIInteraction uiInteraction = null;

    private void Awake()
    {
        this.uiInteraction = this.gameObject.AddComponent<UIInteraction>();
        this.uiInteraction.OnButtonPressed += this.PressButton;
    }

    public void SetUIInteractionRegistry(UIInteractionRegistry registry)
    {
        this.uiInteraction.Registry = registry;
    }

    private void PressButton(UIInteraction interaction, UIInteractionRegisterer registerer)
    {
        if (registerer.buttonId == 1)
        {
            this.AddIntent(InteractionSemantic.SwitchMainCamera);
        }
        else if (registerer.buttonId == 2)
        {
            this.AddIntent(InteractionSemantic.SwitchShoulderCameraSide);
        }
        else if (registerer.buttonId == 3)
        {
            this.AddIntent(InteractionSemantic.RestartLevel);
        }
    }
}
