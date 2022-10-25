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

        private void Awake()
        {
            _gameState = GetComponent<GameStateManager>();
            _menuUI.enabled = false;
            _endUI.enabled = false;
            _gameUI.enabled = false;
            _hintUI.enabled = false;

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


            }
            else
            {
                canvas.alpha = 0;
                while (canvas.alpha < 1)
                {
                    canvas.alpha += Time.deltaTime*.5f;
                   
                    await Task.Yield();

                }
                canvas.interactable = true;
            }
        }

       

        
       
        private void HideAllUI() {
            if (_loadingUI.enabled)
            {
                _loadingUI.enabled = false;
            }

            if (_menuUI.enabled)
            {
                Task t = FadeInOut(_menuUI.GetComponent<CanvasGroup>(), true);
              //  t.Wait();
                _menuUI.enabled = false;
            }
            if (_endUI.enabled)
            {
                Task t = FadeInOut(_endUI.GetComponent<CanvasGroup>(), true);
              //  t.Wait();
                _endUI.enabled = false;
            }
            if(_gameUI.enabled)
            {
                Task t = FadeInOut(_gameUI.GetComponent<CanvasGroup>(), true);
              //  t.Wait();
                _gameUI.enabled = false;
            }
            
        }
        public void HideHintText()
        {
            Task t = FadeInOut(_hintUI.GetComponent<CanvasGroup>(), true);
           // t.Wait();
            _hintUI.enabled = false;
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
            _endUI.enabled = true;
            Task t = FadeInOut(_endUI.GetComponent<CanvasGroup>(), false);

        }

        public void ShowHintText() {
            _hintUI.enabled = true;
            Task t = FadeInOut(_hintUI.GetComponent<CanvasGroup>(), false);
        }

        public void StartGameUI() {
            HideAllUI();

            _gameUI.enabled = true;
            Task t = FadeInOut(_gameUI.GetComponent<CanvasGroup>(), false);

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