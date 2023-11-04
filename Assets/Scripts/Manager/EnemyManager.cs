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
        void Start()
        {
            BuildGrid();
            //SpawnMobs();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        void SpawnMobs()
        {
        }

        void BuildGrid()
        {
            minX = spawnStartPoint.position.x;

            GameObject swarm = new GameObject { name = "Swarm" };
            Vector2 currentPos = spawnStartPoint.position;

            rowIndex = 0;
            foreach (var enemyType in  enemies)
            {
                var invaderName = enemyType.name.Trim();
                for (int i = 0, len = enemyType.rowCount; i < len; i++)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        var invader = new GameObject() { name = invaderName };
                        invader.AddComponent<SpriteRenderer>().sprite = enemyType.sprite[0]; //TODO Sprites richtig zuordnen aufpassen!
                        invader.transform.position = currentPos;
                        invader.transform.SetParent(swarm.transform);

                        currentPos.x += xSpacing;
                    }

                    currentPos.x = minX;
                    currentPos.y -= ySpacing;
                    rowIndex++;
                }
            }
        }
    }
}
