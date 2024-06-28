using Manager;
using UnityEngine;
using UnityEngine.Serialization;

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
    
    [FormerlySerializedAs("_gm")] [SerializeField] 
    private GameManager _gameManager;

    [FormerlySerializedAs("_sceneManager")] [FormerlySerializedAs("_sm")] [SerializeField] 
    private SceneCustomManager sceneCustomManager;
    #endregion

    private PlayerControls _controls;
    private Rigidbody2D _rigidbody2D;

    [SerializeField] private Animator AnimationControl;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        playerPosX = 0;
        playerPosY = 0;
        playerPos = new Vector2(playerPosX, playerPosY);
        _controls = new PlayerControls();
        _controls.Enable();
        playerAlive = true;
        _controls.Player.Move.performed += moving => { playerPos.x = moving.ReadValue<float>(); };
        _controls.Player.Pause.performed += _ => Pause();
        _controls.Player.Fire.performed += _ => Fire();
        AnimationControl.SetBool("PlayerAlive", true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdatePosition();
    }

    void Pause()
    {
            _gameManager.Pause();
    }

    void UpdatePosition()
    {
        _rigidbody2D.velocity = Vector2.Lerp(_rigidbody2D.velocity, playerPos * speed, lerpSpeed * Time.deltaTime);

        if (_rigidbody2D.velocity.x > 0)
        {
            //Driving to the right side
            AnimationControl.SetInteger("Direction", 1);
        }
        else
        {
            //Driving to the left side
            AnimationControl.SetInteger("Direction", -1);
        }
    }

    void Fire()
    {
        if (_gameManager.gameRunning)
        {
            Instantiate(bulletPrefab, bulletPos.transform.position, transform.rotation);
        }

        if (sceneCustomManager.isHighscoreShown)
        {
            sceneCustomManager.BackToMenu();
        }
    }

    void LifeHandling()
    {
        playerLife--;

        if (playerLife <= 0)
        {
            //GameOver
            playerLife = 0;
            playerAlive = false;
            AnimationControl.SetBool("PlayerAlive", false);
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
            _rigidbody2D.velocity = Vector2.zero;
        }
    }
}