using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Поворот оружия персонажа
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class WeaponRotation : MonoBehaviour
{
	[SerializeField]
	[Header("Sprite оружия")]
	private SpriteRenderer _weaponRenderer;

	[SerializeField]
	[Header("Рендер пернажа")]
	private List<SpriteRenderer> _characterRenderers;

	[SerializeField]
	[Header("Положение оружия")]
	private Transform _armPivot;

	private CharacterController _controller;

	private void Awake()
	{
		_controller = GetComponent<CharacterController>();
	}

	private void Start()
	{
		_controller.OnLookEvent.AddListener(OnAim);
		_controller.Stats.OnStatsUpdate.AddListener((characterStats) => OnWeaponChange(characterStats.Weapon));
	}

	private void On()
	{
		_controller.OnLookEvent.RemoveListener(OnAim);
	}

	/// <summary>
	/// Вызов при смене направления атаки
	/// </summary>
	/// <param name="newAimDirection"> направление атаки </param>
	public void OnAim(Vector2 newAimDirection) => RotateArm(newAimDirection);

	/// <summary>
	/// Поворот оружия
	/// </summary>
	/// <param name="direction"> The new aim direction </param>
	private void RotateArm(Vector2 direction)
	{
		float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		bool flipY = Mathf.Abs(rotZ) > 90f;

		_weaponRenderer.flipY = flipY;

		foreach (SpriteRenderer charRenderer in _characterRenderers)
			charRenderer.flipX = flipY;

		_armPivot.rotation = Quaternion.Euler(0, 0, rotZ);
	}

	public void OnWeaponChange(WeaponConfig weapon)
	{
		_weaponRenderer.sprite = weapon.WeaponSprite;
	}
}
