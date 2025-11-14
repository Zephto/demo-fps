using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDBehaviour : MonoBehaviour
{
	#region Public references
	[Header("Instructions references")]
	[SerializeField] private CanvasGroup canvasInstructions;

	[Header("Gameplay references")]
	[SerializeField] private CanvasGroup canvasGameplay;
	[SerializeField] private Slider healthBar;

	[Header("Gameover references")]
	[SerializeField] private CanvasGroup canvasGameover;
	[SerializeField] private TextMeshProUGUI finalText;
	[SerializeField] private Button buttonPlayAgain;
	[SerializeField] private Button buttonMainMenu;
	#endregion

	private void Start() {
		canvasInstructions.alpha = 0f;
		canvasInstructions.interactable = false;
		canvasInstructions.blocksRaycasts = false;

		canvasGameplay.alpha = 0f;
		canvasGameplay.interactable = false;
		canvasGameplay.blocksRaycasts = false;

		canvasGameover.alpha = 0f;
		canvasGameover.interactable = false;
		canvasGameover.blocksRaycasts = false;


		buttonPlayAgain.onClick.AddListener(()=>{});
		buttonMainMenu.onClick.AddListener(()=>{});
	}

	#region Public methods
	public void UpdateHealthBar(float total, float remaining)
	{
		Debug.Log("Update life: " + (remaining/total));
		healthBar.value = remaining/total;
	}

	public void ShowInstructions(bool set)
	{
		StartCoroutine(Fade(canvasInstructions, set? 1f: 0f, 0.3f));
	}

	public void ShowGameplayHUD()
	{
		StartCoroutine(Fade(canvasGameplay, 1f, 0.3f));
	}

	public void ShowGameoverHUD()
	{
		StartCoroutine(Fade(canvasGameplay, 0f, 0.3f));
		StartCoroutine(Fade(canvasGameover, 1f, 0.3f));
	}
	#endregion

	private IEnumerator Fade(CanvasGroup group, float target, float duration)
	{
		float start = group.alpha;
		float time = 0f;

		while(time < duration)
		{
			time += Time.deltaTime; 
			group.alpha = Mathf.Lerp(start, target, time/duration);
			yield return null;
		}

		group.alpha = target;

		if(target > 0.99f)
		{
			group.blocksRaycasts = true;
			group.interactable = true;
		}

		if(target < 0.01f)
		{
			group.blocksRaycasts = false;
			group.interactable = false;
		}
	}
}
