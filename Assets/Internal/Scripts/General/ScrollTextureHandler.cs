using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace General { 
    public class ScrollTextureHandler : EventListener
    {

        
        //  INSPECTOR VARIABLES      //
        
        
        [SerializeField] private float _slowDown = .05f;
        [SerializeField] private float _offsetY;
        [SerializeField] private float _topSpeed = 8;

        
        //  PRIVATE VARIABLES         //
        

        private Renderer _render;
        private  bool _active;
        private  float _currentSpeed=0;

        
        //  PRIVATE METHODS           //
        
        private async void SpeedUp()
        {
            while (_currentSpeed < _topSpeed)
            {
                await  Task.Yield();
                _currentSpeed += Time.deltaTime;
            }
            _currentSpeed = 50; 
        }

        private void Awake()
        {
            _render = GetComponent<Renderer>();
            _topSpeed = 8;

          
            EventConstants.ToPlay.RegisterListener(this);
            EventConstants.ToEnd.RegisterListener(this);
        }

        
        private void Update()
        {
            if (_active)
            {
                _offsetY = Time.timeSinceLevelLoad * (_currentSpeed * _slowDown);
                _render.material.mainTextureOffset = new Vector2(0, _offsetY);
            }
 
        }

        

        
        //  PUBLIC API               //
        
      

        public override void OnEventRaised(string gameEventName)
        {
            switch (gameEventName)
            {
                
                case "ToPlay":
                    _active = true;
                    _currentSpeed = 0;
                    SpeedUp();
                    break;

                case "ToEnd":
                    _active = false;

                    break;
            }
        }

    }
}
