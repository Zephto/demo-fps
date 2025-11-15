using UnityEngine;
using UnityEngine.Events;

public static class GlobalData
{
	public static UnityEvent OnPlayerShot = new UnityEvent();
	public static UnityEvent OnZombieKill = new UnityEvent();
	public static UnityEvent<float> OnGrabHealth = new UnityEvent<float>();
}
