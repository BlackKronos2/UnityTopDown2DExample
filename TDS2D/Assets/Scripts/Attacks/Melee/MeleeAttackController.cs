using UnityEngine;

/// <summary>
/// Содержит логику для оружия дальнего боя
/// </summary>
public class MeleeAttackController : MonoBehaviour
{
	private MeleeAttackConfig _attackConfig;
	private Vector3 _endPosition;
	private Quaternion _endRotation;
	private bool _isReady;
	private Vector3 _startPosition;
	private Quaternion _startRotation;
	private float _timeActive;
	private Transform _transform;

	private void Update()
	{
		if (!_isReady)
			return;

		_timeActive += Time.deltaTime;

		if (_timeActive > _attackConfig.Speed)
			Attack();

		// Применить вращение и отдачу
		_transform.localRotation = Quaternion.Lerp(_startRotation, _endRotation, _attackConfig.SwingCurve.Evaluate(_timeActive / _attackConfig.Speed));
		_transform.localPosition = _transform.localRotation * Vector3.Lerp(_startPosition, _endPosition, _attackConfig.ThrustCurve.Evaluate(_timeActive / _attackConfig.Speed));
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (_attackConfig.Target.value == (_attackConfig.Target.value | (1 << other.gameObject.layer)))
		{
			HealthSystem health = other.gameObject.GetComponent<HealthSystem>();
			if (health != null)
			{
				health.ChangeHealth(-_attackConfig.power);
				KnockBackController knockBack = other.gameObject.GetComponent<KnockBackController>();
				if (knockBack != null)
				{
					knockBack.ApplyKnockBack(transform);
				}
			}
		}
	}

	/// <summary>
	/// Инициализирует параметры атаки
	/// </summary>
	/// <param name="attackConfig">Параметры атаки</param>
	public void InitializeAttack(MeleeAttackConfig attackConfig)
	{
		_transform = transform;
		this._attackConfig = attackConfig;

		ComputeSwingRotations();
		ComputeThrustPositions();
		ScaleAttack();

		_transform.localRotation = _startRotation;
		_transform.localPosition = _startPosition;

		_timeActive = 0f;
		_isReady = true;
	}

	/// <summary>
	/// Вычисляет начальное и конечное вращение атаки на основе угла размаха конфигурации
	/// </summary>
	private void ComputeSwingRotations()
	{
		Quaternion rotation = _transform.rotation;
		_startRotation = rotation * Quaternion.Euler(0, 0, -_attackConfig.SwingAngle);
		_endRotation = rotation * Quaternion.Euler(0, 0, _attackConfig.SwingAngle);
	}

	/// <summary>
	/// Вычисляет начальную и конечную позицию атаки
	/// </summary>
	private void ComputeThrustPositions()
	{
		Vector3 position = _transform.localPosition;
		_startPosition = position;
		_endPosition = position + new Vector3(_attackConfig.ThrustDistance, 0, 0);
	}

	/// <summary>
	/// Изменяет масштаб атаки в соответствии с параметрами
	/// </summary>
	private void ScaleAttack()
	{
		transform.localScale = new Vector3(_attackConfig.size, _attackConfig.size, _attackConfig.size);
	}

	/// <summary>
	/// Атакует
	/// </summary>
	private void Attack() => Destroy(gameObject);
}