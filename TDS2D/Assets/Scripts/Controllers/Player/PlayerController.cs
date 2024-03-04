using UnityEngine;


/// <summary>
/// Контроллер управляемый игроком
/// </summary>
public class PlayerController : CharacterController
{
	protected override void Awake()
	{
		base.Awake();
	}

	protected virtual void Start()
	{ 
		LevelEntityList.Instance.AddPlayer(this);
		HealthSystem.OnDeath.AddListener(() => LevelEntityList.Instance.RemovePlayer(this));
	}

	protected virtual void FixedUpdate()
	{
		Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		OnMoveEvent.Invoke(movement);

		IsAttacking = (Input.GetKey(KeyCode.Space));

		var closeEnemy = LevelEntityList.Instance.FindClosestEnemy(transform.position);

		if (closeEnemy != null)
		{
			Vector2 newAim = closeEnemy.transform.position;
			if (!(newAim.normalized == newAim))
			{
				newAim = (newAim - (Vector2)transform.position).normalized;
			}

			if (newAim.magnitude >= .9f)
			{
				OnLookEvent.Invoke(newAim);
			}
		}
	}
}
