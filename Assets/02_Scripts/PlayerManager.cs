using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameManager;
using Unity.VisualScripting;

public class PlayerManager : MonoBehaviour {
	public enum ePlayerAttackType {
		AttackTypeMelee,
		AttackTypeRange
	};

	private float playerBaseAttackCooldown = 1.0f;
	private bool isPlayerBaseAttackCooldown = false;

	void Start() {

	}

	void Update() {
		/*
		 * GetMouseButton(0): 마우스 왼쪽 클릭
		 * GetMouseButton(1): 마우스 오른쪽 클릭
		 * GetMouseButton(2): 마우스 휠 클릭
		 * GetMouseButton(3-6): 마우스 추가 버튼 클릭
		 */
		if (Input.GetMouseButton(0)) {
			PlayerBaseAttack(ePlayerAttackType.AttackTypeMelee);
			return;
		}
		else if (Input.GetMouseButton(1)) {
			PlayerBaseAttack(ePlayerAttackType.AttackTypeRange);
			return;
		}
	}

	private void PrintVector2(string vectorName, Vector2 vector) {
		Debug.Log("[PrintVector2] " + vectorName + "(" + vector.x + ", " + vector.y + ")");
	}
	private void PrintVector3(string vectorName, Vector3 vector) {
		Debug.Log("[PrintVector3] " + vectorName + "(" + vector.x + ", " + vector.y + ", " + vector.z + ")");
	}

	private void PlayerBaseAttack(ePlayerAttackType playerAttackType) {
		if (GameManager.GameManager.isPaused || GameManager.GameManager.isPausingOverlay)
			return;
		
		if (isPlayerBaseAttackCooldown)
			return;


		switch (playerAttackType) {
			case ePlayerAttackType.AttackTypeMelee:
				break;
			case ePlayerAttackType.AttackTypeRange:
				GameObject playerProjectile = Instantiate(ItemManager.Find("PlayerBaseAttackProjectile"), transform.position, Quaternion.identity);

				playerProjectile.GetComponent<PlayerAttackManager>().SetLifespan(1.2f).SetValid(true).SetDamage(10);

				Vector2 playerPosition = transform.position;
				Vector2 inputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

				Vector2 dist = (inputPosition - playerPosition).normalized;

				playerProjectile.GetComponent<Rigidbody2D>().velocity = dist * 10.0f;

				isPlayerBaseAttackCooldown = true;
				StartCoroutine(PlayerBaseAttackCooldownProcess(playerBaseAttackCooldown));
				break;
		}
	}

	private IEnumerator PlayerBaseAttackCooldownProcess(float cooldown) {
		yield return new WaitForSeconds(cooldown);
		isPlayerBaseAttackCooldown = false;
	}
}
