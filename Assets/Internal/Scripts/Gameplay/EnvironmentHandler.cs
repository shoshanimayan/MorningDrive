using General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public class EnvironmentHandler : MonoBehaviour
    {
        private GameStateManager _gameState;

        private void Awake()
        {
            _gameState = GetComponent<GameStateManager>();

        }
    }
}
