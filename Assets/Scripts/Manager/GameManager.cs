using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        [Header("GameStates")] 
        [SerializeField] private bool gameRunning;
        
        [Header("Entities")]
        [SerializeField] private GameObject player;

        [SerializeField] private Camera mainCamera;

        [Header("Playfield")]

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
            
        }

        void StartGame()
        {
            gameRunning = true;
        }

        void SetupWalls()
        {
            stageDimensions = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

            wallLeft.transform.position = new Vector3(-stageDimensions.x - 0.5f, 0, 0);
            wallRight.transform.position = new Vector3(stageDimensions.x + 0.5f, 0, 0);
            wallUp.transform.position = new Vector3(0, stageDimensions.y + 0.5f, 0);
            wallDown.transform.position = new Vector3(0, -stageDimensions.y - 0.5f, 0);
        }
    }
}