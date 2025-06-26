using UnityEngine;

namespace Game
{
    public class PlayState : IGameState
    {
        public GameSM GameSM { get; set; }
        
        private readonly FollowingCamera _followingCamera;
        private readonly PlayersList _playersList;

        public PlayState(PlayersList playersList, FollowingCamera camera)
        {
            _playersList = playersList;
            _followingCamera = camera;
        }
        
        public void Enter()
        {
            Time.timeScale = 1;
            
            _followingCamera.Target = _playersList.MainPlayer.transform;
            _followingCamera.Follow = true;
        }

        public void Exit()
        {
            
        }
    }
}