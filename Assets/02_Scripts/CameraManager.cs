using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameManager;
using System.Runtime.CompilerServices;

public class CameraManager : MonoBehaviour {
	private GameObject player = null;
	private Camera cameraObject;
	private Vector3 cameraOffset = new Vector3(0.0f, 0.0f, -10.0f);
	private float smoothCoefficient = 0.15f;

	private void Awake() {
		cameraObject = GetComponent<Camera>();
	}

	private void Start() {
		GameManager.GameManager.Init();
		player = ItemManager.Find("Player");
	}

	private void Update() {
		if (player == null) {
			player = ItemManager.Find("Player");
			return;
		}

		MoveCamera(player.transform.position);
	}

	public void MoveCamera(Vector3 position) {
		transform.position = position + cameraOffset;
	}

	public void MoveCameraLerp(Vector3 position) {
		MoveCameraLerp(position + cameraOffset, smoothCoefficient);
	}

	public void MoveCameraLerp(Vector3 position, float smoothCoefficient) {
		transform.position = Vector3.Lerp(transform.position, position + cameraOffset, smoothCoefficient);
	}
}