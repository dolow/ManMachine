using UnityEngine;

public class GameInteractionInterfaceMouse : AGameInteractionInterface
{
    public float screenInteractionMaxX = 300.0f;
    public float screenInteractionMaxY = 300.0f;

    private MouseInteraction mouseInteraction = null;

    private void Awake()
    {
        this.mouseInteraction = this.gameObject.AddComponent<MouseInteraction>();

        this.mouseInteraction.OnClicking += this.MouseMove;
        this.mouseInteraction.OnClickEnding += this.MouseStop;
        this.mouseInteraction.OnClick += this.MouseAction;
    }

    protected void MouseMove(MouseInteraction interaction)
    {
        Vector3 moveVector = interaction.GetClickMoveVector(this.screenInteractionMaxX, this.screenInteractionMaxY);
        this.moveDirection.z = moveVector.y;
        this.rotation = moveVector.x;
        
        this.AddIntent(InteractionSemantic.MoveAny);
        this.AddIntent(InteractionSemantic.RotateAny);
    }

    protected void MouseStop(MouseInteraction interaction)
    {
        this.RemoveIntent(InteractionMediator.MoveIntentions);
        this.RemoveIntent(InteractionSemantic.Interact);
        this.ClearMovement();
    }

    protected void MouseAction(MouseInteraction interaction)
    {
        this.AddIntent(InteractionSemantic.Interact);
    }
}
