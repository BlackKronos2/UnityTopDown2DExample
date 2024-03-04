using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Перемещение камеры
/// </summary>
public class LevelCameraController : MonoBehaviour
{
	[SerializeField]
	[Header("Цели для камеры")]
	private List<Transform> _targets;

	[SerializeField]
	[Header("Скорость движения")]
	private float _followSpeed = 3f;
	[SerializeField]
	[Header("MinZoom")]
	private float _minZoom = 5f;
	[SerializeField]
	[Header("MaxZoom")]
	private float _maxZoom = 8f;
	[SerializeField]
	[Header("Скорость Zoom")]
	private float _zoomLerpSpeed = 3f;

	private Camera _cam;
	private Vector3 _targetPosition;

	private void Start()
	{
		_cam = Camera.main;
	}

	private void Update()
	{
		if (_targets.Count == 0)
			return;

		CalculateAveragePosition();
		ZoomCamera();
		MoveCamera();
	}

	private void CalculateAveragePosition()
	{
		Vector3 averagePos = Vector3.zero;

		foreach (Transform Target in _targets)
		{
			averagePos += Target.position;
		}

		averagePos /= _targets.Count;
		_targetPosition = averagePos;
	}

	private void ZoomCamera()
	{
		float distance = 0f;

		foreach (Transform Target in _targets)
		{
			distance = Mathf.Max(distance, Vector3.Distance(Target.position, _targetPosition));
		}

		float targetZoom = Mathf.Lerp(_maxZoom, _minZoom, distance / 10f); // Adjust the divisor for zoom sensitivity
		_cam.orthographicSize = Mathf.Lerp(_cam.orthographicSize, targetZoom, Time.deltaTime * _zoomLerpSpeed);
	}

	private void MoveCamera()
	{
		Vector3 desiredPosition = new Vector3(_targetPosition.x, _targetPosition.y, transform.position.z);
		transform.position = Vector3.Lerp(transform.position, desiredPosition, _followSpeed * Time.deltaTime);
	}
}
