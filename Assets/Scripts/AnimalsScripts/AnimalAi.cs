using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.Image;

public class AnimalAi : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    private float speed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    [SerializeField] private float maxDistance = 0f;

    private Vector3 randomPosition;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        randomPosition = FindRandomTargetPosition(transform.position, maxDistance);
    }

    void Update()
    {
        float distanceToTargetPosition = Vector3.Distance(transform.position, randomPosition);
        float minDistance = 2f;
        if(distanceToTargetPosition < minDistance)
        {
            randomPosition = FindRandomTargetPosition(transform.position, maxDistance);
        }
        else
        {
            navMeshAgent.SetDestination(randomPosition);
        }
        //RotateTowards(randomPosition);
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
