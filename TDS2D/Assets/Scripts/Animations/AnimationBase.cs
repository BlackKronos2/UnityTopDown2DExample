using UnityEngine;


/// <summary>
/// Механика анимации персонажа
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public abstract class AnimationBase : MonoBehaviour
{
	protected Animator _animator;
	protected CharacterController _controller;

	protected virtual void Awake()
	{
		_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController>();
	}
}