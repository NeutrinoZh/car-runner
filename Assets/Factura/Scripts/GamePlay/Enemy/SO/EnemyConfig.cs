using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Game/Enemy/Config")]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public float AngrySpeed { get; private set; }
        [field: SerializeField] public float IdleSpeed { get; private set; }
        [field: SerializeField] public float MaxHealth { get; private set; }
        [field: SerializeField] public float AttackRate { get; private set; }
    }
}