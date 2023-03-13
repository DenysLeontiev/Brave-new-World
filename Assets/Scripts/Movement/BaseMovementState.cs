using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMovementState
{
    public abstract void EnterState(PlayerMovement movement);
    public abstract void UpdateState(PlayerMovement movement);
    public abstract void ExitState(PlayerMovement movement, BaseMovementState state);
}
