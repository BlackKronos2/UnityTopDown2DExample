using UnityEngine;


/// <summary>
/// Удаление сущности после смерти
/// </summary>
public class DisappearOnDeath : MonoBehaviour
{
	public void OnDeath()
	{
		foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
		{
			if (!(component is AudioSource) && !(component is TopDownFx))
			{
				component.enabled = false;
			}
		}

		foreach (Renderer component in transform.GetComponentsInChildren<Renderer>())
		{
			if (!(component is ParticleSystemRenderer))
			{
				component.enabled = false;
			}
		}

		Destroy(gameObject, 15f);
	}
}
