using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, Damagable
{

	private NavMeshAgent agent;
	private float life = 100;
	[SerializeField] private GameObject target;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		agent = this.GetComponent<NavMeshAgent>();
		// target = this.GetComponent<GameObject>();
	
		agent.SetDestination(target.transform.position);
	}

	// Update is called once per frame
	void Update()
	{
	}

	public void GetDamage(float damage)
	{
        life -= damage;

        if(life <= 0)
		{
            Destroy(this.gameObject);
		}
	}
}
