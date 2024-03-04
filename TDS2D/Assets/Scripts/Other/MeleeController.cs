using UnityEngine;


/// <summary>
/// Логикой оружия ближнего боя
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class MeleeController : MonoBehaviour
{
	[SerializeField]
	private GameObject _attackObject;

	[SerializeField]
	[Header("Точка поворота атаки")]
	private Transform _attackPivot;

	private Vector2 _attackDirection;
	private CharacterController _controller;

	private void Awake()
	{
		_controller = GetComponent<CharacterController>();
	}

	private void Start()
	{
		_controller.OnAttackEvent.AddListener(Attack);
		_controller.OnLookEvent.AddListener(Rotate);
	}

	private void Attack(AttackConfig config)
	{
		if (!(config is MeleeAttackConfig))
			return;

		InstantiateAttack((MeleeAttackConfig)config);
	}

	private void Rotate(Vector2 rotation)
	{
		_attackDirection = rotation;
	}

	/// <summary>
	/// Атака
	/// </summary>
	/// <param name="AttackConfig"> Показатели ближней атаки</param>
	private void InstantiateAttack(MeleeAttackConfig attackConfig)
	{
		_attackPivot.localRotation = Quaternion.identity;
		GameObject obj = Instantiate(_attackObject, _attackPivot.position, Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, _attackDirection)), _attackPivot);
		MeleeAttackController attackController = obj.GetComponent<MeleeAttackController>();
		attackController.InitializeAttack(attackConfig);
	}
}
