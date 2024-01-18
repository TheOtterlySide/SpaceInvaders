using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int points;
    public Sprite sprite;
    public int rowCount;
    private Rigidbody2D rb;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField] 
    private Vector2 bulletPos;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Contains("PlayerBullet"))
        {
            transform.parent.GetComponent<EnemyManager>().DeleteAlienFromList(gameObject);
            Destroy(gameObject);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (other.gameObject.CompareTag("Wall"))
        {
            transform.parent.GetComponent<EnemyManager>().CustomCollisionEnter(other);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        transform.parent.GetComponent<EnemyManager>().CustomCollisionExit(other);
    }
    
    public void Fire()
    {
        var position = transform.position;
        bulletPos = new Vector2(position.x, position.y - 0.5f);
        Instantiate(bulletPrefab,bulletPos, transform.rotation);
    }
}
