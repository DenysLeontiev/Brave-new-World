using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AnimalAi : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Animator animalAnimator;

    private float speed;
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float runSpeed = 8f;

    [SerializeField] private float maxDistance = 0f;

    [HideInInspector] public BaseAnimalState currentState;
    [HideInInspector] public AnimalIdle Idle = new AnimalIdle();
    [HideInInspector] public AnimalWalk Walk = new AnimalWalk();
    [HideInInspector] public AnimalEat Eat = new AnimalEat();
    [HideInInspector] public AnimalRun Run = new AnimalRun();

    [HideInInspector] public bool isPlayerNear = false;

    private Vector3 randomPosition;
    private bool hasEaten = true;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animalAnimator = GetComponent<Animator>();
        randomPosition = FindRandomTargetPosition(transform.position, maxDistance);
    }

    private void Start()
    {
        SwitchState(Walk);
    }

    void Update()
    {
        HandleSpeed();
        AnimalBehaviour();
        currentState.UpdateState(this);
    }

    public void SwitchState(BaseAnimalState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    private void HandleSpeed()
    {
        navMeshAgent.speed = animalAnimator.GetBool("run") ? runSpeed : walkSpeed;
    }

    private void AnimalBehaviour()
    {
        float distanceToTargetPosition = Vector3.Distance(transform.position, randomPosition);

        float minDistance = 2f;
        if (distanceToTargetPosition < minDistance)
        {
            StartCoroutine(EatAnimal());
            randomPosition = FindRandomTargetPosition(transform.position, maxDistance);
        }
        else if(hasEaten && !isPlayerNear)
        {
            navMeshAgent.SetDestination(randomPosition);
        }

        if(isPlayerNear && hasEaten)
        {
            randomPosition = FindRandomTargetPosition(transform.position, maxDistance);
            navMeshAgent.SetDestination(randomPosition);
            SwitchState(Run);
        }

        float distanceToPlayer = Vector3.Distance(transform.position, GameObject.FindWithTag("Player").transform.position);
        float triggerDistance = 15f;
        if(distanceToPlayer < triggerDistance)
        {
            isPlayerNear = true;
            
        }
        else
        {
            isPlayerNear = false;
        }
    }

    private IEnumerator EatAnimal()
    {
        hasEaten= false;
        SwitchState(Eat);
        yield return new WaitForSeconds(5);
        hasEaten = true;
    }

    private Vector3 FindRandomTargetPosition(Vector3 center, float maxDistance)  // finds random position on NavMesh by raycasting down a hit on Y-axis
    {
        Vector3 randomDirection = Random.insideUnitSphere * maxDistance;
        randomDirection += center;
        NavMeshHit hit;

        NavMesh.SamplePosition(randomDirection, out hit, maxDistance, NavMesh.AllAreas);

        return hit.position;
    }

    private void RotateTowards(Vector3 to)
    {

        Quaternion _lookRotation =
            Quaternion.LookRotation((to - transform.position).normalized);

        //over time
        transform.rotation =
            Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * speed);

        //instant
        transform.rotation = _lookRotation;
    }
}
