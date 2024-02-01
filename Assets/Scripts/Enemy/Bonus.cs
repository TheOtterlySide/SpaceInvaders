using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] 
    private float deathTime;

    [SerializeField] 
    private int points;


    private GameManager _gm;
    private float _timeAlive;

    private void Start()
    {
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Contains("PlayerBullet"))
        {
            Death();
        }
    }

    private void Update()
    {
        _timeAlive += Time.deltaTime;

        if (_timeAlive >= deathTime)
        {
            Death();
        }
    }

    private void Death()
    {
        transform.parent.GetComponent<EnemyManager>().bonusAlive = false;
        _gm.score += points;
        Destroy(gameObject);
    }
}
