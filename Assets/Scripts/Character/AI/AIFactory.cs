using System;

namespace BattleArena.Character
{
    public static class AIFactory
    {
        public static IAIController GetAIController(ICharacterController controller, ITargetController targetController)
        {
            return controller switch
            {
                CharacterController characterController => new AIController(characterController, targetController),
                _ => throw new ArgumentException($"Invalid controller {controller}")
            };
        }
    }
}