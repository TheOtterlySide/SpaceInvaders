using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class EnemyManager : MonoBehaviour
    {
        // Start is called before the first frame update

        [Header("Mobs")]

        #region Mobs
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private GameObject enemyPrefab_Bonus;
        private GameObject swarm;    
        #endregion

        [Header("Grid")]

        #region Grid

        [SerializeField] private int columnCount;
        [SerializeField] private Enemy[] enemies;
        [SerializeField] private int xSpacing;
        [SerializeField] private int ySpacing;

        [SerializeField] private Transform spawnStartPoint;
        private float minX;
        private int rowIndex;

        #endregion
        
        [Header("Movement")]

        #region Movement
        
        [SerializeField] private float speed;
        [SerializeField] private float lerpspeed;
        private bool isFacingRight;
        private List<GameObject> aliens = new List<GameObject>();
        private int rowCount;
        private float maxX;
        private float xIncrement;
        private Vector2 currentX;

        #endregion
        void Start()
        {
            swarm = new GameObject { name = "Swarm" };

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
        }
        

        void BuildGrid()
        {
            Vector2 currentPos = spawnStartPoint.position;
            Debug.Log(minX + " " + maxX);
            foreach (var enemyType in  enemies)
            {
                var invaderName = enemyType.name.Trim();
                for (int i = 0, len = enemyType.rowCount; i < len; i++)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        var invader = (GameObject)Instantiate(enemyPrefab, transform.position, transform.rotation); 
                        invader.AddComponent<SpriteRenderer>().sprite = enemyType.sprite;
                        invader.transform.position = currentPos;
                        invader.transform.SetParent(swarm.transform);
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
            xIncrement = speed * Time.deltaTime;
            if (!isFacingRight)
            {
                //Mobs move to the left
                currentX.x += xIncrement;

                if (currentX.x < maxX)
                {
                    foreach (var alienGO in aliens)
                    {
                        var _rb = alienGO.GetComponent<Rigidbody2D>();
                        _rb.velocity = Vector2.Lerp(_rb.velocity, currentX * speed, lerpspeed * Time.deltaTime);
                    }
                    
                }
                else
                {
                    changeDirection();
                }
            }
            else
            {
                //Mobs move to the right
                currentX.x -= xIncrement;
                //if (currentX.x )
            }
        }

        void changeDirection()
        {
            isFacingRight = !isFacingRight;
            swarm.transform.Translate(swarm.transform.position.x, 
                swarm.transform.position.y - ySpacing, 
                swarm.transform.position.z );
        }
    }
}
