using UnityEngine;


/// <summary>
/// Controller пулом снарядов.
/// </summary>
public class ProjectileManager : MonoBehaviour
{
	public static ProjectileManager Instance { get; private set; }

	[SerializeField] 
	private ParticleSystem _impactParticleSystem;

	private PoolController _projectilesPool => PoolController.Instance;

	private void Awake()
	{
		Instance = Instance ?? this;
	}

	public void CreateImpactParticlesAtPosition(Vector3 position, RangedAttackConfig config)
	{
		_impactParticleSystem.transform.position = position;
		ParticleSystem.EmissionModule em = _impactParticleSystem.emission;
		em.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(config.size * 5f)));
		ParticleSystem.MainModule mainModule = _impactParticleSystem.main;
		mainModule.startSpeedMultiplier = config.size * 10f;
		_impactParticleSystem.Play();
	}

	/// <summary>
	/// Создаем снаряд из пула и инициализируем его свойства
	/// </summary>
	/// <param name="startPosition"> Точка стрельбы </param>
	/// <param name="direction"> Направление </param>
	/// <param name="config"> Параметры </param>
	public void ShootBullet(Vector2 startPosition, Vector2 direction, RangedAttackConfig config)
	{
		GameObject projectileObject = _projectilesPool.GetPooledObject();

		projectileObject.transform.position = startPosition;
		RangedAttackController rangedAttack = projectileObject.GetComponent<RangedAttackController>();
		rangedAttack.InitializeAttack(direction, config, this);

		projectileObject.SetActive(true);
	}
}
