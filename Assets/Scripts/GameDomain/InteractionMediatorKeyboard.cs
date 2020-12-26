using System.Collections.Generic;
using UnityEngine;

public struct KeyboardSemantic
{
    // NOTE: consider multiple keys
    public KeyCode forward;
    public KeyCode backward;
    public KeyCode left;
    public KeyCode right;
    public KeyCode rotateLeft;
    public KeyCode rotateRight;

    public KeyCode switchMainCamera;
    public KeyCode switchShoulderCameraSide;

    public KeyCode action;

    private List<KeyCode> keyCodesCache;

    public List<KeyCode> KeyCodes {
        get {
            if (this.keyCodesCache == null)
            {
                this.keyCodesCache = new List<KeyCode> {
                    this.forward,
                    this.backward,
                    this.left,
                    this.right,
                    this.rotateLeft,
                    this.rotateRight,
                    this.switchMainCamera,
                    this.switchShoulderCameraSide,
                    this.action
                };
            }
            return this.keyCodesCache;
        }
    }
}

public partial class InteractionMediator : MonoBehaviour
{
    public static KeyboardSemantic DefaultKeyboard
    {
        get
        {
            KeyboardSemantic semantic = new KeyboardSemantic();
            semantic.forward = KeyCode.W;
            semantic.backward = KeyCode.S;
            semantic.left = KeyCode.A;
            semantic.right = KeyCode.D;
            semantic.rotateLeft = KeyCode.J;
            semantic.rotateRight = KeyCode.K;
            semantic.switchMainCamera = KeyCode.U;
            semantic.switchShoulderCameraSide = KeyCode.I;
            semantic.action = KeyCode.Space;

            return semantic;
        }
    }

    private KeyboardSemantic keySemantic;

    private void AwakeKeyboardInteraction(KeyboardSemantic semantic)
    {
        this.keySemantic = semantic;

        this.keyboardInteraction.OnHoldBegan += this.KeyPressBegan;
        this.keyboardInteraction.OnHolding += this.KeyPressing;
        this.keyboardInteraction.OnHoldEnding += this.KeyPressEnding;

        for (int i = 0; i < this.keySemantic.KeyCodes.Count; i++)
        {
            this.keyboardInteraction.AddListeningKey(this.keySemantic.KeyCodes[i]);
        }
    }

    protected void KeyPressBegan(KeyboardInteraction interaction, KeyCode key)
    {
        if (key == this.keySemantic.forward)
        {
            this.moveDirection.z = 1.0f;
            this.moving = true;
        }
        else if (key == this.keySemantic.backward)
        {
            this.moveDirection.z = -1.0f;
            this.moving = true;
        }
        else if (key == this.keySemantic.left)
        {
            this.moveDirection.x = -1.0f;
            this.moving = true;
        }
        else if (key == this.keySemantic.right)
        {
            this.moveDirection.x = 1.0f;
            this.moving = true;
        }
        else if (key == this.keySemantic.rotateLeft)
        {
            this.rotate = -1.0f;
            this.moving = true;
        }
        else if (key == this.keySemantic.rotateRight)
        {
            this.rotate = 1.0f;
            this.moving = true;
        }
        else if (key == this.keySemantic.switchMainCamera)
        {
            this.switchMainCameraTrigger = true;
        }
        else if (key == this.keySemantic.switchShoulderCameraSide)
        {
            this.switchShoulderCameraSideTrigger = true;
        }
        else if (key == this.keySemantic.action)
        {
            this.action = true;
        }
    }

    protected void KeyPressing(KeyboardInteraction interaction, KeyCode key, float duration)
    {
        if (key == this.keySemantic.forward)
        {
            this.moveDirection.z = 1.0f;
            this.moving = true;
        }
        else if (key == this.keySemantic.backward)
        {
            this.moveDirection.z = -1.0f;
            this.moving = true;
        }
        else if (key == this.keySemantic.left)
        {
            this.moveDirection.x = -1.0f;
            this.moving = true;
        }
        else if (key == this.keySemantic.right)
        {
            this.moveDirection.x = 1.0f;
            this.moving = true;
        }
        else if (key == this.keySemantic.rotateLeft)
        {
            this.rotate = -1.0f;
            this.moving = true;
        }
        else if (key == this.keySemantic.rotateRight)
        {
            this.rotate = 1.0f;
            this.moving = true;
        }
    }

    protected void KeyPressEnding(KeyboardInteraction interaction, KeyCode key, float duration)
    {
        bool moveKey = false;
        for (int i = 0; i < this.keySemantic.KeyCodes.Count; i++)
        {
            KeyCode code = this.keySemantic.KeyCodes[i];
            if (this.keyboardInteraction.IsHolding(this.keySemantic.KeyCodes[i])) {
                if (code != this.keySemantic.switchMainCamera && code != this.keySemantic.switchShoulderCameraSide)
                {
                    moveKey = true;
                    break;
                }
            }
        }

        if (!moveKey)
        {
            this.moving = false;
        }
    }
}
