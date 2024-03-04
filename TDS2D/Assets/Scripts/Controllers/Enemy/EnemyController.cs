using UnityEngine;


/// <summary>
/// ИИ врага
/// </summary>
public abstract class EnemyController : CharacterController
{
	protected Transform ClosestTarget { get; private set; }

	protected override void Awake()
	{
		base.Awake();
	}

	protected virtual void Start()
	{ 
		LevelEntityList.Instance.AddEnemy(this);
		HealthSystem.OnDeath.AddListener(() => LevelEntityList.Instance.RemoveEnemy(this));
		ClosestTarget = FindClosestTarget();
	}

	protected virtual void FixedUpdate()
	{
		ClosestTarget = FindClosestTarget();
	}

	private Transform FindClosestTarget()
	{
		var closePlayer = LevelEntityList.Instance.FindPlayer(transform.position);

		if (closePlayer == null)
			return null;

		return closePlayer.transform;
	}
	protected float DistanceToTarget() => ClosestTarget == null ? 0f : Vector3.Distance(transform.position, ClosestTarget.transform.position);
	protected Vector2 DirectionToTarget() => ClosestTarget == null ? Vector2.zero : (ClosestTarget.transform.position - transform.position).normalized;
}
