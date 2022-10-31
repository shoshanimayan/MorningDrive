using GamePlay;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI;
using UnityEngine;
using Window;
using Audio;
public enum GameStateType { Loading, Menu, Playing, Ending }

namespace General
{

    public class GameStateManager : Singleton<GameStateManager>
    {

        ///////////////////////////////
        //  PRIVATE VARIABLES         //
        ///////////////////////////////
     //   private GameStateType _state= GameStateType.Loading;
        private SceneLoader _sceneLoader;
        private GameplayManger _gameplayManger;
        private UIHandler _uIHandler;
        private GameState _currentState;

        private AudioManager _audioManager { get { return AudioManager.Instance; } }

        ///////////////////////////////
        //  PRIVATE METHODS           //
        ///////////////////////////////
        private void Awake()
        {
            _currentState = Resources.Load<GameState>("CurrentState");
            Application.targetFrameRate = 90;
            _sceneLoader = GetComponent<SceneLoader>();
            _gameplayManger = GetComponent<GameplayManger>();
            _uIHandler = GetComponent<UIHandler>();
            _uIHandler.ShowLoadingUI();
        }

        private void Start()
        {
            _sceneLoader.FirstLoad();
        }


        ///////////////////////////////
        //  PUBLIC API               //
        ///////////////////////////////

       // public float PlayLength=1;

        public GameStateType State
        {
            get
            {
                return _currentState.State;
            }
             set
            {
               
                if (_currentState.State == value) return;
                _currentState.State = value;

                switch (value)
                {
                    case GameStateType.Menu:
                        _uIHandler.ShowMenuUI();
                        _currentState.Window.SetClearAge(10);
                        _currentState.Window.SetRadius(0);
                        _audioManager.PlayMusicAudio();
                        break;

                    case GameStateType.Loading:
                        _uIHandler.ShowLoadingUI();
                        break;

                    case GameStateType.Ending:
                        _audioManager.StopCarAudio();
                        _currentState.Window.SetRadius(0);
                        _uIHandler.ShowEndGameUI();
                        _currentState.ScrollTextureHandler.MyState = value;
                        break;

                    case GameStateType.Playing:
                        _currentState.Window.SetRadius(10);
                        _uIHandler.StartGameUI();
                        _gameplayManger.StartGame();
                        _audioManager.StopMusicAudio();
                        _audioManager.PlayCarAudio();
                        _currentState.ScrollTextureHandler.MyState = value;
                        break;
                }
            }
        }


        

        public void ToMenu()
        {
            State = GameStateType.Menu;
        }

        public void ToEnd()
        {
            State = GameStateType.Ending;
        }

        public void ToPlay()
        {
            State = GameStateType.Playing;
        }

        public void ToExit()
        {
            Application.Quit();
        }

    }
}
