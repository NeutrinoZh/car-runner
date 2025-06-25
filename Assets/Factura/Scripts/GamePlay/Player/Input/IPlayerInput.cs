using UnityEngine;

namespace Game.Input
{
    public interface IPlayerInput
    {
        Vector2 Direction { get; }
        bool IsShooting { get; }
    }
}