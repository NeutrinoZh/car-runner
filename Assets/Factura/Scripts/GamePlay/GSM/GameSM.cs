
using System.Collections.Generic;

namespace Game
{
    public class GameSM
    {
        private readonly Dictionary<System.Type, IGameState> _states;
        private IGameState _activeState;

        public GameSM(Dictionary<System.Type, IGameState> states)
        {
            _states = states;
        }

        public void Enter<TState>() where TState : IGameState
        {
            _activeState?.Exit();
            _activeState = _states[typeof(TState)];
            _activeState.Enter();
        }
    }
}