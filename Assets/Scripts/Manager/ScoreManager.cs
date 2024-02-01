using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;

namespace Manager
{
    public class ScoreManager
    {
        public int score;
        private string fmt = "000000.##";
        private int maxHighscoreCount = 10;
        public void BuildHighscore()
        {
            var userName = "Tom";
            var userHighscore = score;

            var highscoreList = GetHighscoreList();
            highscoreList = CheckHighscoreList(highscoreList, userHighscore, userName);
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

        private List<HighscoreEntry> CheckHighscoreList(List<HighscoreEntry> entries, int newScore, string userName)
        {
            for (int i = 0; i < entries.Count; i++)
            {
                if (entries[i].Score < newScore)
                {
                    var newEntry = new HighscoreEntry();
                    newEntry.Name = userName;
                    newEntry.Score = newScore;
                    entries.Insert(i, newEntry);
                }
            }

            if (entries.Count > 10)
            {
                var entriesToRemove = entries.Count - maxHighscoreCount;
                entries.RemoveRange(11, entriesToRemove);
            }
            
            return entries;
        }

        private void SaveHighscore(List<HighscoreEntry> entries)
        {
            for (int i = 0; i < maxHighscoreCount; i++)
            {
                PlayerPrefs.SetString("Highscore" + i, entries[i].Name);
                PlayerPrefs.SetInt("Highscore" + i, entries[i].Score);
            }
            
            PlayerPrefs.Save();
        }
    }
}