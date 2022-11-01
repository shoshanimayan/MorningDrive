using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventListener : MonoBehaviour
{
    private List<GameEvent> events = new List<GameEvent>();
    
    private void OnDisable()
    {

        foreach (GameEvent e in events) 
        {
            e.UnregisterListener(this);

        }
    }

    public abstract void OnEventRaised(string gameEventName);

    public void AddEvent(GameEvent e)
    {
        events.Add(e);
    }
}
