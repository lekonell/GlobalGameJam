using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    GameObject battleTilemap;

    // Start is called before the first frame update
    void Start()
    {
        battleTilemap = transform.Find("TilemapBattleWall").gameObject;

        SetTilemap();
    }

    // Update is called once per frame
    void Update()
    {
        SetTilemap();
    }

    public void SetTilemap()
    {
        if (Managers.GM.isFight)
        {
            battleTilemap.SetActive(true);
        }
        else
        {
            battleTilemap.SetActive(false);
        }
    }

}
