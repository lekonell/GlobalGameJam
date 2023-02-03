using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

public class bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public float speed;

    void Start() 
    {
        target = ItemManager.Find("Player").gameObject;
    }

    // Update is called once per frame
    void Update() {
        Vector3 dir = (target.transform.position - transform.position).normalized;
        transform.position += dir * speed;
        
    }
    private void OnCollisionEnter2D() 
    {
        print("dd");
       Destroy(this.gameObject); 
	}


}
