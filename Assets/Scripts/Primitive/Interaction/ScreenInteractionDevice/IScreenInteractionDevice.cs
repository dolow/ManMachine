using System.Collections.Generic;
using UnityEngine;

public interface IScreenInteractionDevice
{
    bool IsInteracting();
    Vector3 InteractingPosition();
    List<Vector3> InteractingPositions();

    // TODO: mouse hover
}
