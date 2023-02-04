using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameManager;
using DG.Tweening;

public class Enemy : MonoBehaviour {
	private Rigidbody2D rigid;
	private GameObject target;
	private GameObject barHP;

	public float HP;
	public float maxHP;
	public float enemySpeed;
	public float AD;
	public int nextmove;

	public bool isDead = false;

	void Start() {
		HP = 100;
		maxHP = 100;

		rigid = GetComponent<Rigidbody2D>();
		enemySpeed = 3;
		AD = 10.0f;
		Invoke("Think", 5);
		target = ItemManager.Find("Player");
		barHP = gameObject.transform.Find("Canvas/barHP").gameObject;
	}

	void Update() {
		float dis = Vector3.Distance(transform.position, target.transform.position);
		if (dis <= 10) {
			Move();
		}
		else {
			Rendom_Move();
		}
	}

	void Move() {
		Vector2 dist = (target.transform.position - transform.position).normalized;
		transform.Translate(dist * enemySpeed * Time.deltaTime);
	}

	void Rendom_Move() {
		rigid.velocity = new Vector2(nextmove, rigid.velocity.y);
	}

	void Think() {
		nextmove = Random.Range(-1, 2);
		float nextThinkTime = Random.Range(2f, 5f);
		Invoke("Think", nextThinkTime);
	}

	private void OnCollisionEnter2D() {
		//
	}

	public void UpdateHP(float newHP) {
		HP = newHP;

		if (HP >= maxHP)
			HP = maxHP;

		if (HP < 0)
			HP = 0;

		barHP.GetComponent<Image>().fillAmount = HP / maxHP;

		if (HP <= 0) {
			isDead = true;

			gameObject.GetComponent<SpriteRenderer>().material.DOColor(Color.black, 0.8f).OnComplete(() => {
				// Destroy(gameObject);
				gameObject.SetActive(false);
			});
		}
	}
}