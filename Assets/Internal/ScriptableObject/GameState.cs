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
    public DrawOnTexture Window;
    public ScrollTextureHandler ScrollTextureHandler;

    private void OnEnable()
    {
        State = GameStateType.Loading;
        PlayLength = 1;
        Window = null;
        ScrollTextureHandler = null;
    }
}
