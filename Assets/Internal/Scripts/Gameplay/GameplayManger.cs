using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using UnityEngine.UI;
using System.Threading.Tasks;
using Audio;

namespace GamePlay
{
    public class GameplayManger : EventListener
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

        private Camera _camera;

        private GameState _currentState;

        private bool Playing
        {
            get { return _playing; }
            set
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

        ///////////////////////////////
        //  PRIVATE METHODS           //
        ///////////////////////////////

        private void Awake()
        {
            
            _currentState = Resources.Load<GameState>("CurrentState");
            _camera = Camera.main;
            EventConstants.ToPlay.RegisterListener(this);

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
            EventConstants.StartTweensEvent.Raise();
        }

        private void EndGame()
        {
            Playing = false;
            EventConstants.ToEnd.Raise();
        }

        private void CameraShake()
        {
            EventConstants.SpeedBumpEvent.Raise();
            Shake(.5f,.2f);
        }

        private async void SetCameraShake()
        {

            float time = Random.Range(30f, _maxTime-10);
            time = (time )*1000;
            await Task.Delay((int)time);
            CameraShake();
        }

        private void StartGame()
        {
            _timer = 0;
            _maxTime = _currentState.PlayLength * 60;
            Playing = true;
            Task t = RunProgress();
        }

        ///////////////////////////////
        //  PUBLIC API               //
        ///////////////////////////////





        public override void OnEventRaised(string gameEventName)
        {
            switch (gameEventName)
            {
               
                case "ToPlay":
                    StartGame();
                    break;
               
            }
        }
    }
}