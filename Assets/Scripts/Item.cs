using UnityEngine;

public class Item : MonoBehaviour
{
	[SerializeField] private GameObject grabParticles;

	void OnDestroy()
	{
		Instantiate(grabParticles, this.transform.position, Quaternion.identity);
	}
}
