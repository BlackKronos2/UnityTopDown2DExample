using UnityEngine;

/// <summary>
/// ���������� �� ������
/// </summary>
[CreateAssetMenu(fileName = "WeaponConfig", menuName = "TopDownShooter/RangeWeapon", order = 0)]
public class WeaponConfig : ScriptableObject
{
	[Header("����������")]
	[SerializeField]
	public AttackConfig AttackConfig;
	[Header("����������� ������")]
	public Sprite WeaponSprite;
}
