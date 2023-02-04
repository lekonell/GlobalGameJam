using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameManager;
using DG.Tweening;


public class Enemy_gun : MonoBehaviour {
	public GameObject Bullet;
	public Transform FirePos;
	private GameObject barHP;
	public int del = 0;
	public float HP;
	public static float speed;
	public static float AD;
	public float maxHP;
	public int nextmove;
	public bool isDead = false;
	Animation anim;
	Animator animator;
	Rigidbody2D rigid;

	private void Awake() {
		animator = GetComponent<Animator>();
	}

	void Start() 
	{
		HP = 100;
		maxHP = 100;
		rigid = GetComponent<Rigidbody2D>();
		speed = 3;
		AD = 10.0f;
		barHP = gameObject.transform.Find("Canvas/barHP").gameObject;
	}
	void Update() {
		if (del == 0) {
			Instantiate(Bullet, FirePos.transform.position, FirePos.transform.rotation);
			del = 1;
			print("발싸");
			animator.SetBool("NewBool", true);

		}

		else if (del == 1) {
			del = 2;
			StartCoroutine(RE());
		}
	}
	private IEnumerator RE() {
		yield return new WaitForSeconds(3.0f);
		del = 0;
	
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