using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

namespace GamePlay
{
    public class GameplayManger : MonoBehaviour
    {

        private float _maxTime = 60;
        private GameStateManager _gameState;
        private EnvironmentHandler _environmentHandler;

        private void Awake()
        {
            _gameState = GetComponent<GameStateManager>();
            _environmentHandler = GetComponent<EnvironmentHandler>();
        }

        public float MaxTime
        {
            get { return _maxTime; }
            private set
            {
                if (value == _maxTime / 60)
                    return;
                _maxTime = value * 60;
            }

        }
    }
}