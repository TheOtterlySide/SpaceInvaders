using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed;
    private Vector2 bulletPos;
    private Rigidbody2D _rb;
    [SerializeField]private float lerpSpeed;
    [SerializeField] float timeAlive;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        DecideBulletDirection();
        Destroy(transform.gameObject, timeAlive);
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = Vector2.Lerp(_rb.velocity, bulletPos * speed, lerpSpeed * Time.deltaTime);
    }

    void DecideBulletDirection()
    {
        bulletPos = gameObject.CompareTag("EnemyBullet") ? Vector2.down : Vector2.up;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && gameObject.CompareTag("PlayerBullet"))
        {
            //Player shoots Enemy
            Destroy(gameObject);
        }

        if (other.CompareTag("Player") && gameObject.CompareTag("EnemyBullet"))
        {
            //Enemy shoots player
            Destroy(gameObject);
        }

        if (other.CompareTag("Bunker"))
        {
            Destroy(gameObject);
        }
    }


}
