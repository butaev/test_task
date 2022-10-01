using System;
using UnityEngine;
using UnityEngine.AI;

namespace BattleArena.Character
{
    public class MovementController : IDisposable
    {
        private readonly NavMeshAgent _navMeshAgent;

        public MovementController(NavMeshAgent navMeshAgent)
        {
            _navMeshAgent = navMeshAgent;
        }

        public void MoveTo(Transform targetTransform)
        {
            _navMeshAgent.destination = targetTransform.position;
        }

        public void Dispose()
        {
            _navMeshAgent.isStopped = true;
        }
    }
}