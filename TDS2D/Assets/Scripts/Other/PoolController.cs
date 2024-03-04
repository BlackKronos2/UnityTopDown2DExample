using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Упралнение пулом объектов
/// </summary>
public class PoolController : MonoBehaviour
{
	public static PoolController Instance { get; private set; }

	[SerializeField] 
	private List<GameObject> _pooledObjects; //список объектов
	[SerializeField] 
	private GameObject _objectToPool;
	[SerializeField] 
	private int _objectsCount;

	private void Awake()
	{
		Instance = Instance ?? this;
	}

	private void Start()
	{
		PoolCreate();
	}

	/// <summary>
	/// Создание пула объектов
	/// </summary>
	private void PoolCreate()
	{
		_pooledObjects = new List<GameObject>();
		for (int i = 0; i < _objectsCount; i++)
		{
			GameObject @object = Instantiate(_objectToPool);
			@object.SetActive(false);
			_pooledObjects.Add(@object);
		}
	}

	/// <summary>
	/// Получить объект из списка неактивных
	/// </summary>
	public GameObject GetPooledObject()
	{
		for (int i = 0; i < _objectsCount; i++)
		{
			if (!_pooledObjects[i].activeInHierarchy)
				return _pooledObjects[i];

		}

		return null;
	}
}
