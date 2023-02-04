using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameManager;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour {
	public GameObject player;
	public bool isCreated = false;
	private bool isCameraFixed = false;

	private Vector3 cameraOffset = new Vector3(0.0f, 0.0f, -10.0f);
	private float smoothCoefficient = 0.15f;

	public CameraManager SetCameraFixed(bool _isCameraFixed) {
		isCameraFixed = _isCameraFixed;
		return this;
	}

	public bool GetCameraFixedState() {
		return isCameraFixed;
	}

	private void Start() {
		if (isCreated) return;
		isCreated = true;

		Scene scene = SceneManager.GetActiveScene();
		if (scene.name == "Root") {
			isCreated = true;
			isCameraFixed = true;
			return;
		}

		GameObject minimapCamera = Managers.Resource.Instantiate("MinimapCamera");
		if (!Managers.Sound.CheckBgmPlay(Managers.Sound.bgmSound.battleBgm)) {
			Managers.Sound.Play(Managers.Sound.bgmSound.battleBgm, Define.Sound.Bgm);
		}

		minimapCamera.AddComponent<CameraManager>();
		minimapCamera.GetComponent<CameraManager>().isCreated = true;
		minimapCamera.GetComponent<CameraManager>().player = player;
		minimapCamera.GetComponent<CameraManager>().isCameraFixed = false;
	}

	private void Update() {
		if (isCameraFixed)
			return;

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