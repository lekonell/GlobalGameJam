using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potal : MonoBehaviour {
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            print("dd");
            Managers.GM.OnPortal();


        }
    }
}

