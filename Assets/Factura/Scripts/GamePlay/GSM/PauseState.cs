using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class PauseState: IGameState
    {
        public GameSM GameSM { get; set; }

        private readonly MenuScreens _menuScreens;
        
        public PauseState(MenuScreens menuScreens)
        {
            _menuScreens = menuScreens;
        }
        
        public void Enter()
        {
            _menuScreens.OnStart += StartHandle;
            _ = PauseRoutine();
        }

        public void Exit()
        {
            Time.timeScale = 1;
            _menuScreens.OnStart -= StartHandle;
            _menuScreens.HideAll();
        }

        private void StartHandle()
        {
            GameSM.Enter<PlayState>();
        }
        
        private async Task PauseRoutine()
        {
            await Task.Delay(100);
            Time.timeScale = 0;
        }
    }
}