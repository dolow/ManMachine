using System;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionSemantic
{
    MoveForward,
    MoveBackward,
    MoveLeft,
    MoveRight,
    MoveAny,

    RotateLeft,
    RotateRight,
    RotateAny,

    SwitchMainCamera,
    SwitchShoulderCameraSide,

    RestartLevel,

    Interact
}

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

    public bool keyboard = false;
    public bool touchScreen = false;
    public bool mouse = false;
    public bool ui = false;

    public UIInteractionRegistry uiInteractionRegistry = null;

    public Move RequestMove = null;
    public Stop RequestStop = null;
    public SwitchMainCamera RequestSwitchMainCamera = null;
    public SwitchShoulderCameraSide RequestSwitchShoulderCameraSide = null;
    public RestartLevel RequestRestartLevel = null;
    public Action RequestAction = null;

    private List<AGameInteractionInterface> interactionInterfaces = new List<AGameInteractionInterface>();

    public static int MoveIntentions
    {
        get
        {
            return (
                (1 << (int)InteractionSemantic.MoveAny) |
                (1 << (int)InteractionSemantic.RotateAny) |
                (1 << (int)InteractionSemantic.MoveForward) |
                (1 << (int)InteractionSemantic.MoveBackward) |
                (1 << (int)InteractionSemantic.MoveLeft) |
                (1 << (int)InteractionSemantic.MoveRight) |
                (1 << (int)InteractionSemantic.RotateLeft) |
                (1 << (int)InteractionSemantic.RotateRight)
            );
        }
        set { }
    }
    public static int CameraIntentions
    {
        get
        {
            return (
                (1 << (int)InteractionSemantic.SwitchMainCamera) |
                (1 << (int)InteractionSemantic.SwitchShoulderCameraSide)
            );
        }
        set { }
    }
    public static int InteractionIntentions
    {
        get
        {
            return (
                (1 << (int)InteractionSemantic.Interact)
            );
        }
        set { }
    }


    private void Awake()
    {
        if (this.keyboard)
        {
            this.interactionInterfaces.Add(this.gameObject.AddComponent<GameInteractionInterfaceKeyboard>());
        }
        if (this.touchScreen)
        {
            this.interactionInterfaces.Add(this.gameObject.AddComponent<GameInteractionInterfaceTouch>());
        }
        if (this.mouse)
        {
            this.interactionInterfaces.Add(this.gameObject.AddComponent<GameInteractionInterfaceMouse>());
        }
        if (this.ui)
        {
            GameInteractionInterfaceUI interactionMediatorUi = this.gameObject.AddComponent<GameInteractionInterfaceUI>();
            this.interactionInterfaces.Add(interactionMediatorUi);
            interactionMediatorUi.SetUIInteractionRegistry(this.uiInteractionRegistry);
        }
    }

    private void Update()
    {
        if (!this.HasAnyIntention(MoveIntentions))
        {
            this.RequestStop?.Invoke();
        }
        else
        {
            this.ClearAnyIntention(MoveIntentions);

            Vector3 movement = this.CompositMoveDirection();
            float rotation = this.CompositRotation();

            this.RequestMove?.Invoke(movement.z, movement.x, rotation);
        }

        if (this.HasAnyIntention(InteractionIntentions))
        {
            this.ClearAnyIntention(InteractionIntentions);
            if (!this.HasAnyIntention(CameraIntentions))
            {
                this.RequestAction?.Invoke();
            }
        }

        if (this.HasAnyIntention(InteractionSemantic.SwitchMainCamera))
        {
            this.ClearAnyIntention(InteractionSemantic.SwitchMainCamera);
            this.RequestSwitchMainCamera?.Invoke();
        }
        if (this.HasAnyIntention(InteractionSemantic.SwitchShoulderCameraSide))
        {
            this.ClearAnyIntention(InteractionSemantic.SwitchShoulderCameraSide);
            this.RequestSwitchShoulderCameraSide?.Invoke();
        }
        if (this.HasAnyIntention(InteractionSemantic.RestartLevel))
        {
            this.ClearAnyIntention(InteractionSemantic.RestartLevel);
            this.RequestRestartLevel?.Invoke();
        }
    }

    private bool HasAnyIntention(InteractionSemantic semantic)
    {
        int flag = 1 << (int)semantic;
        for (int i = 0; i < this.interactionInterfaces.Count; i++)
        {
            if (this.interactionInterfaces[i].HasIntent(flag))
            {
                return true;
            }
        }

        return false;
    }

    private bool HasAnyIntention(int semantics)
    {
        for (int i = 0; i < this.interactionInterfaces.Count; i++)
        {
            if (this.interactionInterfaces[i].HasIntent(semantics))
            {
                return true;
            }
        }

        return false;
    }

    private void ClearAnyIntention(InteractionSemantic semantic)
    {
        int flag = 1 << (int)semantic;
        for (int i = 0; i < this.interactionInterfaces.Count; i++)
        {
            this.interactionInterfaces[i].RemoveIntent(flag);
        }
    }
    private void ClearAnyIntention(int semantics)
    {
        for (int i = 0; i < this.interactionInterfaces.Count; i++)
        {
            this.interactionInterfaces[i].RemoveIntent(semantics);
        }
    }

    private Vector3 CompositMoveDirection()
    {
        Vector3 movement = Vector3.zero;

        for (int i = 0; i < this.interactionInterfaces.Count; i++)
        {
            movement += this.interactionInterfaces[i].MoveDirection;
        }

        return movement;
    }
    private float CompositRotation()
    {
        float rotation = 0.0f;

        for (int i = 0; i < this.interactionInterfaces.Count; i++)
        {
            rotation += this.interactionInterfaces[i].Rotation;
        }

        return rotation;
    }
}
