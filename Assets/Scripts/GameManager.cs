using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
	#region Game References
	[Header("Player references")]
	[SerializeField] private Woman woman;
	[SerializeField] private PlayerFirstPerson player;
	[SerializeField] private ParticleSystem rain;

	[Header("Music references")]
	[SerializeField] private AudioManager audioManager;
	[SerializeField] private AudioClip shootSound;
	[SerializeField] private AudioClip music;
	[SerializeField] private AudioClip itemSound;
	[SerializeField] private AudioClip explosion;
	
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
		audioManager.PlayMusic(music, 0.4f);

		StartCoroutine(GameplayCinematic());

		GlobalData.OnPlayerShot.AddListener(()=> audioManager.PlaySfx(shootSound, 0.25f));

		woman.OnUpdateHealth.AddListener((total, remaining)=>{
			hud.UpdateHealthBar(total, remaining);
			audioManager.PlaySfx(itemSound, 1.2f);
			
			if(remaining <= 0)
			{
				StartCoroutine(GameoverCinematic());
			}
		});

		GlobalData.OnZombieKill.AddListener(()=> {
			
			audioManager.PlaySfx(explosion, 0.4f);
			zombieCounter++;

			if(zombieCounter == 20)
			{
				rain.Play();
			}
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
