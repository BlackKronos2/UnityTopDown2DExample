using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "CharacterStats", menuName = "TopDownShooter/CharacterConfig", order = 0)]
/// <summary>
/// Показатели персонажа
/// </summary>
public class CharacterStats : ScriptableObject
{
	[Range(0, 100)]
	[Header("Максимальное кол-во здоровья")]
	public int MaxHealth;

	[Range(0f, 20f)]
	[Header("Скорость перемещения персонажа")]
	public float Speed;

	[Header("Оружие персанажа")]
	public WeaponConfig Weapon;

	[Header("Возможные цели")]
	public LayerMask Target;
}
