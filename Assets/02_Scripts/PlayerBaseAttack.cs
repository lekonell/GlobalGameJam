using GameManager;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBaseAttack : MonoBehaviour {
	private GameObject player;
	private PlayerControl playerControl;
	private PlayerWeaponManager playerWeaponManager;
	public List<Collider2D> collisionEnemies;

	private static bool isValid = false;

	private void Start() {
		player = ItemManager.Find("Player");
		playerControl = player.GetComponent<PlayerControl>();
		playerWeaponManager = player.GetComponent<PlayerWeaponManager>();
		collisionEnemies = new List<Collider2D>();

		isValid = false;
	}

	public PlayerBaseAttack SetValid(bool _isValid) {
		isValid = _isValid;

		if (isValid) {
			StartCoroutine(BaseAttackValidationProcess());
		}

		return this;
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag != "Enemy")
			return;

		if (!collisionEnemies.Contains(collision))
			collisionEnemies.Add(collision);
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collisionEnemies.Contains(collision))
			collisionEnemies.Remove(collision);
	}

	private void FixedUpdate() {
		if (!isValid || collisionEnemies.Count == 0)
			return;

		int randIdx = Random.Range(0, collisionEnemies.Count);
		Collider2D collision = collisionEnemies[randIdx];
		GameObject enemy = collision.gameObject;
		Enemy enemyControl = enemy.GetComponent<Enemy>();
		if (enemyControl.isDead)
			return;

		isValid = false;

		enemyControl.UpdateHP(enemyControl.HP - playerWeaponManager.GetWeaponDamage());
	}

	private IEnumerator BaseAttackValidationProcess() {
		/*
		 * BaseAttack이 0.4s동안 실행되므로,
		 * 앞 0.2s에 대해 공격이 Valid하지 않도록 처리해 모션과 공격타이밍이 일치하도록 한다.
		 */
		isValid = false;
		yield return new WaitForSeconds(0.2f);
		isValid = true;
		yield return new WaitForSeconds(0.2f);
		isValid = false;
	}
}