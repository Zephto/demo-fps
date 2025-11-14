using UnityEngine;
using UnityEngine.Events;

public class Woman : MonoBehaviour, Damagable
{

	[SerializeField] private float totalLife = 100;
	private float remainingLife = 100;
	[SerializeField] private Animator anim;

	#region Events
	public UnityEvent<float, float> OnDamage = new UnityEvent<float, float>();
	#endregion

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		remainingLife = totalLife;
		OnDamage?.Invoke(totalLife, remainingLife);
		anim.SetTrigger("Scary");
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void GetDamage(float damage)
	{
		remainingLife -= damage;
		OnDamage?.Invoke(totalLife, remainingLife);

		if(remainingLife <= 0)
		{
			anim.SetTrigger("Dead");
			//Destroy(this.gameObject);
		}
	}
}
