using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int points;
    public int rowCount;
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField] 
    private Vector2 bulletPos;
    private GameManager _gameManager;
    [SerializeField] private Animator Animator;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Animator.SetBool("Death", false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PlayerBullet"))
        {
            Animator.SetTrigger("Death");
            _boxCollider.enabled = false;
            transform.parent.GetComponent<EnemyManager>().DeleteAlienFromList(gameObject);
            _gameManager.score += points;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        
        if (other.gameObject.CompareTag("Wall"))
        {
            transform.parent.GetComponent<EnemyManager>().CustomCollisionEnter(other);
        }
        
        if (other.gameObject.CompareTag("Bunker"))
        {
            _gameManager.GameOver();
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

    public void DestroyGameobject()
    {
        Destroy(gameObject);
    }
}
