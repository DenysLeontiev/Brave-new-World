using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalEat : BaseAnimalState
{
    public override void EnterState(AnimalAi movement)
    {
        movement.animalAnimator.SetBool("eat", true);
    }

    public override void UpdateState(AnimalAi movement)
    {
        if(movement.navMeshAgent.velocity.magnitude > 0.1f) 
        {
            ExitState(movement, movement.Walk);
        }

    }

    public override void ExitState(AnimalAi movement, BaseAnimalState state)
    {
        movement.animalAnimator.SetBool("eat", false);
        movement.SwitchState(state);
    }
}
