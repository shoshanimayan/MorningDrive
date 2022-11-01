using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameEvent", order = 2)]
public class GameEvent : ScriptableObject
{
	private List<EventListener> listeners =
		new List<EventListener>();

	private void OnEnable()
	{
		listeners.Clear();
	}

	public void Raise()
	{
		for (int i = listeners.Count - 1; i >= 0; i--)
			listeners[i].OnEventRaised(name);
	}

	public void RegisterListener(EventListener listener)
	{ 
		listeners.Add(listener);
		listener.AddEvent(this);
	}

	public void UnregisterListener(EventListener listener)
	{ listeners.Remove(listener); }
}
