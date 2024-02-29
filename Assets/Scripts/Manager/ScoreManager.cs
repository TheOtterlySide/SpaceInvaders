using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Manager
{
    public class ScoreManager
    {
        private int highscore;
        private string userName;
        private string fmt = "000000.##";
        private int maxHighscoreCount = 10;
        private GameObject userGameObject;
        private GameObject scoreGameObject;

        public void FillList(GameObject userList, GameObject scoreList)
        {
            userGameObject = userList;
            scoreGameObject = scoreList;
        }
        public void BuildHighscore(int score, string inputUsername)
        {
            userName = inputUsername;
            highscore = score;

            var highscoreList = GetHighscoreList();
            highscoreList = CheckHighscoreList(highscoreList, highscore, userName);
            SaveHighscore(highscoreList);
        }

        public List<HighscoreEntry> GetHighscoreList()
        {
            var fillList = new List<HighscoreEntry>();
            for (int i = 0; i < maxHighscoreCount; i++)
            {
                var entry = new HighscoreEntry();
                
                if (PlayerPrefs.GetString("Highscore" + i) == null)
                {
                    entry.Name = "";
                }
                else
                {
                    entry.Name = PlayerPrefs.GetString("Highscore" + i);
                }
                
                if (PlayerPrefs.GetInt("Highscore" + i) == 0)
                {
                    entry.Score = 0;
                }
                else
                {
                    entry.Score = PlayerPrefs.GetInt("Highscore" + i);
                }
                fillList.Add(entry);
            }

            return fillList;
        }

        private List<HighscoreEntry> CheckHighscoreList(List<HighscoreEntry> entries, int newScore, string newUserName)
        {
            var oldCount = entries.Count();
            int highestCount = -1;
            
            var newEntry = new HighscoreEntry();
            newEntry.Name = newUserName;
            newEntry.Score = newScore;
            
            for (int i = 9; i >= 0; i--)
            {
                if (entries[i].Score < newScore)
                {
                    highestCount = i;
                }
            }

            if (highestCount != -1)
            {
                entries.Insert(highestCount, newEntry);
            }
            
            if (entries.Count > 10)
            {
                var entriesToRemove = entries.Count - maxHighscoreCount;
                entries.RemoveRange(10, entriesToRemove);
            }
            
            return entries;
        }

        private void SaveHighscore(List<HighscoreEntry> entries)
        {
            for (int i = 0; i < entries.Count(); i++)
            {
                var userText = userGameObject.GetComponentsInChildren<TMP_Text>();
                userText[i].text = entries[i].Name;
                var userScore = scoreGameObject.GetComponentsInChildren<TMP_Text>();
                userScore[i].text = entries[i].Score.ToString();
            }

            
            for (int i = 0; i < maxHighscoreCount; i++)
            {
                PlayerPrefs.SetString("Highscore" + i, entries[i].Name);
                PlayerPrefs.SetInt("Highscore" + i, entries[i].Score);
            }
            
            PlayerPrefs.Save();
        }
    }
}