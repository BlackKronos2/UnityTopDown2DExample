using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


/// <summary>
/// UI для индикатора здоровья
/// </summary>
public class EntityHealthBarUpdater : MonoBehaviour
{
	#region UI_FIELDS

	[SerializeField]
	private Slider _healthSlider;
	[SerializeField]
	private UnityEvent _onHealthUpdate;

	#endregion

	[SerializeField]
	private GameObject _entityObject;

	private HealthSystem _entityHealth;

	private void Awake()
	{
		_entityHealth = _entityObject.GetComponent<HealthSystem>();
	}

	private void Start()
	{
		if (_entityHealth.OnDamage == null)
			Debug.Log("ONDAMAGENULL");

		_entityHealth.OnDamage.AddListener(UpdateHealth);
		_entityHealth.OnHeal.AddListener(UpdateHealth);
	}

	/// <summary>
	/// Обновление статуса здоровья
	/// </summary>
	private void UpdateHealth()
	{
		_healthSlider.value = _entityHealth.CurrentHealth / _entityHealth.MaxHealth;
		_onHealthUpdate.Invoke();
	}
}
