using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using Window;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameState", order = 1)]
public class GameState : ScriptableObject
{
    public GameStateType State;
    public float PlayLength;


    private void OnEnable()
    {
        State = GameStateType.Loading;
        PlayLength = 1;
        
    }
}
