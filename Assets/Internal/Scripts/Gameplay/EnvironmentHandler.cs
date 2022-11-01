using General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

namespace GamePlay
{
    public class EnvironmentHandler : EventListener
    {
        ///////////////////////////////
        //  INSPECTOR VARIABLES      //
        ///////////////////////////////
        [SerializeField] GameObject[] _trees;
        [SerializeField ]private Transform _treeEndPoint;

        ///////////////////////////////
        //  PRIVATE VARIABLES         //
        ///////////////////////////////

        private Vector3 _origin;

        ///////////////////////
        //  PRIVATE METHODS  //
        ///////////////////////
        private void Awake()
        {
            EventConstants.ToEnd.RegisterListener(this);
            EventConstants.StartTweensEvent.RegisterListener(this);
            if (_trees[0])
            {
                _origin = _trees[0].transform.position;
            }

        }
        private void KillTweens()
        {
            DOTween.KillAll();
        }


        private async void StartTweens()
        {
            foreach (GameObject tree in _trees)
            {
                await Task.Delay(1000);
                tree.transform.DOMoveX(_treeEndPoint.position.x, 4).SetLoops(-1, LoopType.Restart);

            }
        }

        ///////////////////////////////
        //  PUBLIC API               //
        ///////////////////////////////


        public override void OnEventRaised(string gameEventName)
        {
            switch (gameEventName)
            {
                case "ToEnd":
                    KillTweens();
                    break;
                case "StartTweensEvent":
                    StartTweens();
                    break;

            }
        }



    }
}
