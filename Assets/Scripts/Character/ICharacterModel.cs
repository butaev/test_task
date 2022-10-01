using UnityEngine;

namespace BattleArena.Character
{
    public interface ICharacterModel
    {
        Vector2Int MinMaxHp { get; }
        Vector2Int MinMaxDamage { get; }
        Vector2 MinMaxAttackDistance { get; }
        Vector2 MinMaxAttackCooldown { get; }
    }
}