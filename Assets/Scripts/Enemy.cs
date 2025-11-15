using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, Damagable
{

	[Header("Combat system")]
	[SerializeField] private Transform attackPoint;
	[SerializeField] private float radius;
	[SerializeField] private float damage;
	[SerializeField] private GameObject explosion;
	[SerializeField] private Animator anim;
	[SerializeField] private GameObject[] itemsToSpawn;

	private NavMeshAgent agent;
	private Woman target;
	private float life = 100;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		agent = this.GetComponent<NavMeshAgent>();
		target = FindAnyObjectByType<Woman>();

		StartCoroutine(UpdateDestinationRoute());
	}

	void Update()
	{

		// Debug.Log("---" + agent.remainingDistance);
		// if(agent.remainingDistance <= agent.stoppingDistance)
		// {
		// 	Debug.Log("Estoy pa atacar");
		// 	Attack();
		// }
	}

	private void PrepareToAttack()
	{
		
		//Focus to taget
		Vector3 targetDirection = (target.transform.position - transform.position).normalized;
		targetDirection.y = 0;

		Quaternion toRotaton = Quaternion.LookRotation(targetDirection);
		this.transform.rotation = toRotaton;
	}

	private void Attack()
	{
		Collider[] colliders = Physics.OverlapSphere(attackPoint.position, radius);
		foreach(Collider coll in colliders)
		{
			if(coll.TryGetComponent(out Damagable damagable))
			{
				damagable.GetDamage(damage);
			}
		}
	}


	private IEnumerator UpdateDestinationRoute()
	{
		anim.SetTrigger("Walk");

		while (target != null)
		{
			Vector3 randomOffset = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5, 5f));
			agent.SetDestination(target.transform.position + randomOffset);
			yield return new WaitForSeconds(0.5f);

			if(agent.remainingDistance <= agent.stoppingDistance)
			{
				//Stop agent
				agent.isStopped = true;

				Debug.Log("Estoy pa atacar");
				PrepareToAttack();
				anim.SetTrigger("Attack");

				yield return new WaitForSeconds(0.3f);
				Attack();

				yield return new WaitForSeconds(1f);
				agent.isStopped = false;

				anim.SetTrigger("Walk");
			}
		}

		Debug.Log("Se murio mi target jsjsj ganamos");
		anim.SetTrigger("Dance");
	}

	public void GetDamage(float damage)
	{
        life -= damage;

        if(life <= 0)
		{
			if(Random.value > 0.9)
			{
				Instantiate(itemsToSpawn[Random.Range(0, itemsToSpawn.Length)], this.transform.position, Quaternion.identity);
			}

			GlobalData.OnZombieKill?.Invoke();
			Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
		}
	}
}
