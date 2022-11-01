using General;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace UI
{
    public class UIHandler : EventListener
    {

        ///////////////////////////////
        //  INSPECTOR VARIABLES      //
        ///////////////////////////////
        [Header("Canvases")]
        [SerializeField] private Canvas _loadingUI;
        [SerializeField] private Canvas _menuUI;
        [SerializeField] private Canvas _endUI;
        [SerializeField] private Canvas _gameUI;
        [SerializeField] private Canvas _hintUI;

        [Header("Loading Sphere")]
        [SerializeField] private GameObject _loadingSphere;

        [Header("Main Menu Elements")]
        [SerializeField] TextMeshProUGUI _LengthText;

        ///////////////////////////////
        //  PRIVATE VARIABLES         //
        ///////////////////////////////
        private bool _showTip = false;
        private GameState _currentState;

        private void Awake()
        {
            _currentState = Resources.Load<GameState>("CurrentState");
            EventConstants.ToMenu.RegisterListener(this);
            EventConstants.ToLoading.RegisterListener(this);
            EventConstants.ToPlay.RegisterListener(this);
            EventConstants.ToEnd.RegisterListener(this);
            _menuUI.enabled = false;
            _endUI.enabled = false;
            _gameUI.enabled = false;
            _hintUI.enabled = false;
            _loadingUI.enabled = false;

        }

      
        private void Update()
        {
            
            if (_showTip && Input.GetMouseButtonUp(0))
            {
                _showTip = false;
                HideHintText();
            }
        }

        private async Task FadeInOut(CanvasGroup canvas, bool fade)
        {
            canvas.interactable = false;
            if (fade)
            {
                canvas.alpha = 1;
                while (canvas.alpha > 0)
                {
                    canvas.alpha -= Time.deltaTime;
                    
                    await Task.Yield();

                }
                canvas.gameObject.GetComponent<Canvas>().enabled = false;

            }
            else
            {
                canvas.alpha = 0;
                while (canvas.alpha < 1)
                {
                    canvas.alpha += Time.deltaTime;
                   
                    await Task.Yield();

                }
                canvas.interactable = true;
            }
        }

        private async void ShowTipWithWait(int seconds)
        {
            await Task.Delay(seconds * 1000);
            ShowHintText();
        }

        private void HideAllUI() {
            if (_loadingUI.enabled)
            {
                _loadingUI.enabled = false;
                _loadingSphere.SetActive(false);
            }

            if (_menuUI.enabled)
            {
                Task t = FadeInOut(_menuUI.GetComponent<CanvasGroup>(), true);
              
            }
            if (_endUI.enabled)
            {
                Task t = FadeInOut(_endUI.GetComponent<CanvasGroup>(), true);

               
            }
            if (_gameUI.enabled)
            {
                Task t = FadeInOut(_gameUI.GetComponent<CanvasGroup>(), true);

                
            }
           

        }

        

        private void HideHintText()
        {
            Task t = FadeInOut(_hintUI.GetComponent<CanvasGroup>(), true);
        }
        private void ShowMenuUI() 
        {
           
            HideAllUI();
            _menuUI.enabled = true;

            Task t = FadeInOut(_menuUI.GetComponent<CanvasGroup>(), false);

        }

        private void ShowEndGameUI()
        {
            HideAllUI();
            _showTip = false;
            HideHintText();

            _endUI.enabled = true;
            Task t = FadeInOut(_endUI.GetComponent<CanvasGroup>(), false);

        }

        private void ShowLoadingUI()
        {
            HideAllUI();           
            _loadingUI.enabled = true;
            _loadingSphere.SetActive(true);
        }

        private void ShowHintText() {
            _hintUI.enabled = true;
             Task t = FadeInOut(_hintUI.GetComponent<CanvasGroup>(), false);
            _showTip = true;

        }

        private void StartGameUI() {
            HideAllUI();
            _gameUI.enabled = true;
            Task t = FadeInOut(_gameUI.GetComponent<CanvasGroup>(), false);
            ShowTipWithWait(5);
        }




        ///////////////////////////////
        //  PUBLIC API               //
        ///////////////////////////////

        public void SetLengthText(Slider Length)
        {
            if (Length.value > 1)
            {
                _LengthText.text = "Length: " + Length.value.ToString() + " minutes";
            }
            else
            {
                _LengthText.text = "Length: 1 minute";

            }
            _currentState.PlayLength = Length.value;
        }

        public override void OnEventRaised(string gameEventName)
        {
            switch (gameEventName)
            {
                case "ToMenu":
                    ShowMenuUI();
                    break;
                case "ToPlay":
                    StartGameUI();
                    break;
                case "ToLoading":
                    ShowLoadingUI();
                    break;
                case "ToEnd":
                    ShowEndGameUI();
                    break;
            }
        }
    }
}