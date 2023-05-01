using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class ArtificialIntelligent : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private Transform target;
    [SerializeField] private NavMeshSurface surface;
    
    void Update()
    {
        agent.SetDestination(target.position);
        surface.BuildNavMesh();
    }
}