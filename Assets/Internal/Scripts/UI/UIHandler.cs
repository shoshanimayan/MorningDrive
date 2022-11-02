using General;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Web;
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
        [SerializeField] TextMeshProUGUI _lengthText;
        [SerializeField] TextMeshProUGUI _wordOfTheDayText;


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
            _wordOfTheDayText.enabled = false;


        }

      
        private void Update()
        {
            
            if (_showTip && Input.GetMouseButtonUp(0))
            {
                _showTip = false;
                HideHintText();
            }
        }

        private async void  FadeInOutCanvas(CanvasGroup canvas, bool fade)
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

        private async void FadeInText(TextMeshProUGUI text)
        {
            
            var color = text.color;
            float alpha = 0;
            text.enabled = true;
            text.color = new Color(color.r, color.g, color.b, alpha);
            while (text.color.a < 1)
            {
                alpha += Time.deltaTime;
                text.color = new Color(color.r, color.g, color.b, alpha);
                await Task.Yield();

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
                 FadeInOutCanvas(_menuUI.GetComponent<CanvasGroup>(), true);
              
            }
            if (_endUI.enabled)
            {
                 FadeInOutCanvas(_endUI.GetComponent<CanvasGroup>(), true);

               
            }
            if (_gameUI.enabled)
            {
               FadeInOutCanvas(_gameUI.GetComponent<CanvasGroup>(), true);

                
            }
           

        }


        private async void GetRandomWordOfTheDay()
        {
            _wordOfTheDayText.enabled = false;
            var word = await new RandomWordCall().GetRandomWord();
            if (word != null)
            {
                _wordOfTheDayText.text = "Random Word Of The Day: " + word;
                FadeInText(_wordOfTheDayText);
            }
            
        }


        private void HideHintText()
        {
             FadeInOutCanvas(_hintUI.GetComponent<CanvasGroup>(), true);
        }
        private void ShowMenuUI() 
        {
           
            HideAllUI();
            _menuUI.enabled = true;
            GetRandomWordOfTheDay();
            FadeInOutCanvas(_menuUI.GetComponent<CanvasGroup>(), false);

        }

        private void ShowEndGameUI()
        {
            HideAllUI();
            _showTip = false;
            HideHintText();

            _endUI.enabled = true;
            FadeInOutCanvas(_endUI.GetComponent<CanvasGroup>(), false);

        }

        private void ShowLoadingUI()
        {
            HideAllUI();           
            _loadingUI.enabled = true;
            _loadingSphere.SetActive(true);
        }

        private void ShowHintText() {
            _hintUI.enabled = true;
              FadeInOutCanvas(_hintUI.GetComponent<CanvasGroup>(), false);
            _showTip = true;

        }

        private void StartGameUI() {
            HideAllUI();
            _gameUI.enabled = true;
             FadeInOutCanvas(_gameUI.GetComponent<CanvasGroup>(), false);
            ShowTipWithWait(5);
        }


      

        ///////////////////////////////
        //  PUBLIC API               //
        ///////////////////////////////

        public void SetLengthText(Slider Length)
        {
            if (Length.value > 1)
            {
                _lengthText.text = "Length: " + Length.value.ToString() + " minutes";
            }
            else
            {
                _lengthText.text = "Length: 1 minute";

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