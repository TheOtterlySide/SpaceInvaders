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
        
        
        private string username;
        private string fmt = "000000.##";
        private int Score;
        private void Start()
        {
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
            Debug.Log(input);
        }
        public void GameOver(bool status, int score)
        {
            GameOverScore.text = score.ToString(fmt);
            GameOverMenu.SetActive(status);
        }

        public void Highscore(bool status)
        {
            GameOverMenu.SetActive(false);
            HighscoreMenu.SetActive(status);
        }
    }
}