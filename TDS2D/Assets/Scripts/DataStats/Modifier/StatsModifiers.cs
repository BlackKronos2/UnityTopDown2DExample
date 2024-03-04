using System;
using UnityEngine;


/// <summary>
/// Операции модификации
/// </summary>
[Serializable]
public enum StatsChangeType
{
	ADD,
	MULTIPLY,
	OVERRIDE,
}

/// <summary>
/// Тип модификации
/// </summary>
[Serializable]
public enum ModifierType
{ 
	NONE,
	WEAPON_RANGE,
	CHARACTER,
}

[Serializable]
/// <summary>
/// Модификатор показателей
/// </summary>
public abstract class StatsModifier : ScriptableObject
{
	public StatsChangeType StatsChangeType;
	public virtual ModifierType BuffType => ModifierType.NONE;
}



