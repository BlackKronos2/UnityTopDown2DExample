using System;
using UnityEngine;

/// <summary>
/// Модификатор оружия дальнего боя
/// </summary>
[CreateAssetMenu(fileName = "RangeWeaponModifier", menuName = "TopDownShooter/Modifires/RangeWeaponModifier", order = 0)]
[Serializable]
public class RangeWeaponModifier : StatsModifier
{
	public RangedAttackConfig AttackConfig;

	public override ModifierType BuffType => ModifierType.WEAPON_RANGE;
}
