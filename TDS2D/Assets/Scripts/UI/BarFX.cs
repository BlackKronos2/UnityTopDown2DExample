using UnityEngine;

/// <summary>
/// Визуальные эффекты индикатора
/// </summary>
public class BarFX : MonoBehaviour
{
	private readonly int PlayEffect = Animator.StringToHash("PlayEffect");

	[SerializeField] 
	private Animator _effectAnimator;

	/// <summary>
	/// Запуск эффекта
	/// </summary>
	public void StartGaugeFX() => _effectAnimator.SetTrigger(PlayEffect);

}
