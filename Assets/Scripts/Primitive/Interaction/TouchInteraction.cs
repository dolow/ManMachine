using System;
using UnityEngine;

// TODO: multi touch
public class TouchInteraction : MonoBehaviour
{
    public delegate void HoldBegan(TouchInteraction interaction);
    public delegate void Holding(TouchInteraction interaction);
    public delegate void HoldEnding(TouchInteraction interaction);
    public delegate void Tap(TouchInteraction interaction);

    public HoldBegan OnHoldBegan = null;
    public Holding OnHolding = null;
    public HoldEnding OnHoldEnding = null;
    public Tap OnTap = null;

    public float tapDuration = 0.1f;

    protected float holdingDuration = 0.0f;
    protected IScreenInteractionDevice device = null;

    protected bool holding = false;

    protected Vector3 positionHoldBegan = Vector3.zero;
    protected Vector3 positionHoldEnd = Vector3.zero;

    void Awake()
    {
        this.SetDevice(Application.platform);
    }

    void Update()
    {
        if (this.device.IsInteracting())
        {
            this.positionHoldEnd = this.device.InteractingPosition();

            if (!this.holding)
            {
                this.holding = true;
                this.positionHoldBegan = this.device.InteractingPosition();
                this.holdingDuration = 0.0f;
                this.OnHoldBegan?.Invoke(this);
            }
            else
            {
                this.holding = true;
                this.holdingDuration += Time.deltaTime;
                this.OnHolding?.Invoke(this);
            }
        }
        else
        {
            if (this.holding)
            {
                if (this.holdingDuration <= this.tapDuration)
                {
                    this.OnTap?.Invoke(this);
                }
                this.OnHoldEnding?.Invoke(this);
            }

            this.holding = false;
            this.positionHoldBegan = Vector3.zero;
            this.holdingDuration = 0.0f;
        }
    }

    public float HoldingDuration()
    {
        return this.holdingDuration;
    }

    public bool HoldingOver(float seconds)
    {
        return this.holdingDuration >= seconds;
    }

    public Vector3 GetLastHoldMove()
    {
        return this.positionHoldEnd - this.positionHoldBegan;
    }
    public Vector3 GetInitialHoldPosition()
    {
        return this.positionHoldBegan;
    }
    public Vector3 GetLastHoldPosition()
    {
        return this.positionHoldEnd;
    }
    public Vector3 GetHoldMoveVector(float maxX, float maxY)
    {
        Vector3 initialPos = this.GetInitialHoldPosition();
        Vector3 lastPosition = this.GetLastHoldPosition();

        Vector3 direction = lastPosition - initialPos;

        float x = Mathf.Min(Mathf.Abs(direction.x), maxX) / maxX;
        float y = Mathf.Min(Mathf.Abs(direction.y), maxY) / maxY;

        return new Vector3(direction.x < 0 ? -x : x, direction.y < 0 ? -y : y, 0.0f);
    }

    private void SetDevice(RuntimePlatform platform)
    {
        switch (platform)
        {
            case RuntimePlatform.Android:
            case RuntimePlatform.IPhonePlayer:
                {
                    this.device = new ScreenInteractionDeviceTouch();
                    break;
                }
            default:
                {
                    this.device = new ScreenInteractionDeviceClick();
                    break;
                }
        }
    }
}
