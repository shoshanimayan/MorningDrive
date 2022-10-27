using General;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace UI
{
    public class UIHandler : MonoBehaviour
    {
        [Header("Canvases")]
        [SerializeField] Canvas _loadingUI;
        [SerializeField] Canvas _menuUI;
        [SerializeField] Canvas _endUI;
        [SerializeField] Canvas _gameUI;
        [SerializeField] Canvas _hintUI;

        [Header("Main Menu Elements")]
        [SerializeField] TextMeshProUGUI _LengthText;


        private GameStateManager _gameState;
        private float _gameLengthTextValue=1;
        private bool _showTip = false;

        private void Awake()
        {
            _gameState = GetComponent<GameStateManager>();
            _menuUI.enabled = false;
            _endUI.enabled = false;
            _gameUI.enabled = false;
            _hintUI.enabled = false;

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
        public void HideHintText()
        {
            Task t = FadeInOut(_hintUI.GetComponent<CanvasGroup>(), true);
        }
        public void ShowMenuUI() 
        {

            HideAllUI();
            _menuUI.enabled = true;

            Task t = FadeInOut(_menuUI.GetComponent<CanvasGroup>(), false);

        }

        public void ShowEndGameUI()
        {
            HideAllUI();
            _showTip = false;
            HideHintText();

            _endUI.enabled = true;
            Task t = FadeInOut(_endUI.GetComponent<CanvasGroup>(), false);

        }

        public  void ShowHintText() {
            _hintUI.enabled = true;
             Task t = FadeInOut(_hintUI.GetComponent<CanvasGroup>(), false);
            _showTip = true;

        }

        public void StartGameUI() {
            HideAllUI();

            _gameUI.enabled = true;
            Task t = FadeInOut(_gameUI.GetComponent<CanvasGroup>(), false);
            ShowTipWithWait(5);
        }


        public void SetLengthText(Slider Length) 
        {
            if (Length.value > 1) {
                _LengthText.text = "Length: "+Length.value.ToString()+" minutes";
                    }
            else
            {
                _LengthText.text = "Length: 1 minute";

            }
            _gameLengthTextValue = Length.value;
        }

        public float GetLengthValueSet()
        {
            return _gameLengthTextValue;
        }


    }
}