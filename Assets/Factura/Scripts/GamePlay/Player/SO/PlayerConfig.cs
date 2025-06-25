using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Game/Player/Config")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float BaseSpeed { get; private set; }
        [field: SerializeField] public float MaxHealth { get; private set; }
        [field: SerializeField] public float FireRate { get; private set; }
    }
}