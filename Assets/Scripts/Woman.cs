using UnityEngine;

public class Woman : MonoBehaviour, Damagable
{

    [SerializeField] private float life = 100;
    [SerializeField] private Animator anim;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        anim.SetTrigger("Scary");
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
            anim.SetTrigger("Dead");
            //Destroy(this.gameObject);
		}
	}
}
