using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class SceneManager : MonoBehaviour
    {
        [Header("Screens")]

        #region Screens

        [SerializeField]
        private GameObject PauseMenu;

        [SerializeField] private GameObject GameOverMenu;
        [SerializeField] private GameObject HighscoreMenu;


        #endregion

        [SerializeField] private TMP_Text GameOverScore;
        [SerializeField] private TMP_InputField UserNameInput;
        [SerializeField] private GameObject UserList;
        [SerializeField] private GameObject ScoreList;


        public bool isHighscoreShown;
        private ScoreManager _scoreManager;
        private string username;
        private string fmt = "000000.##";
        private int score;
        private void Start()
        {
            _scoreManager = new ScoreManager();
            isHighscoreShown = false;
            PauseMenu.SetActive(false);
            GameOverMenu.SetActive(false);
            HighscoreMenu.SetActive(false);
        }

        public void Pause(bool status)
        {
            PauseMenu.SetActive(status);
        }

        public void SetUserName(string input)
        {
            username = input;
            Highscore();
        }
        public void GameOver(int userScore)
        {
            GameOverScore.text = score.ToString(fmt);
            score = userScore;
            UserNameInput.Select();
            GameOverMenu.SetActive(true);
        }

        public void Highscore()
        {
            GameOverMenu.SetActive(false);
            HighscoreMenu.SetActive(true);
            isHighscoreShown = true;
            _scoreManager.FillList(UserList, ScoreList);
            _scoreManager.BuildHighscore(score, username);
        }

        public void BackToMenu()
        {
            Debug.Log("Funkt");
        }
    }
}