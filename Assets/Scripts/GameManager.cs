using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
	#region Game References
	[Header("Player references")]
	[SerializeField] private Woman woman;
	[SerializeField] private PlayerFirstPerson player;
	
	[Header("General references")]
	[SerializeField] private EnemySpawner spawner;

	[Header("HUD reference")]
	[SerializeField] private HUDBehaviour hud;
	#endregion

	#region Private variables
	private int zombieCounter = 0;
	#endregion

	private void Start() {
		zombieCounter = 0;
		player.CanMove = false;

		StartCoroutine(GameplayCinematic());

		woman.OnUpdateHealth.AddListener((total, remaining)=>{
			hud.UpdateHealthBar(total, remaining);
			
			if(remaining <= 0)
			{
				StartCoroutine(GameoverCinematic());
			}
		});

		GlobalData.OnZombieKill.AddListener(()=> {
			Debug.Log("Zombie matao");
			zombieCounter++;
		});
	}

	private IEnumerator GameplayCinematic()
	{
		Cursor.lockState = CursorLockMode.None;

		hud.ShowInstructions(true);
		yield return new WaitForSeconds(3f);
		hud.ShowInstructions(false);

		Cursor.lockState = CursorLockMode.Locked;
		player.CanMove = true;

		hud.ShowGameplayHUD();
		yield return new WaitForSeconds(1f);

		spawner.StartSpawn();
	}

	private IEnumerator GameoverCinematic()
	{
		Cursor.lockState = CursorLockMode.None;
		player.CanMove = false;

		hud.ShowGameoverHUD(zombieCounter);

		yield return null;
	}
}
