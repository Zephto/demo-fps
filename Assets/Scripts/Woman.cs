using UnityEngine;

public class Woman : MonoBehaviour, Damagable
{

    [SerializeField] private float life = 100;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {

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
