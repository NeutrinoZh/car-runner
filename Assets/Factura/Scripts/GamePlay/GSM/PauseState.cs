using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            _menuScreens.OnRestart += RestartHandle;
            _ = PauseRoutine();
        }

        public void Exit()
        {
            Time.timeScale = 1;
            _menuScreens.OnStart -= StartHandle;
            _menuScreens.OnRestart -= RestartHandle;
            _menuScreens.HideAll();
        }

        private void StartHandle()
        {
            GameSM.Enter<PlayState>();
        }

        private void RestartHandle()
        {
            SceneManager.LoadScene(0);
        }
        
        private async Task PauseRoutine()
        {
            await Task.Delay(100);
            Time.timeScale = 0;
        }
    }
}