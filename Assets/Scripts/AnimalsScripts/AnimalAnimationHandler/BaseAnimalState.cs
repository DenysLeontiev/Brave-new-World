using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAnimalState
{
    public abstract void EnterState(AnimalAi movement);
    public abstract void UpdateState(AnimalAi movement);
    public abstract void ExitState(AnimalAi movement, BaseAnimalState state);
}
