using System;
using UnityEngine;

/// <summary>
/// Модификатор показателей персонажа
/// </summary>
[CreateAssetMenu(fileName = "CharacterModifier", menuName = "TopDownShooter/Modifires/CharacterModifier", order = 0)]
[Serializable]
public class CharacterModifier : StatsModifier
{
	[Range(0, 100)]
	public int MaxHealth;

	[Range(0f, 20f)]
	[Header("Скорость перемещения персонажа")]
	public float Speed;

	public override ModifierType BuffType => ModifierType.CHARACTER;
}
