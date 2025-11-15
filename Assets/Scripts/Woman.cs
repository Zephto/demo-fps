using UnityEngine;
using UnityEngine.Events;

public class Woman : MonoBehaviour, Damagable
{

	[SerializeField] private float totalLife = 100;
	private float remainingLife = 100;
	[SerializeField] private Animator anim;

	#region Events
	public UnityEvent<float, float> OnUpdateHealth = new UnityEvent<float, float>();
	#endregion

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		remainingLife = totalLife;
		OnUpdateHealth?.Invoke(totalLife, remainingLife);
		anim.SetTrigger("Scary");

		GlobalData.OnGrabHealth.AddListener((value) =>
		{
			remainingLife += value;
			if(remainingLife > totalLife) remainingLife = totalLife;
			OnUpdateHealth?.Invoke(totalLife, remainingLife);
		});
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void GetDamage(float damage)
	{
		remainingLife -= damage;
		OnUpdateHealth?.Invoke(totalLife, remainingLife);

		if(remainingLife <= 0)
		{
			anim.SetTrigger("Dead");
			//Destroy(this.gameObject);
		}
	}
}
