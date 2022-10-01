using System;

namespace BattleArena.Character
{
    public interface IAIController : IDisposable
    {
        ActionState State { get; }
        void Update();
        void Start();
    }
}