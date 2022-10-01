using UnityEngine;
using UnityEngine.AI;

namespace BattleArena.Character
{
    public interface ICharacterController
    {
        int Hp { get; }
        int Damage { get; }
        float AttackDistance { get; }
        float AttackCooldown { get; }
        NavMeshAgent NavMeshAgent { get; }
        bool HasTarget { get; }
        ICharacterController Target { get; }
        Transform Transform { get; }
        int Layer { get; }

        void SetTarget(ICharacterController target);
        void Inflict(int damage);
    }
}