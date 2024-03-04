using UnityEngine;


/// <summary>
/// Логика оружия дальнего боя
/// </summary>
public class RangedAttackController : MonoBehaviour
{
	[SerializeField]
	private LayerMask _levelCollisionLayer;

	private RangedAttackConfig _config;
	private float _currentDuration;
	private Vector2 _direction;
	private ParticleSystem _impactParticleSystem;
	private bool _isReady;
	private Rigidbody2D _rb;
	private SpriteRenderer _spriteRenderer;
	private TrailRenderer _trail;
	private ProjectileManager _projectileManager;

	private bool _effectsOn = true;

	private bool OnHit { get; set; } = true;

	public ref Vector2 Direction => ref _direction;

	private void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_rb = GetComponent<Rigidbody2D>();
		_trail = GetComponent<TrailRenderer>();
	}


	/// <summary>
	/// Инициализирует параметры
	/// </summary>
	/// <param name="direction"> Направление атаки </param>
	/// <param name="config"> Параметры дальнего боя</param>
	/// <param name="projectileManager"></param>
	public void InitializeAttack(Vector2 direction, RangedAttackConfig config, ProjectileManager projectileManager)
	{
		_projectileManager = projectileManager;
		_config = config;
		_direction = direction;
		UpdateProjectileSprite();
		_trail.Clear();
		_currentDuration = 0f;
		_spriteRenderer.color = config.ProjectileColor;

		_isReady = true;
	}

	private void UpdateProjectileSprite()
	{
		transform.localScale = Vector3.one * _config.size;
	}

	/// <summary>
	/// Выстрел
	/// </summary>
	/// <param name="pos">Позиция, где создать частицы</param>
	/// <param name="createFx">Создавать ли частицы</param>
	private void Projectile(Vector2 pos, bool createFx)
	{
		if (createFx)
			_projectileManager.CreateImpactParticlesAtPosition(pos, _config);

		gameObject.SetActive(false);
	}

	private void Update()
	{
		if (!_isReady)
			return;

		_currentDuration += Time.deltaTime;

		if (_currentDuration > _config.Duration)
			Projectile(transform.position, false);

		_rb.velocity = _direction * _config.Speed;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (_levelCollisionLayer.value == (_levelCollisionLayer.value | (1 << other.gameObject.layer)))
		{
			if (OnHit)
			{
				Projectile(other.ClosestPoint(transform.position) - _direction * .2f, _effectsOn);
			}
		}
		else if (_config.Target.value == (_config.Target.value | (1 << other.gameObject.layer)))
		{
			HealthSystem health = other.gameObject.GetComponent<HealthSystem>();
			if (health != null)
			{
				health.ChangeHealth(-_config.power);
				KnockBackController knockBack = other.gameObject.GetComponent<KnockBackController>();
				if (knockBack != null)
				{
					knockBack.ApplyKnockBack(transform);
				}
			}

			if (OnHit)
				Projectile(other.ClosestPoint(transform.position), _effectsOn);
		}
	}
}
