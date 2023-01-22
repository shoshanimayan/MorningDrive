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

    public class GameStateManager : EventListener
    {

        
        //  PRIVATE VARIABLES         //
        
        private GameState _currentState;


        
        //  PRIVATE METHODS           //
        
        private void Awake()
        {

            _currentState = Resources.Load<GameState>("CurrentState");           

            EventConstants.ToMenu.RegisterListener(this);
            EventConstants.ToLoading.RegisterListener(this);
            EventConstants.ToPlay.RegisterListener(this);
            EventConstants.ToEnd.RegisterListener(this);

            Application.targetFrameRate = 90;
           
            EventConstants.ToLoading.Raise();
        }

        private void Start()
        {

            EventConstants.LoadSceneEvent.Raise();
        }

        private void GoToMenu()
        {
            State = GameStateType.Menu;
        }

        private void GoToEnd()
        {
            State = GameStateType.Ending;
        }

        private void GoToPlay()
        {
            State = GameStateType.Playing;
        }

        
        //  PUBLIC API               //
        


        public GameStateType State
        {
            get
            {
                return _currentState.State;
            }
             set
            {
                if (_currentState)
                {
                    if (_currentState.State == value) return;
                    _currentState.State = value;
                }
            }
        }


        public void ToMenu()
        {
            EventConstants.ToMenu.Raise();
        }

        public void ToPlay()
        {
            EventConstants.ToPlay.Raise();
        }
        
        public void GoToExit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        public override void OnEventRaised(string gameEventName)
        {
            switch (gameEventName)
            {
                case "ToMenu":
                    GoToMenu();
                    break;
                case "ToPlay":
                    GoToPlay();
                    break;
                case "ToLoading":
                    
                    break;
                case "ToEnd":
                    GoToEnd();
                    break;
            }
        }
    }
}
