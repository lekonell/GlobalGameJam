using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potal : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Managers.GM.gold += col.GetComponent<PlayerControl>().GetPlayerGold();
            Managers.GM.OnPortal();
        }
    }
}