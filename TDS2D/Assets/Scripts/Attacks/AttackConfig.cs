using UnityEngine;


/// <summary>
///     The base class for an attack configuration
/// </summary>
public class AttackConfig : ScriptableObject
{
	[Header("Размер пули")]
	public float size;

	[Header("Значение задержки между атаками")]
	public float delay;

	[Header("The damage dealt by an attack")]
	public float power;

	[Header("The Speed of the attack")]
	public float Speed;

	[Header("The possible targets for this attack")]
	public LayerMask Target;
}
