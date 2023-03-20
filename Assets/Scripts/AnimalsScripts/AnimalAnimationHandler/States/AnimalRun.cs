using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalRun : BaseAnimalState
{
    public override void EnterState(AnimalAi movement)
    {
        movement.animalAnimator.SetBool("run", true);
    }

    public override void UpdateState(AnimalAi movement)
    {
        if(!movement.isPlayerNear) 
        {
            ExitState(movement, movement.Walk);
        }
    }

    public override void ExitState(AnimalAi movement, BaseAnimalState state)
    {
        movement.animalAnimator.SetBool("run", false);
        movement.SwitchState(state);
    }
}
