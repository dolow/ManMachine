using UnityEngine;

public partial class InteractionMediator : MonoBehaviour
{
    public float screenInteractionMaxX = 300.0f;
    public float screenInteractionMaxY = 300.0f;

    private void AwakeScreenInteraction()
    {
        this.screenInteraction.OnHolding += this.ScreenMove;
        this.screenInteraction.OnHoldEnding += this.ScreenStop;
        this.screenInteraction.OnTap += this.ScreenAction;
    }

    protected void ScreenMove(ScreenInteraction interaction)
    {
        this.moving = true;
        Vector3 moveVector = interaction.GetHoldMoveVector(this.screenInteractionMaxX, this.screenInteractionMaxY);
        this.moveDirection.z = moveVector.y;
        this.rotate = moveVector.x;
    }

    protected void ScreenStop(ScreenInteraction interaction)
    {
        this.moving = false;
    }

    protected void ScreenAction(ScreenInteraction interaction)
    {
        this.action = true;
    }
}
