using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
	private readonly List<GameEventListener> EventListeners = new List<GameEventListener>();

	public void Raise()
	{
		for (int i = EventListeners.Count -1; i >= 0; i--)
			EventListeners[i].OnEventRaised();
	}

	public void RegisterListener(GameEventListener listener)
	{
		if (!EventListeners.Contains(listener))
			EventListeners.Add(listener);
	}
	
	public void UnregisterListener(GameEventListener listener)
	{
		if (EventListeners.Contains(listener))
			EventListeners.Remove(listener);
	}
}
