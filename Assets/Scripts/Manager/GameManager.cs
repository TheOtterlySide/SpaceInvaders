using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        [Header("GameStates")] 
        [SerializeField] private bool gameRunning;
        
        [Header("Entities")]
        [SerializeField] private GameObject player;

        [SerializeField] private GameObject defense;
        
        [SerializeField] private Camera mainCamera;

        [Header("Play Field")]

        #region Playfield

            [SerializeField] private GameObject wallLeft;
            [SerializeField] private GameObject wallRight;
            [SerializeField] private GameObject wallUp;
            [SerializeField] private GameObject wallDown;
            private Vector3 stageDimensions;

        #endregion
        
        
        // Start is called before the first frame update
        void Start()
        {
            stageDimensions = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
            
            SetupWalls();
            SetPosition();
            StartGame();
        }

        // Update is called once per frame
        void Update()
        {
        }

        void SetPosition()
        {
            player.transform.position = new Vector3(0, -stageDimensions.y + 1.5f, 0);
            defense.transform.position = new Vector3(-stageDimensions.x + 7f, -stageDimensions.y + 5f, 0);
        }

        void StartGame()
        {
            gameRunning = true;
        }

        void SetupWalls()
        {
            wallLeft.transform.position = new Vector3(-stageDimensions.x - 0.5f, 0, 0);
            wallRight.transform.position = new Vector3(stageDimensions.x + 0.5f, 0, 0);
            wallUp.transform.position = new Vector3(0, stageDimensions.y + 0.5f, 0);
            wallDown.transform.position = new Vector3(0, -stageDimensions.y - 0.5f, 0);
        }
    }
}