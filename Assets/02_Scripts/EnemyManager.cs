using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameManager;
using DG.Tweening;
using static PlayerControl;
using static EnemyManager;

public class EnemyManager : MonoBehaviour {
	public enum eEnemyType {
		EnemyMelee,
		EnemyRange,
		EnemyHappy,
		Box,
	};

	private GameObject player = null;
	private GameObject enemyHPBar = null;

	public eEnemyType enemyType;
	private float enemyHP = 100.0f;
	private float enemyMaxHP = 100.0f;
	private float enemyAttackCooldown = 1.2f;
	private float enemyAttackDamage = 1.0f;
	private float enemyMoveSpeed = 2.0f;
	private float enemyAttackLifespan = 3.0f;
	private bool isAttackCooldown = false;
	public bool isDead = false;

	private Coroutine EnemyMeleeAttackCoroutine = null;
	private bool isPlayerAttackable = false;

	public EnemyManager SetEnemyType(eEnemyType _enemyType) {
		enemyType = _enemyType;

		switch (enemyType) {
			case eEnemyType.EnemyMelee:
				SetEnemyMaxHP(100.0f);
				SetEnemyHP(enemyMaxHP);
				SetEnemyAttackDamage(1.0f);
				SetEnemyAttackCooldown(1.6f);
				SetEnemyMoveSpeed(2.0f);
				break;
			case eEnemyType.EnemyRange:
				SetEnemyMaxHP(50.0f);
				SetEnemyHP(enemyMaxHP);
				SetEnemyAttackDamage(1.0f);
				SetEnemyAttackCooldown(2.4f);
				SetEnemyMoveSpeed(1.6f);
				SetEnemyAttackLifespan(3.0f);
				break;
			case eEnemyType.EnemyHappy:
				SetEnemyMaxHP(75.0f);
				SetEnemyHP(enemyMaxHP);
				SetEnemyAttackDamage(1.0f);
				SetEnemyAttackCooldown(2.0f);
				SetEnemyMoveSpeed(2.0f);
				break;
			case eEnemyType.Box:
				SetEnemyMaxHP(1.0f);
				SetEnemyHP(1.0f);
				transform.Find("BoxAnimator").gameObject.SetActive(false);
				// transform.Find("BoxAnimator").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("SPUM/SPUM_Sprites/Items/6_Weapons/transparent");
				break;
		}

		return this;
	}

	public EnemyManager SetEnemyAttackCooldown(float _enemyAttackCooldown) {
		enemyAttackCooldown = _enemyAttackCooldown;
		return this;
	}

	public EnemyManager SetEnemyAttackDamage(float _enemyAttackDamage) {
		enemyAttackDamage = _enemyAttackDamage;
		return this;
	}

	public EnemyManager SetEnemyAttackLifespan(float _enemyAttackLifespan) {
		enemyAttackLifespan = _enemyAttackLifespan;
		return this;
	}

	public EnemyManager SetEnemyMoveSpeed(float _enemyMoveSpeed) {
		enemyMoveSpeed = _enemyMoveSpeed;
		return this;
	}

	public EnemyManager SetEnemyMaxHP(float _enemyMaxHP) {
		enemyMaxHP = _enemyMaxHP;
		return this;
	}

	public void SetEnemyHP(float _enemyHp) {
		Debug.Log("SetEnemyHP(" + _enemyHp + ")");

		enemyHP = _enemyHp;
		if (enemyHP > enemyMaxHP)
			enemyHP = enemyMaxHP;

		if (enemyHP <= 0)
			enemyHP = 0;

		if (enemyType != eEnemyType.Box) {
			enemyHPBar.GetComponent<Image>().fillAmount = enemyHP / enemyMaxHP;
		}

		if (enemyHP <= 0) {
			float destroyTime = 0.5f;

			if (enemyType == eEnemyType.Box) {
				destroyTime = 0.7f;
				GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("SPUM/SPUM_Sprites/Items/6_Weapons/transparent");

				transform.Find("BoxAnimator").gameObject.SetActive(true);
				transform.Find("BoxAnimator").GetComponent<Animator>().SetTrigger("Destroy");
			}

			Destroy(GetComponent<Rigidbody2D>());
			Destroy(GetComponent<BoxCollider2D>());
			isDead = true;

			transform.GetComponent<SpriteRenderer>().material.DOColor(Color.black, destroyTime).OnComplete(() => {
				Destroy(gameObject);
				if(!gameObject.name.Contains("box"))
					Managers.GM.MonsterCount -= 1;
				print(Managers.GM.MonsterCount);
			});
		}
	}

	private void Start() {
		player = ItemManager.Find("Player");

		if (enemyType != eEnemyType.Box) {
			enemyHPBar = transform.Find("Canvas").Find("barHP").gameObject;
		}
		SetEnemyType(enemyType);
	}
	public enum eEnemyDirection {
		DirectionLeft,
		DirectionRight
	};

	public eEnemyDirection enemyDirection = eEnemyDirection.DirectionLeft;

