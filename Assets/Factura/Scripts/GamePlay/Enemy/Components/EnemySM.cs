using System.Collections.Generic;
using Zenject;

namespace Game
{
    public class EnemySM : ITickable
    {
        private readonly Dictionary<System.Type, IEnemyState> _states;
        private IEnemyState _activeState;

        public EnemySM(Dictionary<System.Type, IEnemyState> states)
        {
            _states = states;
        }

        public void Enter<TState>() where TState : IEnemyState
        {
            _activeState?.Exit();
            _activeState = _states[typeof(TState)];
            _activeState.SM = this;
            _activeState.Enter();
        }

        public void Tick()
        {
            _activeState?.Tick();
        }
    }
}