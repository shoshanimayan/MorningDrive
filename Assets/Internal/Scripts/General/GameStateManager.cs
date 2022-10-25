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


        private bool _showTip = false;
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

        private async void ShowTipWithWait(int seconds)
        {
            await Task.Delay(seconds*1000);
            _uIHandler.ShowHintText();
            _showTip = true;
        }

        // Update is called once per frame
       


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
                        _window.SetRadius(0);
                        break;
                    case GameState.Loading:
                        break;
                    case GameState.Ending:
                        _uIHandler.ShowEndGameUI();
                        break;
                    case GameState.Playing:
                        _window.SetRadius(10);
                        _window.SetClearAge(10);
                        _uIHandler.StartGameUI();
                        ShowTipWithWait(3);
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
