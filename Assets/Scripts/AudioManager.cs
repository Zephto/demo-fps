using System.Diagnostics;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	// public static AudioManager Instance;

	[SerializeField] private AudioSource musicSource;
	[SerializeField] private AudioSource sfxSource;

	// void Awake()
	// {
	// 	if(Instance == null)
	// 	{
	// 		Instance = this;
	// 		DontDestroyOnLoad(this.gameObject);
	// 	}
	// 	else
	// 	{
	// 		Destroy(this.gameObject);
	// 	}
	// }

	public void PlayMusic(AudioClip music, float volume = 1f)
	{	
		musicSource.clip = music;
		musicSource.volume = volume;
		musicSource.loop = true;
		musicSource.Play();
	}

	public void PlaySfx(AudioClip sfx, float volume = 1f)
	{
		sfxSource.volume = volume;
		sfxSource.PlayOneShot(sfx);
	}
}
