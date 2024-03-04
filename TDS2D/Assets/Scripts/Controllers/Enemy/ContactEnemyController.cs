using UnityEngine;

/// <summary>
/// ИИ врага с оружие ближнего боя
/// </summary>
public class ContactEnemyController : EnemyController
{
	[SerializeField]
	[Range(0f, 100f)] 
	private float _followRange;

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		Vector2 direction = Vector2.zero;
		if (DistanceToTarget() < _followRange)
			direction = DirectionToTarget();

		OnMoveEvent.Invoke(direction);
	}
}
