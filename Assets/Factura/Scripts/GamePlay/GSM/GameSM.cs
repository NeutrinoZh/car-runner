
using System.Collections.Generic;

namespace Game
{
    public class GameSM
    {
        private Dictionary<System.Type, IGameState> _states;
        private IGameState _activeState;
        
        public void SetStates(Dictionary<System.Type, IGameState> states)
        {
            _states = states;
        }

        public bool IsState<TState>() where TState : IGameState
        {
            return _activeState is TState;
        }

        public void Enter<TState>() where TState : IGameState
        {
            _activeState?.Exit();
            _activeState = _states[typeof(TState)];
            _activeState.GameSM = this;
            _activeState.Enter();
        }
    }
}