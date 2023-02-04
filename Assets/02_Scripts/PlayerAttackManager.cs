using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameManager;

public class PlayerAttackManager : MonoBehaviour {
	private bool isValid;
	private float projectileDamage;
	private float projectileLifespan;
	private Vector2 projectileDirection;
	private float projectileSpeed;

	public PlayerAttackManager SetValid(bool _isValid) {
		isValid = _isValid;
		if (isValid)
			StartCoroutine(PlayerAttackLifespanProcess());
		
		return this;
	}

	public PlayerAttackManager SetDamage(float _projectileDamage) {
		projectileDamage = _projectileDamage;
		return this;
	}

	public PlayerAttackManager SetLifespan(float _projectileLifespan) {
		projectileLifespan = _projectileLifespan;
		return this;
    }

    public PlayerAttackManager SetSpeed(float _projectileSpeed)
    {
        projectileSpeed = _projectileSpeed;
        return this;
    }

    public PlayerAttackManager SetDirection(Vector2 _projectileDirection)
	{
		projectileDirection = _projectileDirection;
		return this;
	}

    private void Update()
    {
		transform.Translate(projectileDirection * projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
		if (!isValid || collision.tag != "Enemy")
			return;

		GameObject enemyObject = collision.gameObject;
		// EnemyManager enemyManager = enemyObject.GetComponent<EnemyManager>();
		//if (enemyManager.isDead)
		//	return;

		isValid = false;
		// enemyManager.UpdateHP(enemyManager.currentHP - damage);

		PlayerAttackDestroy();
	}

	private IEnumerator PlayerAttackLifespanProcess() {
		yield return new WaitForSeconds(projectileLifespan);
		PlayerAttackDestroy();
	}

	private void PlayerAttackDestroy() {
		Destroy(gameObject);
		// gameObject.SetActive(false);
	}
}
