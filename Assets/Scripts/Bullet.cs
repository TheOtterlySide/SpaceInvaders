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
        bulletPos = transform.rotation.y > 0 ? Vector2.down : Vector2.up;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("Enemy"))
        {
            Destroy(transform.gameObject);
        }
    }
}
