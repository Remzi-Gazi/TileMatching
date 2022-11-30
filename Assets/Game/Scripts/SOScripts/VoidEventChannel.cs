using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have no arguments (Example: Exit game event)
/// </summary>

[CreateAssetMenu(menuName = "Scriptable Objects/Events/Void Event Channel")]
public class VoidEventChannel : EventChannel
{
	public UnityAction OnEventRaised;

	public void RaiseEvent()
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke();
	}

	
}


