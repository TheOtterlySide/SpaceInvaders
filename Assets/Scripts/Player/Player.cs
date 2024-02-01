using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Position")]

    #region Position

    [SerializeField]
    private float playerPosX;

    [SerializeField] 
    private float playerPosY;
    [SerializeField] 
    private Vector2 playerPos;
    [SerializeField] 
    private float speed;
    [SerializeField] 
    private float lerpSpeed;

    #endregion

    [Header("Bullet")]

    #region Bullet

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField] 
    private GameObject bulletPos;

    #endregion

    [Header("Life & Damage")]

    #region Life&Damage

    [SerializeField] 
    public int playerLife;

    public bool playerAlive;
    
    [SerializeField] 
    private GameManager _gm;

    #endregion

    private PlayerControls _controls;
    private Rigidbody2D _rb;
    

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        playerPosX = 0;
        playerPosY = 0;
        playerPos = new Vector2(playerPosX, playerPosY);
        _controls = new PlayerControls();
        _controls.Enable();
        playerAlive = true;
        _controls.Player.Move.performed += moving => { playerPos.x = moving.ReadValue<float>(); };
        _controls.Player.Pause.performed += _ => Pause();
        _controls.Player.Fire.performed += _ => Fire();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdatePosition();
    }

    void Pause()
    {
        _gm.Pause();
    }

    void UpdatePosition()
    {
        _rb.velocity = Vector2.Lerp(_rb.velocity, playerPos * speed, lerpSpeed * Time.deltaTime);
    }

    void Fire()
    {
        Instantiate(bulletPrefab, bulletPos.transform.position, transform.rotation);
    }

    void LifeHandling()
    {
        playerLife--;

        if (playerLife <= 0)
        {
            //GameOver
            playerLife = 0;
            playerAlive = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("EnemyBullet"))
        {
            LifeHandling();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            _rb.velocity = Vector2.zero;
        }
    }
}