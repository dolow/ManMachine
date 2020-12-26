using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenInteractionDeviceClick : IScreenInteractionDevice
{
    public bool IsInteracting()
    {
        return Input.GetMouseButton(0);
    }

    public Vector3 InteractingPosition()
    {
        return Input.mousePosition;
    }

    public List<Vector3> InteractingPositions()
    {
        return new List<Vector3>()
        {
            Input.mousePosition
        };
    }
}
