using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        [Header("GameStates")] 
        public bool gameRunning;

        [Header("Entities")]
        [SerializeField] private Player player;

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

        [Header("Play Field")]

        #region UI

        [SerializeField] private GameObject playerLife1;
        [SerializeField] private GameObject playerLife2;
        [SerializeField] private GameObject playerLife3;
        public int score;
        [SerializeField] private int scoreLength;
        [SerializeField] private TMP_Text highscorePoints;
        private string fmt = "000000.##";
        #endregion

        #region Manager
        [FormerlySerializedAs("sceneManager")] [SerializeField]
        private SceneCustomManager sceneCustomManager;
        [SerializeField] private AudioManager audioManager;
        #endregion


        private bool firstStart = true;
        // Start is called before the first frame update
        private void Start()
        {
            Setup();
        }

        private void Setup()
        {
            stageDimensions = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
            score = 000000;
            SetupWalls();
            SetPosition();
            StartGame();

            if (firstStart)
            {
                audioManager.Start();
            }

            firstStart = false;
        }

        // Update is called once per frame
        void Update()
        {
           UpdateUiLife();
           UpdateUiScore();
        }

        void SetPosition()
        {
            player.transform.position = new Vector3(1, -stageDimensions.y + 1.5f, 0);
            defense.transform.position = new Vector3(7, -stageDimensions.y + 6f, 0);
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

        bool CheckPlayerStatus()
        {
            return player.playerAlive;
        }
        void UpdateUiLife()
        {
            switch (player.playerLife)
            {
                case 2:
                    playerLife3.SetActive(false);
                    break;
                case 1:
                    playerLife2.SetActive(false);
                    break;
                case 0:
                    playerLife1.SetActive(false);
                    GameOver();
                    break;
                default:
                    break;
            }
        }

        void UpdateUiScore()
        {
            highscorePoints.text = score.ToString(fmt);
        }

        public void GameOver()
        {
            Time.timeScale = 0.0f;
            gameRunning = false;
            audioManager.Stop();
            sceneCustomManager.GameOver(score);
        }
        
        public void Pause()
        {
            if (gameRunning)
            {
                Time.timeScale = 0.0f;
                sceneCustomManager.Pause(true);
                gameRunning = !gameRunning;
                audioManager.Pause();
            }

            else
            {
                Time.timeScale = 1.0f;
                sceneCustomManager.Pause(false);
                gameRunning = !gameRunning;
                audioManager.Resume();
            }
        }
    }
}