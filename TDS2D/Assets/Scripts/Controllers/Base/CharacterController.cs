using UnityEngine;
using UnityEngine.Events;

/// <summary>
///     A basic controller for a character
/// </summary>
[RequireComponent(typeof(CharacterStatsController))]
[RequireComponent(typeof(HealthSystem))]
public abstract class CharacterController : MonoBehaviour
{
	private float _timeSinceLastAttack = float.MaxValue;

	protected bool IsAttacking { get; set; }
	protected HealthSystem HealthSystem { get; private set; }

	#region Events

	public UnityEvent<Vector2> OnMoveEvent { get; set; } = new UnityEvent<Vector2>();
	public UnityEvent<AttackConfig> OnAttackEvent { get; set; } = new UnityEvent<AttackConfig>();
	public UnityEvent<Vector2> OnLookEvent { get; set; } = new UnityEvent<Vector2>();

	#endregion

	public CharacterStatsController Stats { get; private set; }

	protected virtual void Awake()
	{
		Stats = GetComponent<CharacterStatsController>();
		HealthSystem = GetComponent<HealthSystem>();
	}

	protected virtual void Update()
	{
		HandleAttackDelay();
	}

	/// <summary>
	///     Only trigger a attack event when the attack delay is over
	/// </summary>
	private void HandleAttackDelay()
	{
		if (Stats.Weapon == null)
			return;

		if (_timeSinceLastAttack <= Stats.Weapon.AttackConfig.delay)
			_timeSinceLastAttack += Time.deltaTime;

		if (IsAttacking && _timeSinceLastAttack > Stats.Weapon.AttackConfig.delay)
		{
			_timeSinceLastAttack = 0f;
			OnAttackEvent?.Invoke(Stats.Weapon.AttackConfig);
		}
	}

}
