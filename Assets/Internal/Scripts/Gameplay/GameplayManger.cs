using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using UnityEngine.UI;
using System.Threading.Tasks;
using Audio;

namespace GamePlay
{
    public class GameplayManger : MonoBehaviour
    {

        [SerializeField] Slider _progressSlider;
       


        private float _maxTime = 60;
        private float _timer = 0;
        private bool _playing;

        private EnvironmentHandler _environmentHandler;
        private Camera _camera;

        private AudioManager _audioManager { get { return AudioManager.Instance; } }
        private GameStateManager _gameState { get { return GameStateManager.Instance; } }



        public bool Playing {
            get { return _playing; }
            private set {
                if (_playing == value)
                    return;
                if (value)
                {
                    SetCameraShake();
                    StartTweensWithWait(5);
                }
               
            
            }
        
        }



        private void Awake()
        {
            _environmentHandler = GetComponent<EnvironmentHandler>();
            _camera = Camera.main;
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
                _camera.transform.position = new Vector3(orignalPosition.x+ x, orignalPosition.y+ y, orignalPosition.z);
                elapsed += Time.deltaTime;
                await Task.Yield();
            }
            _camera.transform.position = orignalPosition;
        }

    

        private async void StartTweensWithWait(int seconds)
        {
            await Task.Delay(seconds * 1000);
             Task t =_environmentHandler.StartTweens();
        }

        private void EndGame()
        {
            _environmentHandler.KillTweens();
            Playing = false;
            _gameState.ToEnd();
        }

        private void CameraShake()
        {
            _audioManager.PlaySpeedBumpAudio();
            Shake(.5f,.2f);
        }

        private async void SetCameraShake()
        {
            float time = Random.Range(30f, MaxTime-10);
            time = (time )*1000;
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