	void UpdateDirection(eEnemyDirection updateDirection) {
		if (enemyDirection == updateDirection)
			return;

		transform.Rotate(0, 180.0f, 0);
		
		if (enemyType != eEnemyType.Box) {
			enemyHPBar.transform.Rotate(0, 180.0f, 0);
		}

		enemyDirection = updateDirection;
	}
	private void Update() {
		if (player == null) {
			player = ItemManager.Find("Player");
			return;
		}

		if (isDead)
			return;

		Vector2 dist = player.transform.position - transform.position;

		if (dist.x > 0) {
			UpdateDirection(eEnemyDirection.DirectionRight);
		}
		else {
			UpdateDirection(eEnemyDirection.DirectionLeft);
		}

		dist.x = -Mathf.Abs(dist.x);

		switch (enemyType) {
			case eEnemyType.EnemyMelee:
				if (dist.magnitude <= 12.0f) {
					transform.Translate(dist.normalized * enemyMoveSpeed * Time.deltaTime);
				}

				/*
				 * 근접 유형 적이 Player와 적당히 가까워졌을 경우
				 * 이 부분은 CircleCollider2D에 의한 Trigger로 구현되었다.
				 */
				//if (dist.magnitude <= 1.0f) {
				//	// EnemyAttack();
				//	return;
				//}
				break;
			case eEnemyType.EnemyRange:
				if (dist.magnitude <= 15.0f) {
					EnemyAttack();
					return;
				}
				break;
			case eEnemyType.EnemyHappy:
				if (dist.magnitude >= 2.5f && dist.magnitude <= 7.5f) {
					transform.Translate(dist.normalized * enemyMoveSpeed * Time.deltaTime);
				}
				else if (dist.magnitude <= 3.0f) {
					const float happyRadius = 2.5f;
					float theta = Mathf.Atan2(-dist.y, -dist.x) * Mathf.Rad2Deg;
					theta += enemyMoveSpeed * Time.deltaTime;

					transform.position = new Vector3(player.transform.position.x + Mathf.Cos(theta) * happyRadius, player.transform.position.y + Mathf.Sin(theta) * happyRadius, player.transform.position.z);
				}
				break;
		}
	}

	private void PrintVector2(string vectorName, Vector2 vector) {
		Debug.Log("[PrintVector2] " + vectorName + "(" + vector.x + ", " + vector.y + ")");
	}

	private void EnemyAttack() {
		if (ItemManager.Find("EnemyRangeAttackProjectile") == null)
			return;

		if (isDead || isAttackCooldown)
			return;

		const float enemyProjectileSpeed = 4.0f;

		switch (enemyType) {
			case eEnemyType.EnemyMelee:
				PlayerControl playerControl = player.GetComponent<PlayerControl>();
				if (playerControl.GetPlayerSuperArmorState())
					return;

				playerControl.SetPlayerHP(playerControl.GetPlayerHP() - enemyAttackDamage);
				break;
			case eEnemyType.EnemyRange:
				Vector2 dist = (player.transform.position - transform.position).normalized;

				GameObject enemyProjectile = Instantiate(ItemManager.Find("EnemyRangeAttackProjectile"), transform.position, Quaternion.identity);

				enemyProjectile.GetComponent<Rigidbody2D>().velocity = dist * enemyProjectileSpeed;

				EnemyAttackManager enemyAttackManager = enemyProjectile.GetComponent<EnemyAttackManager>();

				float rotateAngle = Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg;
				Quaternion angleAxis = Quaternion.AngleAxis(rotateAngle - 90.0f, Vector3.forward);
				Quaternion rotation = Quaternion.Slerp(enemyProjectile.transform.rotation, angleAxis, 10000 * Time.deltaTime);
				enemyProjectile.transform.rotation = rotation;

				enemyAttackManager.SetLifespan(enemyAttackLifespan);
				enemyAttackManager.SetDamage(enemyAttackDamage);
				enemyAttackManager.SetValid(true);
				break;
			case eEnemyType.EnemyHappy:
				break;
		}

		isAttackCooldown = true;
		StartCoroutine(EnemyAttackCooldownProcess(enemyAttackCooldown));
	}

	private IEnumerator EnemyAttackCooldownProcess(float attackCooldown) {
		yield return new WaitForSeconds(attackCooldown);
		isAttackCooldown = false;
	}

	public float GetEnemyAttackCooldown() {
		return enemyAttackCooldown;
	}

	public float GetEnemyAttackDamage() {
		return enemyAttackDamage;
	}

	public float GetEnemyHP() {
		return enemyHP;
	}

	public float GetEnemyMaxHP() {
		return enemyMaxHP;
	}

	public float GetEnemyAttackLifespan() {
		return enemyAttackLifespan;
	}

	public bool GetEnemyDeadState() {
		return isDead;
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (isDead)
			return;

		if (collision.tag != "Player") {
			return;
		}

		isPlayerAttackable = true;
		EnemyMeleeAttackCoroutine = StartCoroutine(EnemyMeleeAttackProcess());
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (isDead)
			return;

		if (collision.tag != "Player") {
			return;
		}

		if (!isPlayerAttackable)
			return;

		isPlayerAttackable = false;
		StopCoroutine(EnemyMeleeAttackCoroutine);
		EnemyMeleeAttackCoroutine = null;
	}

	private IEnumerator EnemyMeleeAttackProcess() {
		float elapsedTime = 0.0f;
		while (elapsedTime <= 0.5f) {
			if (!isPlayerAttackable)
				yield break;

			elapsedTime += Time.deltaTime;
			yield return null;
		}

		EnemyAttack();
	}
}