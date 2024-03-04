using UnityEngine;


/// <summary>
///  Логика перемещения персонажа
/// </summary>
[RequireComponent(typeof(CharacterStatsController))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{
	private CharacterController _controller;

	private Vector2 _movementDirection = Vector2.zero;
	private Rigidbody2D _rb;
	private CharacterStatsController _stats;

	private void Awake()
	{
		_controller = GetComponent<CharacterController>();
		_stats = GetComponent<CharacterStatsController>();
		_rb = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		_controller.OnMoveEvent.AddListener(Move);
	}

	private void FixedUpdate()
	{
		ApplyMovement(_movementDirection);
	}

	/// <summary>
	/// Смена направления движения
	/// </summary>
	/// <param name="direction"></param>
	private void Move(Vector2 direction)
	{
		_movementDirection = direction;
	}

	/// <summary>
	/// Назначение вектора движения
	/// </summary>
	/// <param name="direction"> Вектор движения </param>
	private void ApplyMovement(Vector2 direction)
	{
		_rb.velocity += direction * _stats.CurrentStats.Speed;
	}
}
