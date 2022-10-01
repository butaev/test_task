using System.Collections.Generic;
using BattleArena.Pool;
using UnityEngine;

namespace BattleArena.Character
{
    public class CharacterLifecycleController: ITargetController
    {
        private readonly ObjectPool _objectPool;
        private readonly ICharacterModel _characterModel;
        private readonly List<ICharacterController> _controllers = new();
        private readonly float _searchDistance;

        public CharacterLifecycleController(ObjectPool objectPool, ICharacterModel characterModel,
            Vector2 arenaSize)
        {
            _objectPool = objectPool;
            _characterModel = characterModel;
            _searchDistance = Mathf.Max(arenaSize.x, arenaSize.y);
        }

        public void Spawn(Vector3 position)
        {
            var controller = _objectPool.GetObject<CharacterController>();
            controller.transform.position = position;
            controller.OnDeadEvent += Despawn;
            controller.Initialize(GetRandomCharacterParams(), AIFactory.GetAIController(controller, this));
            _controllers.Add(controller);
        }

        private void Despawn(CharacterController controller)
        {
            _controllers.Remove(controller);
            controller.Dispose();
            _objectPool.ReturnToPool(controller);
        }

        private CharacterStats GetRandomCharacterParams()
        {
            return new CharacterStats(Random.Range(_characterModel.MinMaxHp.x, _characterModel.MinMaxHp.y),
                Random.Range(_characterModel.MinMaxDamage.x, _characterModel.MinMaxDamage.y),
                Random.Range(_characterModel.MinMaxAttackDistance.x, _characterModel.MinMaxAttackDistance.y),
                Random.Range(_characterModel.MinMaxAttackCooldown.x, _characterModel.MinMaxAttackCooldown.y));
        }

        public bool TryGetClosestTargetFor(ICharacterController characterController, out ICharacterController targetController)
        {
            var minDistance = float.MaxValue;
            targetController = null;
            foreach (var controller in _controllers)
            {
                if (controller == characterController)
                {
                    continue;
                }
            
                var sqrDistance = (controller.Transform.position - characterController.Transform.position).sqrMagnitude;
                if (!(sqrDistance < minDistance)) continue;
                minDistance = sqrDistance;
                targetController = controller;
            }
            return targetController != null;
        }
    }
}