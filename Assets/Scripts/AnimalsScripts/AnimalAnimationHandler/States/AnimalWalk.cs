using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalWalk : BaseAnimalState
{
    public override void EnterState(AnimalAi movement)
    {
        if(!movement.isPlayerNear)
        {
            movement.animalAnimator.SetBool("walk", true);
        }
        else
        {
            ExitState(movement, movement.Run);
        }
    }

    public override void UpdateState(AnimalAi movement)
    {
        if(movement.navMeshAgent.velocity.magnitude <= 0.1f && !movement.isPlayerNear) 
        {
            ExitState(movement, movement.Eat);
        }
    }
    public override void ExitState(AnimalAi movement, BaseAnimalState state)
    {
        movement.animalAnimator.SetBool("walk", false);
        movement.SwitchState(state);
    }
}
