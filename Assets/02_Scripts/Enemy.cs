using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

public class Enemy : MonoBehaviour {
	Rigidbody2D rigid;
	private Transform target;
	public static float HP;
	public static float speed;
	public static float AD;
	public int nextmove;

	void Start() {
		HP = 100;
		rigid = GetComponent<Rigidbody2D>();
		speed = 3;
		AD = 10.0f;
		Invoke("Think", 5);

		target = ItemManager.Find("Player").transform;
		Debug.Log("target: " + target.name);
	}

	void Update() {
		Vector2 dist = target.position - transform.position;

		if (dist.magnitude <= 10) {
			Move();
		}
		else {
			Rendom_Move();
		}
	}

	void Move() {
		Vector2 dist = (target.position - transform.position).normalized;
		transform.Translate(dist * Enemy.speed * Time.deltaTime);
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
		print("다");
	}
}