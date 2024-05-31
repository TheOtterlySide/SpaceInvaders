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

    [SerializeField] private Animator Animator;
    private GameManager _gm;
    private float timeAlive;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
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
        timeAlive += Time.deltaTime;

        if (timeAlive >= deathTime)
        {
            DestroyGameObject();
        }
    }

    private void Death()
    {
        Animator.SetTrigger("Death");
        _rb.velocity = Vector2.zero;
        transform.parent.GetComponent<EnemyManager>().bonusAlive = false;
        _gm.score += points;
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
