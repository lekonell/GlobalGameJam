using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    GameObject normalTilemap;
    GameObject battleTilemap;

    // Start is called before the first frame update
    void Start()
    {
        normalTilemap = transform.Find("TilemapNormalWall").gameObject;
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
            normalTilemap.SetActive(false);
            battleTilemap.SetActive(true);
        }
        else
        {
            normalTilemap.SetActive(true);
            battleTilemap.SetActive(false);
        }
    }

}
