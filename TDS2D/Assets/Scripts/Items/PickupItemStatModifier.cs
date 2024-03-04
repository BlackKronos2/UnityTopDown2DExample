using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Предмет модификатор показателей персонажа
/// </summary>
public class PickupItemStatModifier : PickupItem
{
	[Header("Модификации")]
	[SerializeField]
	private List<StatsModifier> _statsModifier;


	protected override void OnPickedUp(GameObject character)
	{
		CharacterStatsController statsHandler = character.gameObject.GetComponent<CharacterStatsController>();
		foreach (StatsModifier stat in _statsModifier)
		{
			statsHandler.StatsModifiers.Add(stat);
		}

	}
}
