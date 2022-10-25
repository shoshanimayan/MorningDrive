using GamePlay;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI;
using UnityEngine;
using Window;

public enum GameState { Loading, Menu, Playing, Ending }

namespace General
{

    public class GameStateManager : MonoBehaviour
    {
        private GameState _state = GameState.Loading;

        private SceneLoader _sceneLoader;
        private GameplayManger _gameplayManger;
        private UIHandler _uIHandler;


        private DrawOnTexture _window;


        private void Awake()
        {
            _sceneLoader = GetComponent<SceneLoader>();
            _gameplayManger = GetComponent<GameplayManger>();
            _uIHandler = GetComponent<UIHandler>();
        }

        // Start is called before the first frame update
        void Start()
        {
            _sceneLoader.FirstLoad();
        }



        public GameState State
        {
            get
            {
                return _state;
            }
             set
            {
                if (_state == value) return;
                _state = value;

                switch (_state)
                {
                    case GameState.Menu:
                        _uIHandler.ShowMenuUI();
                        _window.SetClearAge(10);
                        _window.SetRadius(0);
                        break;
                    case GameState.Loading:
                        break;
                    case GameState.Ending:
                        _window.SetRadius(0);
                        _uIHandler.ShowEndGameUI();
                        break;
                    case GameState.Playing:
                        _window.SetRadius(10);
                        _uIHandler.StartGameUI();
                        _gameplayManger.MaxTime = _uIHandler.GetLengthValueSet();
                        _gameplayManger.StartGame();
                        break;
                }
            }
        }


        public void SetWindow(DrawOnTexture window)
        {
            _window = window;
        }

        public void ToMenu()
        {
            State = GameState.Menu;
        }

        public void ToEnd()
        {
            State = GameState.Ending;
        }

        public void ToPlay()
        {
            State = GameState.Playing;
        }

        public void ToExit()
        {
            Application.Quit();
        }

    }
}
