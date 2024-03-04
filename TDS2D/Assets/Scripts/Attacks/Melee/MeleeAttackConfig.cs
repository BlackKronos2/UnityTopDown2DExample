using System;
using UnityEngine;

/// <summary>
/// Показатели оружия ближнего боя
/// </summary>
[CreateAssetMenu(fileName = "MeleeConfig", menuName = "TopDownShooter/Attacks/Melee", order = 0)]
[Serializable]
public class MeleeAttackConfig : AttackConfig
{
	[Header("Угол горизонтального размаха атаки")]
	public float SwingAngle;

	[Header("Дистанция толчка атаки")]
	public float ThrustDistance;

	[Header("Кривая, используемая для управления горизонтальным размахом меча")]
	public AnimationCurve SwingCurve;

	[Header("Кривая, используемая для управления положением толчка меча")]
	public AnimationCurve ThrustCurve;
}
