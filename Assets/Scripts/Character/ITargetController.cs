namespace BattleArena.Character
{
    public interface ITargetController
    {
        bool TryGetClosestTargetFor(ICharacterController characterController, out ICharacterController target);
    }
}