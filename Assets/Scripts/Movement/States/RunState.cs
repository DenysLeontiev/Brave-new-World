using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : BaseMovementState
{
    public override void EnterState(PlayerMovement movement)
    {
        movement.playerAnimator.SetBool("run", true);
        movement.moveSpeed = movement.runSpeed;
    }

    public override void UpdateState(PlayerMovement movement)
    {
        if(Input.GetKeyUp(KeyCode.LeftShift)) 
        {
            ExitState(movement, movement.Walk);
        }
    }

    public override void ExitState(PlayerMovement movement, BaseMovementState state)
    {
        movement.playerAnimator.SetBool("run", false);
        movement.SwitchState(state);    
    }
}
