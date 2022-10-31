using General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

namespace GamePlay
{
    public class EnvironmentHandler : MonoBehaviour
    {
        ///////////////////////////////
        //  INSPECTOR VARIABLES      //
        ///////////////////////////////
        [SerializeField] GameObject[] _trees;
        [SerializeField ]private Transform _treeEndPoint;

        ///////////////////////////////
        //  PUBLIC API               //
        ///////////////////////////////
        public void KillTweens()
        {
            DOTween.KillAll(); 
        }

        public async Task StartTweens()
        {
            foreach (GameObject tree in _trees)
            {
                await Task.Delay(1000);
                tree.transform.DOMoveX(_treeEndPoint.position.x, 4).SetLoops(-1,LoopType.Restart);
                
            }
        }

        

    }
}
