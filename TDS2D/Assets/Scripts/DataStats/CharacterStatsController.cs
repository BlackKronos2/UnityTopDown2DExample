using System;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Обрабатывает показателей персонажа и его оружия. Управление их изменениями.
/// </summary>
public class CharacterStatsController : MonoBehaviour
{
	// Лимит показателей
	#region LIMITS

	private const float MinAttackDelay = 0.03f;
	private const float MinAttackPower = 0.5f;
	private const float MinAttackSize = 0.4f;
	private const float MinAttackSpeed = .1f;

	private const float MinSpeed = 1f;
	private const int MinMaxHealth = 1;

	#endregion

	[SerializeField]
	[Header("Стандартные показатели персонажа")]
	private CharacterStats _baseStats;
	[SerializeField]
	[Header("Показатели оружия")]
	private WeaponConfig _weapon;

	/// <summary>
	/// Список всех модификатор
	/// </summary>
	public readonly ObservableCollection<StatsModifier> StatsModifiers = new ObservableCollection<StatsModifier>();

	/// <summary>
	/// Показатели этого персонажа
	/// </summary>
	public CharacterStats CurrentStats { get; private set; }
	public WeaponConfig Weapon => _weapon;

	public UnityEvent<CharacterStats> OnStatsUpdate { get; private set; } = new UnityEvent<CharacterStats>();

	private void Awake()
	{
		DefaultStats();
		UpdateCharacterStats();
		StatsModifiers.CollectionChanged += (sender, e) => UpdateCharacterStats();
	}

	/// <summary>
	/// Сброс показателей до изначальных
	/// </summary>
	private void DefaultStats()
	{
		CurrentStats = Instantiate(_baseStats);
		_weapon = Instantiate(CurrentStats.Weapon);
		_weapon.AttackConfig = Instantiate(CurrentStats.Weapon.AttackConfig);
	}

	/// <summary>
	/// Вызывается при изменении списка модификаторов показателей, обновляет статистику соответствующим образом
	/// </summary>
	private void UpdateCharacterStats()
	{
		DefaultStats();

		if (_weapon.AttackConfig != null)
			_weapon.AttackConfig.Target = CurrentStats.Target;

		foreach (StatsModifier modifier in StatsModifiers)
		{
			switch (modifier.StatsChangeType)
			{
				case StatsChangeType.OVERRIDE: UpdateStats((o, o1) => o1, modifier); break;
				case StatsChangeType.ADD: UpdateStats((o, o1) => o + o1, modifier); break;
				case StatsChangeType.MULTIPLY: UpdateStats((o, o1) => o * o1, modifier); break;
			}
		}

		LimitAllStats();
	}

	/// <summary>
	/// Обновляет показатели
	/// </summary>
	/// <param name="statsModifier"> Модификатор показателя персонажа </param>
	private void UpdateStats(Func<float, float, float> operation, StatsModifier statsModifier)
	{

		if (CurrentStats == null)
		{
			Debug.LogError("CurrentStats - null");
			return;
		}

		switch (statsModifier.BuffType)
		{
			case ModifierType.WEAPON_RANGE: ModifierRangeWeapon(operation, statsModifier as RangeWeaponModifier); break;
			case ModifierType.CHARACTER: ModifierCharacterStats(operation, statsModifier as CharacterModifier); break;
		}
	}

	/// <summary>
	/// Ограничивает показатели, используя LIMITS
	/// </summary>
	private void LimitAllStats()
	{
		if (CurrentStats == null || CurrentStats == null || _weapon == null)
			return;

		_weapon.AttackConfig.delay = _weapon.AttackConfig.delay < MinAttackDelay ? MinAttackDelay : _weapon.AttackConfig.delay;
		_weapon.AttackConfig.power = _weapon.AttackConfig.power < MinAttackPower ? MinAttackPower : _weapon.AttackConfig.power;
		_weapon.AttackConfig.size = _weapon.AttackConfig.size < MinAttackSize ? MinAttackSize : _weapon.AttackConfig.size;
		_weapon.AttackConfig.Speed = _weapon.AttackConfig.Speed < MinAttackSpeed ? MinAttackSpeed : _weapon.AttackConfig.Speed;
		CurrentStats.Speed = CurrentStats.Speed < MinSpeed ? MinSpeed : CurrentStats.Speed;
		CurrentStats.MaxHealth = CurrentStats.MaxHealth < MinMaxHealth ? MinMaxHealth : CurrentStats.MaxHealth;
	}

	/// <summary>
	/// Применяет показатель модификатора покателей персонажа
	/// </summary>
	/// <param name="operation"> Операция </param>
	/// <param name="characterModifier"> Модификатор </param>
	private void ModifierCharacterStats(Func<float, float, float> operation, CharacterModifier characterModifier)
	{
		if (characterModifier == null)
			return;

		CurrentStats.MaxHealth = (int)operation(CurrentStats.MaxHealth, characterModifier.MaxHealth);
		CurrentStats.Speed = operation(CurrentStats.Speed, characterModifier.Speed);
	}

	/// <summary>
	/// Применяет модификатор статистики дальнего боя
	/// </summary>
	/// <param name="operation"> Операция </param>
	/// <param name="statsModifier"> Модификатор </param>
	private void ModifierRangeWeapon(Func<float, float, float> operation, RangeWeaponModifier statsModifier)
	{
		if (statsModifier == null)
			return;

		Weapon.AttackConfig.delay = operation(_weapon.AttackConfig.delay, statsModifier.AttackConfig.delay);
		_weapon.AttackConfig.power = operation(_weapon.AttackConfig.power, statsModifier.AttackConfig.power);
		_weapon.AttackConfig.size = operation(_weapon.AttackConfig.size, statsModifier.AttackConfig.size);
		_weapon.AttackConfig.Speed = operation(_weapon.AttackConfig.Speed, statsModifier.AttackConfig.Speed);

		RangedAttackConfig currentRangedAttacksStats = (RangedAttackConfig)_weapon.AttackConfig;

		if (statsModifier == null || statsModifier.BuffType != ModifierType.WEAPON_RANGE)
			return;

		currentRangedAttacksStats.MultipleProjectilesAngle = operation(currentRangedAttacksStats.MultipleProjectilesAngle, statsModifier.AttackConfig.MultipleProjectilesAngle);
		currentRangedAttacksStats.Spread = operation(currentRangedAttacksStats.Spread, statsModifier.AttackConfig.Spread);
		currentRangedAttacksStats.Duration = operation(currentRangedAttacksStats.Duration, statsModifier.AttackConfig.Duration);
		currentRangedAttacksStats.NumberOfProjectilesPerShot = Mathf.CeilToInt(operation(currentRangedAttacksStats.NumberOfProjectilesPerShot, statsModifier.AttackConfig.NumberOfProjectilesPerShot));
		currentRangedAttacksStats.ProjectileColor = statsModifier.AttackConfig.ProjectileColor.a != 0 ? statsModifier.AttackConfig.ProjectileColor : currentRangedAttacksStats.ProjectileColor;
	}
	/// <summary>
	/// Изменение параметров персонажа
	/// </summary>
	/// <param name="characterStats"> Новые параметры </param>
	public void CharacterStatsChange(CharacterStats characterStats)
	{
		StatsModifiers.Clear();

		_baseStats = characterStats;
		_weapon = characterStats.Weapon;
		CurrentStats = characterStats;

		UpdateCharacterStats();

		OnStatsUpdate?.Invoke(characterStats);
	}
}
