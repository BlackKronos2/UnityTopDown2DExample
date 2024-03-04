using UnityEngine;

/// <summary>
/// Информация об оружии
/// </summary>
[CreateAssetMenu(fileName = "WeaponConfig", menuName = "TopDownShooter/RangeWeapon", order = 0)]
public class WeaponConfig : ScriptableObject
{
	[Header("Информация")]
	[SerializeField]
	public AttackConfig AttackConfig;
	[Header("Изображение оружия")]
	public Sprite WeaponSprite;
}
