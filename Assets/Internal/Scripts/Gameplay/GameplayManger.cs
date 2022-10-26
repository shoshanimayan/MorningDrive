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
        [SerializeField] AudioSource _carAudio;
        [SerializeField] AudioSource _speedBumbAudio;


        private float _maxTime = 60;
        private float _timer = 0;
        private bool _playing;

        private GameStateManager _gameState;
        private EnvironmentHandler _environmentHandler;
        private Camera _camera;



        public bool Playing {
            get { return _playing; }
            private set {
                if (_playing == value)
                    return;
                if (value)
                {
                    SetCameraShake();
                    StartTweensWithWait(5);
                    _carAudio.Play();
                }
                else
                {
                    _environmentHandler.KillTweens();
                    _carAudio.Stop();
                }
            
            }
        
        }



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


        private async void Shake(float duration, float magnitude)
        {
            Vector3 orignalPosition = _camera.transform.position;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;

                transform.position = new Vector3(x, y, -10f);
                elapsed += Time.deltaTime;
                await Task.Yield();
            }
            transform.position = orignalPosition;
        }

    

        private async void StartTweensWithWait(int seconds)
        {
            await Task.Delay(seconds * 1000);
             Task t =_environmentHandler.StartTweens();
        }

        private void EndGame()
        {
            Playing = false;
            _gameState.ToEnd();
        
        }

        private void CameraShake()
        {
            _speedBumbAudio.Play();
            Shake(1,.5f);
        }

        private async void SetCameraShake()
        {
            float time = Random.Range(30f, MaxTime-10);
            time = (time / 60)*1000;

            await Task.Delay((int)time);
            CameraShake();
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
            Playing = true;
            Task t = RunProgress();
        }
    }
}