using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Список всех сущностей на уровне
/// </summary>
public class LevelEntityList : MonoBehaviour
{
	#region SINGLETON

	private static LevelEntityList _instance;
	public static LevelEntityList Instance
	{
		get { return _instance; }
		set
		{ 
			_instance = value;
			_instance.Init();
		}
	}

	#endregion

	[SerializeField]
	private List<CharacterController> _enemiesList;
	[SerializeField]
	private List<PlayerController> _playersList;

	private void Awake()
	{
		Instance = Instance ?? this;
	}

	private void Init()
	{ 
		_enemiesList = new List<CharacterController>(0);
		_playersList = new List<PlayerController>(0);
	}

	#region ENEMY

	public void AddEnemy(CharacterController enemy)
	{
		_enemiesList.Add(enemy);	
	}
	public void RemoveEnemy(CharacterController enemy)
	{
		if (_enemiesList.Contains(enemy))
			_enemiesList.Remove(enemy);
	}
	public PlayerController FindPlayer(Vector2 enemyPosition)
	{
		PlayerController player = _playersList.OrderBy(player => Vector2.Distance(player.transform.position, enemyPosition)).FirstOrDefault();
		return player;
	}

	#endregion

	#region PLAYER 

	public void AddPlayer(PlayerController player)
	{ 
		_playersList.Add(player);
	}
	public void RemovePlayer(PlayerController player)
	{ 
		if(_playersList.Contains(player))
			_playersList.Remove(player);
	}

	public CharacterController FindClosestEnemy(Vector2 playerPosition)
	{
		CharacterController closestEnemy = _enemiesList.OrderBy(enemy => Vector2.Distance(enemy.transform.position, playerPosition)).FirstOrDefault();
		return closestEnemy;
	}

	#endregion
}
