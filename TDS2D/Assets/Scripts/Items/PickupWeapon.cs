using UnityEngine;

/// <summary>
/// Подбор оружия на уровне
/// </summary>
public class PickupWeapon : PickupItem
{
	[SerializeField]
	private WeaponConfig _weapon;

	protected override void OnPickedUp(GameObject receiver)
	{
		var statsController = receiver.GetComponent<CharacterStatsController>();

		var currentStats = statsController.CurrentStats;
		currentStats.Weapon = _weapon;
		currentStats.Weapon.AttackConfig.Target = currentStats.Target;

		statsController.CharacterStatsChange(currentStats);
	}
}
