using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GroundSystem : MonoBehaviour
    {
        private SignalBus _signalBus;
        
        private List<Transform> _groundPlates;
        private Player _player;

        private float _plateLength;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
            signalBus.Subscribe<PlayerSpawnedSignal>(OnPlayerSpawned);
        }
        
        private void Awake()
        {
            _groundPlates = TransformUtils.GetAllChildTransforms(transform).ToList();

            if (_groundPlates.Count < 2)
            { 
                Debug.LogError($"[{nameof(GroundSystem)}] Not enough ground tiles: at least 2 are required, but only {_groundPlates.Count} were found. Please add more child tiles under {gameObject.name}.");
                enabled = false;
                return;
            }

            _plateLength = Mathf.Abs(_groundPlates[1].position.z - _groundPlates[0].position.z);
        }

        private void Update()
        {
            if (!_player)
                return;
            
            var playerZ = _player.transform.position.z;
            foreach (var plate in _groundPlates)
                if (plate.position.z - _plateLength > playerZ)
                {
                    var maxZ = _groundPlates.Min(p => p.position.z);
                    plate.position = new Vector3(
                        plate.position.x, plate.position.y, maxZ - _plateLength
                        );
                }
        }

        private void OnPlayerSpawned(PlayerSpawnedSignal signal)
        {
            _player = signal.Player;
        }

        private void OnDestroy()
        {
            _signalBus.TryUnsubscribe<PlayerSpawnedSignal>(OnPlayerSpawned);
        }
    }
}