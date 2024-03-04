using System;
using UnityEngine;


/// <summary>
/// �������� �����������
/// </summary>
[Serializable]
public enum StatsChangeType
{
	ADD,
	MULTIPLY,
	OVERRIDE,
}

/// <summary>
/// ��� �����������
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
/// ����������� �����������
/// </summary>
public abstract class StatsModifier : ScriptableObject
{
	public StatsChangeType StatsChangeType;
	public virtual ModifierType BuffType => ModifierType.NONE;
}



