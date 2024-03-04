using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Система очков здоровья
/// </summary>
public class HealthSystem : MonoBehaviour
{
	[Header("Задержка между двумя изменениями здоровья в секундах")]
	[SerializeField] 
	private float _healthChangeDelay = 0.5f;

	private CharacterStatsController _statsHandler;
	private float _timeSinceLastChange = float.MaxValue;

	public UnityEvent OnDamage { get; private set; } = new UnityEvent();
	public UnityEvent OnHeal { get; private set; } = new UnityEvent();
	public UnityEvent OnDeath { get; private set; } = new UnityEvent();
	public UnityEvent OnInvincibilityEnd { get; private set; } = new UnityEvent();

	public float CurrentHealth { get; private set; }
	public float MaxHealth => _statsHandler.CurrentStats.MaxHealth;

	private void Awake()
	{
		_statsHandler = GetComponent<CharacterStatsController>();
		DisappearOnDeath onDeath = GetComponent<DisappearOnDeath>();
		if (onDeath != null)
			OnDeath.AddListener(onDeath.OnDeath);
	}

	private void Start()
	{
		CurrentHealth = _statsHandler.CurrentStats.MaxHealth;
	}

	private void Update()
	{
		if (_timeSinceLastChange < _healthChangeDelay)
		{
			_timeSinceLastChange += Time.deltaTime;
			if (_timeSinceLastChange >= _healthChangeDelay)
			{
				OnInvincibilityEnd.Invoke();
			}
		}
	}

	private void Death() => OnDeath.Invoke();

	/// <summary>
	/// Изнение уровня жизни (урон/лечение)
	/// </summary>
	/// <param name="change"> Значение </param>
	/// <returns></returns>
	public bool ChangeHealth(float change)
	{
		if (change == 0 || _timeSinceLastChange < _healthChangeDelay)
			return false;

		_timeSinceLastChange = 0f;
		CurrentHealth += change;
		CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
		CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

		if (change > 0)
			OnHeal.Invoke();
		else
			OnDamage.Invoke();

		if (CurrentHealth <= 0f)
			Death();

		return true;
	}

}
