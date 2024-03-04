using UnityEngine;

/// <summary>
/// ���������� ������� ����������
/// </summary>
public class BarFX : MonoBehaviour
{
	private readonly int PlayEffect = Animator.StringToHash("PlayEffect");

	[SerializeField] 
	private Animator _effectAnimator;

	/// <summary>
	/// ������ �������
	/// </summary>
	public void StartGaugeFX() => _effectAnimator.SetTrigger(PlayEffect);

}
