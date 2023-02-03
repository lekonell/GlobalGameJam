using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_gun : MonoBehaviour {
	public GameObject Bullet;
	public Transform FirePos;
	public int del = 0;
	public static float HP;
	public static float speed;
	public static float AD;
	public int nextmove;
	Rigidbody2D rigid;
	void Start() 
	{
		HP = 100;
		rigid = GetComponent<Rigidbody2D>();
		speed = 3;
		AD = 10.0f;
	}
	void Update() {
		if (del == 0) {
			Instantiate(Bullet, FirePos.transform.position, FirePos.transform.rotation);
			del = 1;
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
}