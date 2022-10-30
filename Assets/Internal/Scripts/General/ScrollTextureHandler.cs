using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace General { 
    public class ScrollTextureHandler : Singleton<ScrollTextureHandler>
    {
        [SerializeField] private float _slowDown = .05f;
        [SerializeField] private float _offsetY;
        [SerializeField] private float _topSpeed = 8;

        private GameStateManager _gameState;

        private Renderer _render;
        private  bool _active;
        private  float _currentSpeed=0;

        public  GameState MyState
        {
            set
            {
                switch (value)
                {
                    case GameState.Playing:
                        print(value);
                        _active = true;
                        _currentSpeed = 0;
                        SpeedUp();
                        break;
                    case GameState.Ending:
                        _active = false;
                        break;
                }
            }
        }


        private  async void SpeedUp()
        {
            while (_currentSpeed < _topSpeed)
            {
                await  Task.Yield();
                _currentSpeed += Time.deltaTime;
            }
            _currentSpeed = 50; ;
        }

       


        private void Awake()
        {
            _gameState = FindObjectOfType<GameStateManager>();
            _render = GetComponent<Renderer>();
            _topSpeed = 8;
        }

        
        void Update()
        {
            if (_active)
            {
                _offsetY = Time.timeSinceLevelLoad * (_currentSpeed * _slowDown);
                _render.material.mainTextureOffset = new Vector2(0, _offsetY);
            }
 
        }
    }
}
