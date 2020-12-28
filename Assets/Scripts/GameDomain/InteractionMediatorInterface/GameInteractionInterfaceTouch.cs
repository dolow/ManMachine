using UnityEngine;

public class GameInteractionInterfaceTouch : AGameInteractionInterface
{
    public float screenInteractionMaxX = 300.0f;
    public float screenInteractionMaxY = 300.0f;

    private TouchInteraction touchInteraction = null;

    private void Awake()
    {
        this.touchInteraction = this.gameObject.AddComponent<TouchInteraction>();

        this.touchInteraction.OnHolding += this.TouchMove;
        this.touchInteraction.OnHoldEnding += this.TouchStop;
        this.touchInteraction.OnTap += this.TouchAction;
    }

    protected void TouchMove(TouchInteraction interaction)
    {
        Vector3 moveVector = interaction.GetHoldMoveVector(this.screenInteractionMaxX, this.screenInteractionMaxY);
        this.moveDirection.z = moveVector.y;
        this.rotation = moveVector.x;

        this.AddIntent(InteractionSemantic.MoveAny);
        this.AddIntent(InteractionSemantic.RotateAny);
    }

    protected void TouchStop(TouchInteraction interaction)
    {
        this.RemoveIntent(InteractionMediator.MoveIntentions);
        this.RemoveIntent(InteractionSemantic.Interact);
        this.ClearMovement();
    }

    protected void TouchAction(TouchInteraction interaction)
    {
        this.AddIntent(InteractionSemantic.Interact);
    }
}
