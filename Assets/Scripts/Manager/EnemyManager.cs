using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Manager
{
    public class EnemyManager : MonoBehaviour
    {
        // Start is called before the first frame update

        [Header("Mobs")]

        #region Mobs

        [SerializeField]
        private GameObject enemyPrefab;

        [SerializeField] 
        private GameObject enemyPrefab_Bonus;

        [SerializeField] 
        private int mobsKilled1;
        [SerializeField] 
        private int speedAddition1;
        [SerializeField] 
        private int mobsKilled2;
        [SerializeField] 
        private int speedAddition2;

        [SerializeField] 
        private float shootTimer;

        [SerializeField] 
        private float timeCooldown = 5;
        #endregion

        [Header("Grid")]

        #region Grid

        [SerializeField]
        private int columnCount;

        [SerializeField] 
        private Enemy[] enemies;
        [SerializeField] 
        private int xSpacing;
        [SerializeField] 
        private int ySpacing;
        [SerializeField] 
        private Transform spawnStartPoint;
        private float minX;
        private int rowIndex;

        #endregion

        [Header("Movement")]

        #region Movement

        [SerializeField]
        private float speed;

        private bool isfirstTriggered;
        private bool isFacingRight;
        private List<GameObject> aliens = new List<GameObject>();
        private int rowCount;
        private float maxX;
        private float xIncrement;
        private Vector2 currentX;

        #endregion

        void Start()
        {
            rowIndex = 0;
            minX = spawnStartPoint.position.x;
            BuildGrid();

            maxX = minX + 2f * xSpacing * columnCount;
            currentX.x = minX;
        }


        // Update is called once per frame
        void Update()
        {
            Move();
            EnemyFire();
        }

        private void EnemyFire()
        {
            shootTimer += Time.deltaTime;

            if (shootTimer >= timeCooldown)
            {
                shootTimer -= timeCooldown;
                Fire();
            }
        }

        void BuildGrid()
        {
            Vector2 currentPos = spawnStartPoint.position;
            currentPos.y = currentPos.y - 3;

            foreach (var enemyType in enemies)
            {
                var invaderName = enemyType.name.Trim();
                for (int i = 0, len = enemyType.rowCount; i < len; i++)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        var invader = (GameObject)Instantiate(enemyPrefab, transform.position, transform.rotation);
                        invader.AddComponent<SpriteRenderer>().sprite = enemyType.sprite;
                        invader.name = j.ToString();
                        invader.transform.position = currentPos;
                        invader.transform.SetParent(gameObject.transform);
                        aliens.Add(invader);

                        currentPos.x += xSpacing;
                    }

                    currentPos.x = minX;
                    currentPos.y -= ySpacing;
                    rowIndex++;
                }
            }
        }

        void Move()
        {
            var direction = Vector2.left;
            
            if (!isFacingRight)
            {
                //Mobs move to the left
                direction = Vector2.left;
            }
            else
            {
                direction = Vector2.right;
            }

            foreach (var alienGO in aliens)
            {
                var _rb = alienGO.GetComponent<Rigidbody2D>();
                _rb.velocity = direction * speed;
            }
        }

        public void changeDirection()
        {
            isFacingRight = !isFacingRight;
            foreach (var alienGO in aliens)
            {
                alienGO.transform.Translate(0, -1, 0);
            }
        }

        public void CustomCollisionEnter(Collision2D other)
        {
            if (isfirstTriggered == false)
            {
                isfirstTriggered = true;

                if (other.gameObject.CompareTag("Wall"))
                {
                    changeDirection();
                }
            }
        }

        public void CustomCollisionExit(Collision2D other)
        {
            isfirstTriggered = false;
        }

        public void DeleteAlienFromList(GameObject alien)
        {
            aliens.Remove(alien);
            
            if (aliens.Count == mobsKilled1)
            {
                speed += speedAddition1;
            }

            if (aliens.Count == mobsKilled2)
            {
                speed += speedAddition2;
            }
        }

        void Fire()
        {
            var randomObject = Random.Range(0, aliens.Count);
            aliens[randomObject].GetComponent<Enemy>().Fire();
        }
    }
}