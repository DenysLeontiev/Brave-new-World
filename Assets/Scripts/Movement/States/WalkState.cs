using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : BaseMovementState
{
    public override void EnterState(PlayerMovement movement)
    {
        movement.playerAnimator.SetBool("walk", true);
        movement.moveSpeed = movement.walkSpeed;
    }

    public override void UpdateState(PlayerMovement movement)
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            ExitState(movement, movement.Run);
        }
    }

    public override void ExitState(PlayerMovement movement, BaseMovementState state)
    {
        movement.playerAnimator.SetBool("walk", false);
        movement.SwitchState(state);
    }
}
