using UnityEngine;


/// <summary>
/// Предмет лечения
/// </summary>
public class PickupItemHeal : PickupItem
{
	[Header("Значение лечения")]
	[SerializeField]
	private float _healValue;

	protected override void OnPickedUp(GameObject character)
	{
		HealthSystem healthSystem = character.GetComponent<HealthSystem>();
		if (healthSystem == null)
			return;

		healthSystem.ChangeHealth(_healValue);
	}
}
