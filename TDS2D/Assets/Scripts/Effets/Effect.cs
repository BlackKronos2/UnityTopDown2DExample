using System;
using UnityEngine;


/// <summary>
/// Типы событий
/// </summary>
public enum TriggerEvents
{
	Walk,
	Look,
	Attack,
	Heal,
	Damage,
	Death,
	InvincibilityEnd,
	Pickup
}

/// <summary>
/// Информация об эффекте
/// </summary>
[Serializable]
public class Effect
{
	public TriggerEvents TriggerEvent;
	public ParticleSystem ParticleSystem;
	public AudioClip SoundEffect;
	public bool ScreenShake;
}
