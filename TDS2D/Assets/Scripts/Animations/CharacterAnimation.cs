using UnityEngine;

/// <summary>
/// Механика анимации персонажей
/// </summary>
public class CharacterAnimation : AnimationBase
{
	private readonly int IsWalking = Animator.StringToHash("IsWalking");
	private readonly int Attack = Animator.StringToHash("Attack");
	private readonly int IsHurt = Animator.StringToHash("IsHurt");

	[SerializeField]
	private bool _createDustOnWalk = true;
	[SerializeField]
	private ParticleSystem _dustParticleSystem;
	private HealthSystem _healthSystem;

	protected override void Awake()
	{
		base.Awake();
		_healthSystem = GetComponent<HealthSystem>();
	}

	protected void Start()
	{
		_controller.OnAttackEvent.AddListener(_ => Attacking());
		_controller.OnMoveEvent.AddListener(Move);

		if (_healthSystem == null)
			return;

		_healthSystem.OnDamage.AddListener(Hurt);
		_healthSystem.OnInvincibilityEnd.AddListener(InvincibilityEnd);
		_healthSystem.OnDeath.AddListener(Death);
	}

	protected void Death()
	{
		_controller.OnAttackEvent.RemoveListener(_ => Attacking());
		_controller.OnMoveEvent.RemoveListener(Move);

		if (_healthSystem == null)
			return;

		_healthSystem.OnDamage.RemoveListener(Hurt);
		_healthSystem.OnInvincibilityEnd.RemoveListener(InvincibilityEnd);
		_healthSystem.OnDeath.RemoveListener(Death);
	}

	/// <summary>
	/// Вызывается, когда персонаж двигается
	/// </summary>
	/// <param name="movementDirection"> Новое направление движения </param>
	private void Move(Vector2 movementDirection) => _animator.SetBool(IsWalking, movementDirection.magnitude > .5f);

	/// <summary>
	/// Вызывается, когда персонаж атакует
	/// </summary>
	private void Attacking() => _animator.SetTrigger(Attack);

	/// <summary>
	/// Вызывается, когда персонаж получает урон
	/// </summary>
	private void Hurt() => _animator.SetBool(IsHurt, true);

	/// <summary>
	/// Вызывается, когда время неуязвимости персонажа заканчивается
	/// </summary>
	public void InvincibilityEnd() => _animator.SetBool(IsHurt, false);

	/// <summary>
	/// Создает частицы пыли, когда персонаж идет, вызывается из анимации
	/// </summary>
	public void CreateDustParticles()
	{
		if (!_createDustOnWalk)
			return;

		_dustParticleSystem.Stop();
		_dustParticleSystem.Play();
	}
}