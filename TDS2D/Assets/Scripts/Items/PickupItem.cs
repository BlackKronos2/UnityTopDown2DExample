using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Управляет логикой объекта, который можно подобрать
/// </summary>
public abstract class PickupItem : MonoBehaviour
{
	[Header("Нужно ли уничтожать объект после подбора")]
	[SerializeField]
	private bool _OnPickup = true;

	[Header("Слой объектов, которые могут подбирать этот предмет")]
	[SerializeField]
	private LayerMask _canBePickupBy;

	public UnityEvent OnPickup { get; private set; } = new UnityEvent();


	/// <summary>
	/// Предмет подбирается
	/// </summary>
	/// <param name="receiver"> GameObject, получающий эффект </param>
	protected virtual void OnPickedUp(GameObject receiver)
	{ }

	/// <summary>
	/// Уничтожает предмет, позволяя ему завершить свои эффекты.
	/// </summary>
	private void Item()
	{
		foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
		{
			if (!(component is AudioSource))
				component.enabled = false;
		}

		foreach (Renderer component in transform.GetComponentsInChildren<Renderer>())
		{
			if (!(component is ParticleSystemRenderer))
				component.enabled = false;
		}

		Destroy(gameObject, 5f);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (_canBePickupBy.value == (_canBePickupBy.value | (1 << other.gameObject.layer)))
		{
			OnPickedUp(other.gameObject);
			OnPickup.Invoke();
			if (_OnPickup)
				Item();

		}
	}
}
