using System;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class MenuScreens : MonoBehaviour
    {
        private const float k_eclipseDuration = 1f;

        public Action OnStart;
        
        [SerializeField] private Image _background;
        [SerializeField] private Transform _playGroup;
        [SerializeField] private Transform _loseGroup;
        [SerializeField] private Transform _winGroup;

        private Button _playButton;
        private Button _restartLoseButton;
        private Button _restartWinButton;
        
        public void HideAll()
        {
            Tween.Alpha(_background, 0f, k_eclipseDuration);
            _playGroup.gameObject.SetActive(false);
            _loseGroup.gameObject.SetActive(false);
            _winGroup.gameObject.SetActive(false);
        }

        public void ShowLose()
        {
            Tween.Alpha(_background, 1f, k_eclipseDuration);   
            _playGroup.gameObject.SetActive(false);
            _loseGroup.gameObject.SetActive(true);
            _winGroup.gameObject.SetActive(false);
        }
        
        public void ShowWin()
        {
            Tween.Alpha(_background, 1f, k_eclipseDuration);   
            _playGroup.gameObject.SetActive(false);
            _loseGroup.gameObject.SetActive(false);
            _winGroup.gameObject.SetActive(true);
        }

        private void Awake()
        {
            _playButton = _playGroup.GetComponentInChildren<Button>();
            _restartLoseButton = _playGroup.GetComponentInChildren<Button>();
            _restartWinButton = _playGroup.GetComponentInChildren<Button>();
        }

        private void Start()
        {
            _playButton.onClick.AddListener(HandleStart);
            _restartLoseButton.onClick.AddListener(HandleStart);
            _restartWinButton.onClick.AddListener(HandleStart);
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(HandleStart);
            _restartLoseButton.onClick.RemoveListener(HandleStart);
            _restartWinButton.onClick.RemoveListener(HandleStart);
        }

        private void HandleStart()
        {
            OnStart?.Invoke();
        }
    }
}