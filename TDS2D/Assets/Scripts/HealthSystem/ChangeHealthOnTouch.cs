using UnityEngine;


/// <summary>
/// Ќанесени€ урона при контакте
/// </summary>
public class ChangeHealthOnTouch : MonoBehaviour
{
	[Header("«начение изменени€ здоровь€ при контакте")]
	[SerializeField]
	private float value;

	[Header("Unity tag цели")]
	[SerializeField]
	private string _targetTag;

	private HealthSystem _collidingTargetHealthSystem;
	private KnockBackController _collidingTargetKnockBackSystem;

	private bool _isCollidingWithTarget;

	private void FixedUpdate()
	{
		if (_isCollidingWithTarget)
			HealthsChange();

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		GameObject receiver = collision.gameObject;

		if (!receiver.CompareTag(_targetTag))
			return;

		_collidingTargetHealthSystem = receiver.GetComponent<HealthSystem>();
		if (_collidingTargetHealthSystem != null)
			_isCollidingWithTarget = true;

		_collidingTargetKnockBackSystem = receiver.GetComponent<KnockBackController>();
	}


	private void OnTriggerExit2D(Collider2D collision)
	{
		if (!collision.CompareTag(_targetTag))
			return;

		_isCollidingWithTarget = false;
	}

	private void HealthsChange()
	{
		bool hasBeenChanged = _collidingTargetHealthSystem.ChangeHealth(value);

		if (_collidingTargetKnockBackSystem != null && hasBeenChanged)
			_collidingTargetKnockBackSystem.ApplyKnockBack(transform);

	}
}
