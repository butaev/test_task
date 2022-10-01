namespace BattleArena.Character
{
    public struct CharacterStats
    {
        public readonly int Hp;
        public readonly int Damage;
        public readonly float AttackDistance;
        public readonly float Cooldown;

        public CharacterStats(int hp, int damage, float attackDistance, float cooldown)
        {
            Hp = hp;
            Damage = damage;
            AttackDistance = attackDistance;
            Cooldown = cooldown;
        }
    }
}