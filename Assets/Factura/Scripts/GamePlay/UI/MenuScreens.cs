using System;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game
{
    public class MenuScreens : MonoBehaviour
    {
        private const float k_eclipseDuration = 1f;

        public event Action OnStart;
        public event Action OnRestart;
        
        [SerializeField] private Image _background;
        [SerializeField] private Transform _playGroup;
        [SerializeField] private Transform _loseGroup;
        [SerializeField] private Transform _winGroup;

        private Button _playButton;
        private Button _restartLoseButton;
        private Button _restartWinButton;

        private float _endValueOfEclipse;
        
        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _endValueOfEclipse = _background.color.a;
        }
        
        public void HideAll()
        {
            Tween.Alpha(_background, 0f, k_eclipseDuration);
            _playGroup.gameObject.SetActive(false);
            _loseGroup.gameObject.SetActive(false);
            _winGroup.gameObject.SetActive(false);
        }

        public void ShowLose()
        {
            Tween.Alpha(_background, _endValueOfEclipse, k_eclipseDuration);   
            _playGroup.gameObject.SetActive(false);
            _loseGroup.gameObject.SetActive(true);
            _winGroup.gameObject.SetActive(false);
        }
        
        public void ShowWin()
        {
            Tween.Alpha(_background, _endValueOfEclipse, k_eclipseDuration);   
            _playGroup.gameObject.SetActive(false);
            _loseGroup.gameObject.SetActive(false);
            _winGroup.gameObject.SetActive(true);
        }

        private void Awake()
        {
            _playButton = _playGroup.GetComponentInChildren<Button>();
            _restartLoseButton = _loseGroup.GetComponentInChildren<Button>();
            _restartWinButton = _winGroup.GetComponentInChildren<Button>();
        }

        private void Start()
        {
            _playButton.onClick.AddListener(HandleStart);
            _restartLoseButton.onClick.AddListener(HandleRestart);
            _restartWinButton.onClick.AddListener(HandleRestart);
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(HandleStart);
            _restartLoseButton.onClick.RemoveListener(HandleRestart);
            _restartWinButton.onClick.RemoveListener(HandleRestart);
        }
        
        private void HandleStart()
        {
            OnStart?.Invoke();
        }

        private void HandleRestart()
        {
            OnRestart?.Invoke();
        }
    }
}