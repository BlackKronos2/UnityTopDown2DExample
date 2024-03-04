using UnityEngine;


/// <summary>
/// Логика оружия дальнего боя/ механика стрельбы
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class ShootingController : MonoBehaviour
{
	[SerializeField]
	[Range(0.0f, 2f)]
	[Header("Сила отдачи от выстрела")]
	private float _recoilStrength = 1f;

	[SerializeField]
	[Header("Пропорциональность отдачи и размеру снаряда")]
	private bool _projectileSizeModifyRecoil = true;

	[SerializeField]
	[Header("Точка начала полета пули")]
	private Transform _projectileSpawnPosition;

	private Vector2 _aimDirection = Vector2.right;
	private ProjectileManager _projectileManager;
	private CharacterController _controller;
	private Rigidbody2D _rb;

	private void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
		_controller = GetComponent<CharacterController>();
	}

	private void Start()
	{
		_projectileManager = ProjectileManager.Instance;
		_controller.OnAttackEvent.AddListener(OnShoot);
		_controller.OnLookEvent.AddListener(OnAim);
	}

	/// <summary>
	/// Поворачивает Vector2 на заданное количество градусов
	/// </summary>
	/// <param name="v">Вектор для поворота</param>
	/// <param name="degrees">Угол в градусах</param>
	private static Vector2 RotateVector2(Vector2 v, float degrees) => Quaternion.Euler(0, 0, degrees) * v;

	/// <summary>
	/// Вызывается при изменении направления прицеливания
	/// </summary>
	/// <param name="newAimDirection">Новое направление прицеливания</param>
	public void OnAim(Vector2 newAimDirection)
	{
		_aimDirection = newAimDirection;
	}

	/// <summary>
	/// Вызывается, когда игрок пытается выстрелить
	/// </summary>
	/// <param name="AttackConfig">Характеристики снаряда для выстрела</param>
	public void OnShoot(AttackConfig AttackConfig)
	{
		RangedAttackConfig rangedAttackConfig = (RangedAttackConfig)AttackConfig;
		float projectilesAngleSpace = rangedAttackConfig.MultipleProjectilesAngle;
		float minAngle = -(rangedAttackConfig.NumberOfProjectilesPerShot / 2f) * projectilesAngleSpace + 0.5f * projectilesAngleSpace;

		for (int i = 0; i < rangedAttackConfig.NumberOfProjectilesPerShot; i++)
		{
			float angle = minAngle + projectilesAngleSpace * i;
			float randomSpread = Random.Range(-rangedAttackConfig.Spread, rangedAttackConfig.Spread);
			angle += randomSpread;
			CreateProjectile(rangedAttackConfig, angle);
		}

		if (_rb != null)
		{
			AddRecoil(rangedAttackConfig);
		}
	}

	/// <summary>
	/// Создает снаряд, соответствующий параметрам снаряда
	/// </summary>
	/// <param name="rangedAttackConfig">Конфигурация снаряда для выстрела</param>
	/// <param name="angle">Изменение направления выстрела</param>
	private void CreateProjectile(RangedAttackConfig rangedAttackConfig, float angle)
	{
		_projectileManager.ShootBullet(_projectileSpawnPosition.position, RotateVector2(_aimDirection.normalized, angle), rangedAttackConfig);
	}

	/// <summary>
	/// Добавляет отдачу, соответствующую размеру снаряда
	/// </summary>
	/// <param name="rangedAttackConfig">Показатели выпущенного снаряда</param>
	private void AddRecoil(RangedAttackConfig rangedAttackConfig)
	{
		Vector2 forceVector = _aimDirection * _recoilStrength * 100f;
		forceVector *= (_projectileSizeModifyRecoil) ? rangedAttackConfig.size : 1;

		_rb.AddForce(-forceVector, ForceMode2D.Impulse);
	}
}
