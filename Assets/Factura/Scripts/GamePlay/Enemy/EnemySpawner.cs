using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Game
{
    public class EnemySpawner : IInitializable
    {
        public const int NumberOfEnemy = 60;
        
        private static readonly Vector3 k_offset = new(0f, 0f, -50);

        private const float k_initSpawnInterval = 1000;
        private const float k_miniumSpawnInterval = 50;
        private const float k_decreaseFrequencyInterval = 1000;
        private const float k_decreaseMultiplier = 0.95f;
        
        private float _spawnInterval;
        private int _spawnCount;
        private bool _running;
        
        private readonly EnemyPool _pool;
        private readonly PlayersList _playerList;
        private readonly GameSM _gameSM;
        
        public EnemySpawner(EnemyPool pool, PlayersList playerList, GameSM gameSM)
        {
            _pool = pool;
            _gameSM = gameSM;
            _playerList = playerList;
        }
        
        public void Initialize()
        {
            _running = true;
            _spawnInterval = k_initSpawnInterval;
            
            _ = RepeatSpawnEveryAsync();
            _ = RepeatDecreaseIntervalEveryAsync();
        }

        private async Task RepeatDecreaseIntervalEveryAsync()
        {
            await Task.Delay(100);

            while (_running)
            {
                if (!_gameSM.IsState<PlayState>())
                {
                    await Task.Delay(100);
                    continue;
                }

                if (_spawnInterval < k_miniumSpawnInterval)
                    break;
                
                _spawnInterval *= k_decreaseMultiplier;
                await Task.Delay((int)(k_decreaseFrequencyInterval * Time.timeScale));
            }
        }
        
        private async Task RepeatSpawnEveryAsync()
        {
            await Task.Delay(100);
            
            while (_running)
            {
                if (!_gameSM.IsState<PlayState>())
                {
                    await Task.Delay(100);
                    continue;
                }

                if (_spawnCount >= NumberOfEnemy)
                    break;
                
                Spawn();
                await Task.Delay((int)(_spawnInterval * Time.timeScale));
            }
        }

        private void Spawn()
        {
            if (!_playerList.MainPlayer)
                return;

            var position = _playerList.MainPlayer.transform.position + k_offset;
            _pool.Spawn(position);
            _spawnCount += 1;
        }
    }
}