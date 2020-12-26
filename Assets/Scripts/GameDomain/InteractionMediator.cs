using System;
using UnityEngine;

/// <summary>
/// InteractionMediator mediates user's primitive interaction to semantic game control 
/// </summary>
public partial class InteractionMediator : MonoBehaviour
{
    public delegate void Move(float front, float right, float rotate);
    public delegate void Stop();
    public delegate void SwitchMainCamera();
    public delegate void SwitchShoulderCameraSide();
    public delegate void RestartLevel();
    public delegate void Action();

    public Move RequestMove = null;
    public Stop RequestStop = null;
    public SwitchMainCamera RequestSwitchMainCamera = null;
    public SwitchShoulderCameraSide RequestSwitchShoulderCameraSide = null;
    public RestartLevel RequestRestartLevel = null;
    public Action RequestAction = null;

    private ScreenInteraction screenInteraction = null;
    private KeyboardInteraction keyboardInteraction = null;
    private UIInteraction uiInteraction = null;

    private Vector3 moveDirection = Vector3.zero;
    private bool moving = false;
    private float rotate = 0.0f;
    private bool switchMainCameraTrigger = false;
    private bool switchShoulderCameraSideTrigger = false;
    private bool restartLevel = false;
    private bool action = false;

    void Awake()
    {
        this.screenInteraction = this.gameObject.AddComponent<ScreenInteraction>();
        this.keyboardInteraction = this.gameObject.AddComponent<KeyboardInteraction>();
        this.uiInteraction = this.gameObject.AddComponent<UIInteraction>();

        this.AwakeKeyboardInteraction(InteractionMediator.DefaultKeyboard);
        this.AwakeScreenInteraction();
        this.AwakeUIInteraction();
    }

    void Update()
    {
        if (!this.moving)
        {
            this.RequestStop?.Invoke();
        }
        else
        {
            this.RequestMove?.Invoke(this.moveDirection.z, this.moveDirection.x, this.rotate);
            this.moveDirection = Vector3.zero;
            this.rotate = 0.0f;
        }

        if (this.action)
        {
            this.action = false;
            if (!this.switchMainCameraTrigger && !this.switchShoulderCameraSideTrigger)
            {
                this.RequestAction();
            }
        }

        if (this.switchMainCameraTrigger)
        {
            this.switchMainCameraTrigger = false;
            this.RequestSwitchMainCamera();
        }
        if (this.switchShoulderCameraSideTrigger)
        {
            this.switchShoulderCameraSideTrigger = false;
            this.RequestSwitchShoulderCameraSide();
        }
        if (this.restartLevel)
        {
            this.restartLevel = false;
            this.RequestRestartLevel();
        }
    }
}
