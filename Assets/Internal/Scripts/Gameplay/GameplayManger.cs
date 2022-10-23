using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManger : MonoBehaviour
{

    private float _maxTime = 60;


    private UIHandler _uIHandler;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public float MaxTime {
        get { return _maxTime; }
        private set
        {
            if (value == _maxTime/60)
                return;
            _maxTime = value * 60;
        }

    }
}
