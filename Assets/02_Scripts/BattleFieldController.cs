using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFieldController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("A");
        if(collision.gameObject.name == "TempPlayer")
        {
            Managers.GM.isFight = true;
            Destroy(gameObject);
        }   
    }
}
