using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWallTrigger : MonoBehaviour
{
    public CMonster boss;

    /*private void Update()
    {
        if(boss.Current_Health <= 0.0f)
        {
            CGameManager.instance.bossWall1.SetActive(false);
            CGameManager.instance.bossWall2.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            CGameManager.instance.bossWall1.SetActive(true);
            CGameManager.instance.bossWall2.SetActive(true);
        }
    }*/

    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 9)
        {
            CGameManager.instance.bossWall1.SetActive(true);
            CGameManager.instance.bossWall2.SetActive(true);
        }
    }*/

}
