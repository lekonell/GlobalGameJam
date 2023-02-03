using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameManager;
using DG.Tweening.Core.Easing;

public class CameraManager : MonoBehaviour {
	private Camera cameraObject;
	private Vector3 cameraOffset = new Vector3(0.0f, 0.0f, -10.0f);
	private float smoothCoefficient = 0.15f;

	private void Start() {
		GameManager.GameManager.Init();
	}

	private void Awake() {
		cameraObject = GetComponent<Camera>();
	}

	public void MoveCamera(Vector3 position) {
		transform.position = position;
	}

	public void MoveCameraLerp(Vector3 position) {
		MoveCameraLerp(position, smoothCoefficient);
	}

	public void MoveCameraLerp(Vector3 position, float smoothCoefficient) {
		transform.position = Vector3.Lerp(transform.position, position + cameraOffset, smoothCoefficient);
	}
}