using UnityEngine;


public class MouseInteraction : MonoBehaviour
{
    public delegate void ClickBegan(MouseInteraction interaction);
    public delegate void Clicking(MouseInteraction interaction);
    public delegate void ClickEnding(MouseInteraction interaction);
    public delegate void Click(MouseInteraction interaction);

    public ClickBegan OnClickBegan = null;
    public Clicking OnClicking = null;
    public ClickEnding OnClickEnding = null;
    public Click OnClick = null;

    public float tapDuration = 0.1f;

    protected float clickingDuration = 0.0f;
    protected IScreenInteractionDevice device = null;

    protected bool clicking = false;

    protected Vector3 positionClickBegan = Vector3.zero;
    protected Vector3 positionClickEnd = Vector3.zero;

    void Awake()
    {
        this.device = new ScreenInteractionDeviceClick();
    }

    void Update()
    {
        if (this.device.IsInteracting())
        {
            this.positionClickEnd = this.device.InteractingPosition();

            if (!this.clicking)
            {
                this.clicking = true;
                this.positionClickBegan = this.device.InteractingPosition();
                this.clickingDuration = 0.0f;
                this.OnClickBegan?.Invoke(this);
            }
            else
            {
                this.clicking = true;
                this.clickingDuration += Time.deltaTime;
                this.OnClicking?.Invoke(this);
            }
        }
        else
        {
            if (this.clicking)
            {
                if (this.clickingDuration <= this.tapDuration)
                {
                    this.OnClick?.Invoke(this);
                }
                this.OnClickEnding?.Invoke(this);
            }

            this.clicking = false;
            this.positionClickBegan = Vector3.zero;
            this.clickingDuration = 0.0f;
        }
    }

    public float ClickingDuration()
    {
        return this.clickingDuration;
    }

    public bool ClickingOver(float seconds)
    {
        return this.clickingDuration >= seconds;
    }

    public Vector3 GetLastClickMove()
    {
        return this.positionClickEnd - this.positionClickBegan;
    }
    public Vector3 GetInitialClickPosition()
    {
        return this.positionClickBegan;
    }
    public Vector3 GetLastClickPosition()
    {
        return this.positionClickEnd;
    }
    public Vector3 GetClickMoveVector(float maxX, float maxY)
    {
        Vector3 initialPos = this.GetInitialClickPosition();
        Vector3 lastPosition = this.GetLastClickPosition();

        Vector3 direction = lastPosition - initialPos;

        float x = Mathf.Min(Mathf.Abs(direction.x), maxX) / maxX;
        float y = Mathf.Min(Mathf.Abs(direction.y), maxY) / maxY;

        return new Vector3(direction.x < 0 ? -x : x, direction.y < 0 ? -y : y, 0.0f);
    }
}
