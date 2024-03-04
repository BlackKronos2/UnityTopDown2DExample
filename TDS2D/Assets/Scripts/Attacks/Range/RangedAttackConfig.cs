using System;
using UnityEngine;


/// <summary>
/// Параметры оружия дальнего боя
/// </summary>
[CreateAssetMenu(fileName = "RangedAttackConfig", menuName = "TopDownShooter/Attacks/Range", order = 0)]
[Serializable]
public class RangedAttackConfig : AttackConfig
{
	[Header("Время существования снаряда")]
	public float Duration;

	[Header("Отклонение угла снаряда")]
	public float Spread;

	[Header("Количество пуль за один выстрел")]
	public int NumberOfProjectilesPerShot;

	[Header("Угол между каждым пулями")]
	public float MultipleProjectilesAngle;

	[Header("Цвет снаряда")]
	public Color ProjectileColor;
}