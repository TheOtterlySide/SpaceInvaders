using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

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
        private ScoreManager _scoreManager;
        [SerializeField]
        private SceneManager sceneManager;
        #endregion
        
        // Start is called before the first frame update
        private void Start()
        {
            _scoreManager = new ScoreManager();
            stageDimensions = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
            score = 000000;
            SetupWalls();
            SetPosition();
            StartGame();
        }

        // Update is called once per frame
        void Update()
        {
           UpdateUiLife();
           UpdateUiScore();
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

        bool CheckPlayerStatus()
        {
            return player.playerAlive;
        }
        void UpdateUiLife()
        {
            switch (player.playerLife)
            {
                case 3:
                    playerLife3.SetActive(false);
                    break;
                case 2:
                    playerLife2.SetActive(false);
                    break;
                case 1:
                    playerLife1.SetActive(false);
                    break;
                case 0:
                    gameRunning = false;
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

        void GameOver()
        {
            //Pause Game, Open End Scene, Show Highscores
            Time.timeScale = 0.0f;
            _scoreManager.BuildHighscore(score);
            sceneManager.GameOver(true, score);
        }
        
        public void Pause()
        {
            if (gameRunning)
            {
                Time.timeScale = 0.0f;
                sceneManager.Pause(true);
                gameRunning = !gameRunning;
            }

            else
            {
                Time.timeScale = 1.0f;
                sceneManager.Pause(false);
                gameRunning = !gameRunning;
            }
        }

        private void StopTheGame()
        {
            
        }

 
    }
}