using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BattleFieldController : MonoBehaviour
{
    public List<GameObject> enemys = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Collider2D[] colls = Physics2D.OverlapBoxAll(transform.position, GetComponent<Collider2D>().bounds.size,0,1<<6);
        var goList = (from coll in colls
                 select coll.gameObject).Distinct().ToList();

        foreach (var item in goList)
        {
            enemys.Add(item.gameObject);
            item.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "TempPlayer" || collision.gameObject.name == "UnitRoot")
        {
            Managers.GM.isFight = true;
            Managers.GM.MonsterCount = enemys.Count;
            foreach (var item in enemys)
            {
                item.gameObject.SetActive(true);
            }
            Destroy(gameObject);
        }
    }
}
