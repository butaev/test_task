using System;
using BattleArena.Pool;
using UnityEngine;
using UnityEngine.AI;

namespace BattleArena.Character
{
    public class CharacterController : PoolObject, ICharacterController
    {
        [SerializeField] private NavMeshAgent _agent;

        private IAIController _aiController;
        private int _layer;

        public int Hp { get; private set; }

        public int Damage { get; private set; }

        public float AttackDistance { get; private set; }

        public float AttackCooldown { get; private set; }

        public NavMeshAgent NavMeshAgent => _agent;
        public bool HasTarget => Target != null;
        public ICharacterController Target { get; private set; }
        public Transform Transform => transform;
        public int Layer => gameObject.layer;

        public event Action<CharacterController> OnDeadEvent;

        public void Initialize(CharacterStats stats, IAIController aiController)
        {
            Hp = stats.Hp;
            Damage = stats.Damage;
            AttackDistance = stats.AttackDistance;
            AttackCooldown = stats.Cooldown;
            _aiController = aiController;
            _aiController.Start();
        }

        public void Dispose()
        {
            _aiController.Dispose();
        }

        public void SetTarget(ICharacterController target)
        {
            Target = target;
        }

        public void Inflict(int damage)
        {
            Hp = Mathf.Clamp(Hp - damage, 0, Hp);
            if (Hp <= 0)
            {
                OnDeadEvent?.Invoke(this);
            }
        }

        private void Update()
        {
            _aiController.Update();
        }
    }
}