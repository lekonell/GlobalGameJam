using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public float speed;

    void Start() 
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        Vector3 dir = (target.transform.position - this.transform.position).normalized;
        transform.position += dir * speed;
        
    }
    private void OnCollisionEnter2D() 
    {
        print("dd");
       Destroy(this.gameObject); 
	}


}
