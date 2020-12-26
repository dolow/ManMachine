using System.Collections.Generic;
using UnityEngine;

public class ScreenInteractionDeviceTouch : IScreenInteractionDevice
{
    public bool IsInteracting()
    {
        Touch[] touches = Input.touches;
        return touches.Length > 0;
    }

    public Vector3 InteractingPosition()
    {
        Touch[] touches = Input.touches;
        if (touches.Length == 0)
        {
            return Vector3.zero;
        }
        Touch touch = Input.GetTouch(0);
        
        return new Vector3(touch.position.x, touch.position.y, 0);
    }
    public List<Vector3> InteractingPositions()
    {
        List<Vector3> list = new List<Vector3>();
        Touch[] touches = Input.touches;
        if (touches.Length == 0)
        {
            return list;
        }

        for (int i = 0; i < touches.Length; i++)
        {
            Touch touch = Input.GetTouch(i);
            list.Add(new Vector3(touch.position.x, touch.position.y, 0));
        }

        return list;
    }
}
