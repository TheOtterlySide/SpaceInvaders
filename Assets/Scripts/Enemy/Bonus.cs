using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] 
    private float deathTime;
    
    private float timeAlive;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Contains("PlayerBullet"))
        {
            Death();
        }
    }

    private void Update()
    {
        timeAlive += Time.deltaTime;

        if (timeAlive >= deathTime)
        {
            Death();
        }
    }

    private void Death()
    {
        transform.parent.GetComponent<EnemyManager>().bonusAlive = false;
        Destroy(gameObject);
    }
}
