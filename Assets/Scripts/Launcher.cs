using BattleArena.Arena;
using BattleArena.Character;
using BattleArena.Pool;
using UnityEngine;

namespace BattleArena
{
    public class Launcher : MonoBehaviour
    {
        [SerializeField] private BattleConfig _battleConfig;
        [SerializeField] private ObjectPool _objectPool;
        [SerializeField] private AreaController _areaController;

        private CharacterLifecycleController _lifecycleController;

        private void Awake()
        {
            _objectPool.Initialize();
            _areaController.Initialize(_battleConfig.ArenaSize);
            _lifecycleController = new CharacterLifecycleController(_objectPool, _battleConfig, _battleConfig.ArenaSize);
        }

        private void Start()
        {
            for (var i = 0; i < _battleConfig.NumberOfCharacters; i++)
            {
                _lifecycleController.Spawn(_areaController.GetRandomPositionInArea());
            }
        }
    }
}