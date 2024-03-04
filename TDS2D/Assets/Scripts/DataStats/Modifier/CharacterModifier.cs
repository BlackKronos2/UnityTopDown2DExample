using System;
using UnityEngine;

/// <summary>
/// ����������� ����������� ���������
/// </summary>
[CreateAssetMenu(fileName = "CharacterModifier", menuName = "TopDownShooter/Modifires/CharacterModifier", order = 0)]
[Serializable]
public class CharacterModifier : StatsModifier
{
	[Range(0, 100)]
	public int MaxHealth;

	[Range(0f, 20f)]
	[Header("�������� ����������� ���������")]
	public float Speed;

	public override ModifierType BuffType => ModifierType.CHARACTER;
}
