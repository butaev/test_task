using BattleArena.Character;
using UnityEngine;

namespace BattleArena
{
    [CreateAssetMenu(menuName = "Configs/BattleConfig")]
    public class BattleConfig : ScriptableObject, ICharacterModel
    {
        [SerializeField] private Vector2 _arenaSize = new Vector2(50, 50);
        [SerializeField] private uint _numberOfCharacters = 10;
        [SerializeField] private Vector2Int _minMaxHp = new Vector2Int(10, 20);
        [SerializeField] private Vector2Int _minMaxDamage = new Vector2Int(1, 5);
        [SerializeField] private Vector2 _minMaxAttackDistance = new Vector2(1, 5);
        [SerializeField] private Vector2 _minMaxAttackCooldown = new Vector2(0.5f, 2f);

        public Vector2 ArenaSize => _arenaSize;
        public uint NumberOfCharacters => _numberOfCharacters;
        public Vector2Int MinMaxHp => _minMaxHp;
        public Vector2Int MinMaxDamage => _minMaxDamage;
        public Vector2 MinMaxAttackDistance => _minMaxAttackDistance;
        public Vector2 MinMaxAttackCooldown => _minMaxAttackCooldown;
    }
}