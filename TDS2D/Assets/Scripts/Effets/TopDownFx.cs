using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Логика визуальных и звуковых эффектов
/// </summary>
public class TopDownFx : MonoBehaviour
{
	[SerializeField] private List<Effect> _effects;
	private CharacterController _controller;

	private HealthSystem _healthSystem;
	private PickupItem _pickup;

	private void Awake()
	{
		_pickup = GetComponent<PickupItem>();
		_healthSystem = GetComponent<HealthSystem>();
		_controller = GetComponent<CharacterController>();
	}

	private void Start()
	{
		if (_pickup != null)
		{
			TryAddListener(_pickup.OnPickup, TriggerEvents.Pickup);
		}

		if (_healthSystem != null)
		{
			TryAddListener(_healthSystem.OnDamage, TriggerEvents.Damage);
			TryAddListener(_healthSystem.OnDeath, TriggerEvents.Death);
			TryAddListener(_healthSystem.OnHeal, TriggerEvents.Heal);
			TryAddListener(_healthSystem.OnInvincibilityEnd, TriggerEvents.InvincibilityEnd);
		}

		if (_controller != null)
		{
			TryAddListener(_controller.OnAttackEvent, TriggerEvents.Attack);
			TryAddListener(_controller.OnLookEvent, TriggerEvents.Look);
			TryAddListener(_controller.OnMoveEvent, TriggerEvents.Walk);
		}
	}

	/// <summary>
	///     Пытается добавить прослушиватель для указанного события
	/// </summary>
	/// <param name="evt"> Объект события Unity</param>
	/// <param name="evtTrigger"> Триггерное событие</param>
	private void TryAddListener(object evt, TriggerEvents evtTrigger)
	{
		if (_effects.All(effect => effect.TriggerEvent != evtTrigger))
			return;

		switch (evtTrigger)
		{
			case TriggerEvents.Attack:
				((UnityEvent<AttackConfig>)evt).AddListener(delegate (AttackConfig arg0)
				{
					TriggerEffects(evtTrigger);
				});
				break;
			case TriggerEvents.Look:
			case TriggerEvents.Walk:
				((UnityEvent<Vector2>)evt).AddListener(delegate { TriggerEffects(evtTrigger); });
				break;
			default:
				((UnityEvent)evt).AddListener(delegate { TriggerEffects(evtTrigger); });
				break;
		}
	}

	/// <summary>
	///     Запускает все эффекты, связанные с указанным событием
	/// </summary>
	/// <param name="TriggerEvent"> Событие, которое было вызвано </param>
	private void TriggerEffects(TriggerEvents TriggerEvent)
	{
		foreach (Effect effect in _effects)
		{
			if (effect.TriggerEvent != TriggerEvent)
				continue;

			if (effect.ParticleSystem != null)
				CreateParticles(effect.ParticleSystem);

			if (effect.SoundEffect != null)
				PlaySound(effect.SoundEffect);

		}
	}

	/// <summary>
	/// Создает один пучок частиц
	/// </summary>
	/// <param name="ps"> Система частиц для излучения </param>
	private static void CreateParticles(ParticleSystem ps)
	{
		ps.Stop();
		ps.Play();
	}

	private void PlaySound(AudioClip clip) => SoundManager.PlaySoundEffect(clip);

}
