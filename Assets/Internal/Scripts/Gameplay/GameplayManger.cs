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
        ///////////////////////////////
        //  INSPECTOR VARIABLES      //
        ///////////////////////////////
        [SerializeField] Slider _progressSlider;

        ///////////////////////////////
        //  PRIVATE VARIABLES         //
        ///////////////////////////////
        private float _maxTime ;
        private float _timer = 0;
        private bool _playing;

        private EnvironmentHandler _environmentHandler;
        private Camera _camera;

        private AudioManager _audioManager { get { return AudioManager.Instance; } }
         private GameStateManager _gameState { get { return GameStateManager.Instance; } }
        private GameState _currentState;

        ///////////////////////////////
        //  PRIVATE METHODS           //
        ///////////////////////////////

        private void Awake()
        {

            _currentState = Resources.Load<GameState>("CurrentState");
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

            float time = Random.Range(30f, _maxTime-10);
            time = (time )*1000;
            await Task.Delay((int)time);
            CameraShake();
        }

       
        ///////////////////////////////
        //  PUBLIC API               //
        ///////////////////////////////
        public bool Playing
        {
            get { return _playing; }
            private set
            {
                if (_playing == value)
                    return;
                if (value)
                {
                    SetCameraShake();
                    StartTweensWithWait(5);
                }


            }

        }

       

        public void StartGame()
        {
            _timer = 0;
            _maxTime = _currentState.PlayLength * 60;
            Playing = true;
            Task t = RunProgress();
        }
    }
}