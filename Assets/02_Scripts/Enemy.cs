using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rigid;
    public Transform target;
	public static float HP;
    public static float speed;
    public static float AD;
    public int nextmove;
   
 
 
   
    void Start()
    {

        HP = 100;
        rigid = GetComponent<Rigidbody2D>();
        speed = 3;
        AD = 10.0f;
        Invoke("Think",5);

    }
    
    void Update()
    {
        float dis = Vector3.Distance(transform.position, target.position);
        if (dis <= 10)
        {
            Move();
        }
        else
        {
            Rendom_Move();
        }


    }

    void Move()
    {
        float dir = target.position.x - transform.position.x; 
        dir = (dir < 0) ? -1 : 1; 
        transform.Translate(new Vector2(dir, 0) * Enemy.speed * Time.deltaTime);
    }
    void Rendom_Move()
    {
        rigid.velocity = new Vector2(nextmove,rigid.velocity.y);
    }
    void Think()
    {
        nextmove = Random.Range(-1,2);
        float nextThinkTime = Random.Range(2f,5f);
        Invoke("Think",nextThinkTime);
    }
    private void OnCollisionEnter2D() 
    {
        print("다");
	}
}