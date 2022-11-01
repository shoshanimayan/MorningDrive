using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventConstants : MonoBehaviour
{
    public static GameEvent ToMenu { get; private set; }
    public static GameEvent ToPlay { get; private set; }
    public static GameEvent ToLoading { get; private set; }
    public static  GameEvent ToEnd { get; private set; }
    public static GameEvent LoadSceneEvent { get; private set; }
    public static GameEvent StartTweensEvent { get; private set; }
    public static GameEvent SpeedBumpEvent { get; private set; }
    public static GameEvent WipeEvent { get; private set; }



    private void Awake()
    {
        ToMenu = Resources.Load<GameEvent>("Events/ToMenu");
        ToPlay = Resources.Load<GameEvent>("Events/ToPlay");
        ToLoading = Resources.Load<GameEvent>("Events/ToLoading");
        ToEnd = Resources.Load<GameEvent>("Events/ToEnd");
        LoadSceneEvent = Resources.Load<GameEvent>("Events/LoadSceneEvent");
        StartTweensEvent = Resources.Load<GameEvent>("Events/StartTweensEvent");
        SpeedBumpEvent = Resources.Load<GameEvent>("Events/SpeedBumpEvent");
        WipeEvent = Resources.Load<GameEvent>("Events/WipeEvent");



    }
}
