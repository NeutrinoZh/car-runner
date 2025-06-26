using System;
using UnityEngine;
using Zenject;

namespace Game
{
    public class PlayerAnimation : IInitializable, IDisposable
    {
        private static readonly int k_dieTriggerId = Animator.StringToHash("Die");

        private readonly PlayerModel _playerModel;
        private readonly Animator _animator;

        public PlayerAnimation(PlayerModel playerModel, Animator animator)
        {
            _playerModel = playerModel;
            _animator = animator;
        }

        public void Initialize()
        {
            _playerModel.OnChangeHealth += ChangeHealthHandle;
        }

        public void Dispose()
        {
            _playerModel.OnChangeHealth -= ChangeHealthHandle;
        }

        private void ChangeHealthHandle(float health)
        {
            if (_playerModel.Health <= 0)
                _animator.SetTrigger(k_dieTriggerId);
        }
    }
}