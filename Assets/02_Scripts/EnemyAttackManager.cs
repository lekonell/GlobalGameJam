using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameManager;
using DG.Tweening;

public class EnemyAttackManager : MonoBehaviour {
	private float projectileDamage;
	private float projectileLifespan;
	private Vector2 projectileDirection;
	private float projectileSpeed;

	private bool isValid;

	public EnemyAttackManager SetValid(bool _isValid) {
		isValid = _isValid;
		if (isValid)
			StartCoroutine(EnemyAttackLifespanProcess());

		return this;
	}

	public EnemyAttackManager SetDamage(float _projectileDamage) {
		projectileDamage = _projectileDamage;
		return this;
	}

	public EnemyAttackManager SetLifespan(float _projectileLifespan) {
		projectileLifespan = _projectileLifespan;
		return this;
	}

	public EnemyAttackManager SetSpeed(float _projectileSpeed) {
		projectileSpeed = _projectileSpeed;
		return this;
	}

	public EnemyAttackManager SetDirection(float _x, float _y) {
		projectileDirection.x = _x;
		projectileDirection.y = _y;
		return this;
	}

	private void PrintVector2(string vectorName, Vector2 vector) {
		Debug.Log("[PrintVector2] " + vectorName + "(" + vector.x + ", " + vector.y + ")");
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (!isValid || collision.tag != "Player")
			return;

		GameObject playerObject = collision.gameObject;
		PlayerControl playerControl = playerObject.GetComponent<PlayerControl>();

		if (playerControl.GetPlayerSuperArmorState())
			return;

		isValid = false;
		playerControl.SetPlayerHP(playerControl.GetPlayerHP() - projectileDamage);

		Destroy(gameObject);
		// gameObject.SetActive(false);
	}

	private IEnumerator EnemyAttackLifespanProcess() {
		yield return new WaitForSeconds(projectileLifespan);
		Destroy(gameObject);
	}
}