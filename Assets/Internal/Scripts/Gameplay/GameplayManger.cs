using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using UnityEngine.UI;
using System.Threading.Tasks;

namespace GamePlay
{
    public class GameplayManger : MonoBehaviour
    {

        [SerializeField] Slider _progressSlider;

        private float _maxTime = 60;
        private float _timer = 0;
        private GameStateManager _gameState;
        private EnvironmentHandler _environmentHandler;
        private bool _playing;


        private void Awake()
        {
            _gameState = GetComponent<GameStateManager>();
            _environmentHandler = GetComponent<EnvironmentHandler>();
        }

        private async Task RunProgress() 
        {
            while (_timer < _maxTime)
            {
                _timer += Time.deltaTime;
                _progressSlider.value = ((_maxTime - _timer) / _maxTime);
                await Task.Yield();

            }
            _timer = 0;
            EndGame();
        }

        private void EndGame()
        {
            _playing = false;
            _gameState.ToEnd();
        
        }

        public float MaxTime
        {
            get { return _maxTime; }
             set
            {
                if (value == _maxTime / 60)
                    return;
                _maxTime = value * 60;
            }

        }

       

        public void StartGame()
        {
            _timer = 0;
            _playing = true;
            Task t = RunProgress();
        }
    }
}