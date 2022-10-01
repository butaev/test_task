using UnityEngine;

namespace BattleArena.Character
{
    public class AIController : IAIController
    {
        private readonly ICharacterController _character;
        private readonly ITargetController _targetController;
        private readonly MovementController _movementController;

        private float _cooldown;

        private bool IsCooldown => _cooldown < _character.AttackCooldown;
        public ActionState State { get; private set; }

        public AIController(ICharacterController character, ITargetController targetController)
        {
            _character = character;
            _targetController = targetController;
            State = ActionState.None;
            _movementController = new MovementController(character.NavMeshAgent);
            _cooldown = character.AttackCooldown;
        }

        public void Dispose()
        {
            _character.SetTarget(null);
            State = ActionState.None;
            _movementController.Dispose();
        }

        public void Start()
        {
            State = ActionState.Moving;
        }

        public void Update()
        {
            switch (State)
            {
                case ActionState.None:
                    return;
                case ActionState.Attack:
                    _cooldown -= Time.deltaTime;
                    break;
                case ActionState.Moving or ActionState.Pending when IsCooldown:
                    _cooldown -= Time.deltaTime;
                    break;
            }

            if (_cooldown <= 0)
            {
                _cooldown = _character.AttackCooldown;
            }

            if (_character.HasTarget)
            {
                if (!_targetController.TryGetClosestTargetFor(_character, out var characterTarget))
                {
                    State = ActionState.Pending;
                    return;
                }

                _character.SetTarget(characterTarget);
                if (characterTarget.Target == _character || !characterTarget.HasTarget)
                {
                    if (!TryAttack(characterTarget))
                    {
                        MoveTo(characterTarget.Transform);
                    }
                }
                else
                {
                    MoveTo(characterTarget.Transform);
                }
            }
            else
            {
                if (_targetController.TryGetClosestTargetFor(_character, out var characterTarget))
                {
                    _character.SetTarget(characterTarget);
                }
                else
                {
                    State = ActionState.Pending;
                }
            }
        }

        private bool TryAttack(ICharacterController characterTarget)
        {
            var distance = Vector3.Distance(characterTarget.Transform.position, _character.Transform.position);
            if (distance > _character.AttackDistance)
            {
                return false;
            }
            if (IsCooldown)
            {
                State = ActionState.Pending;
                return false;
            }

            State = ActionState.Attack;
            characterTarget.Inflict(_character.Damage);
            return true;
        }

        private void MoveTo(Transform transform)
        {
            State = ActionState.Moving;
            _movementController.MoveTo(transform);
        }
    }
}