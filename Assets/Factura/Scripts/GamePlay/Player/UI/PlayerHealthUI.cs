using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game
{
    public class PlayerHealthUI : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        
        private PlayerModel _playerModel;
        private PlayerConfig _playerConfig;
        
        [Inject]
        public void Construct(PlayerModel playerModel, PlayerConfig playerConfig)
        {
            _playerModel = playerModel;
            _playerConfig = playerConfig;
        }
        
        private void Start()
        {
            _playerModel.OnChangeHealth += ChangeHealthHandle;
        }

        private void OnDestroy()
        {
            _playerModel.OnChangeHealth -= ChangeHealthHandle;
        }

        private void ChangeHealthHandle(float health)
        {
            if (health <= 0)
                gameObject.SetActive(false);
            
            _healthBar.fillAmount = health / _playerConfig.MaxHealth;
        }
    }
}