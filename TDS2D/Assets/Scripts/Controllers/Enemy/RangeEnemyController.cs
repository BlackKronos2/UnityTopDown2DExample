using UnityEngine;


/// <summary>
/// ИИ врага с оружием дальнего боя
/// </summary>
public class RangeEnemyController : EnemyController
{
	[SerializeField] 
	private float _followRange = 15f;
	[SerializeField] 
	private float _shootRange = 10f;

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		float distance = DistanceToTarget();
		Vector2 direction = DirectionToTarget();

		IsAttacking = false;

		if (distance > _followRange)
		{
			OnMoveEvent.Invoke(Vector2.zero);
			return;
		}

		if (distance > _shootRange) 
		{
			OnMoveEvent.Invoke(direction);
			return;
		}

		int layerMaskTarget = Stats.Weapon.AttackConfig.Target;
		RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 11f, (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);

		if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
		{
			OnLookEvent.Invoke(direction);
			OnMoveEvent.Invoke(Vector2.zero);
			IsAttacking = true;
		}
		else
		{
			OnMoveEvent.Invoke(direction);
		}
	}
}
