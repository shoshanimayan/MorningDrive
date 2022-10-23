using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState { Loading, Menu, Playing, Ending }

public class GameStateManager : MonoBehaviour
{
    private GameState _state = GameState.Loading;

    private SceneLoader _sceneLoader;
    private GameplayManger _gameplayManger;
    private UIHandler _uIHandler;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public GameState State
    {
        get
        {
            return _state;
        }
        private set
        {
            if (_state == value) return;
            _state = value;
        }
    }

    
}
