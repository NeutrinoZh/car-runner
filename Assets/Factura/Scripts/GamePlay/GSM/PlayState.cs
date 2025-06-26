using UnityEngine;

namespace Game
{
    public class PlayState : IGameState
    {
        public GameSM GameSM { get; set; }
        
        private readonly FollowingCamera _followingCamera;
        private readonly PlayersList _playersList;
        private readonly EnemyPool _enemyPool;
        private readonly MenuScreens _menuScreens;

        private int _countOfDeathEnemy;

        public PlayState(
            PlayersList playersList, 
            EnemyPool enemyPool, 
            FollowingCamera camera,
            MenuScreens menuScreens)
        {
            _playersList = playersList;
            _menuScreens = menuScreens;
            _followingCamera = camera;
            _enemyPool = enemyPool;
        }
        
        public void Enter()
        {
            Time.timeScale = 1;
            
            _followingCamera.Target = _playersList.MainPlayer.transform;
            _followingCamera.Follow = true;

            _enemyPool.OnDie += DieEnemyHandle;
            _playersList.MainPlayer.PlayerModel.OnDeath += DiePlayerHandle;
        }

        public void Exit()
        {
            _playersList.MainPlayer.PlayerModel.OnDeath -= DiePlayerHandle;
            _enemyPool.OnDie -= DieEnemyHandle;
        }

        private void DieEnemyHandle()
        {
            _countOfDeathEnemy += 1;
            if (_countOfDeathEnemy >= EnemySpawner.NumberOfEnemy)
            {
                _menuScreens.ShowWin();
                GameSM.Enter<PauseState>();
            }
        }

        private void DiePlayerHandle()
        {
            _menuScreens.ShowLose();
            GameSM.Enter<PauseState>();
        }
    }
}