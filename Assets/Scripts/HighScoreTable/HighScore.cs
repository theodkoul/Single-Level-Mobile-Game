using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DnA.HighScoreNs
{
    public class HighScore : MonoBehaviour
    {

        private Transform entryContainer;
        private Transform entryTemplate;
        private List<Transform> highscoreEntryTransformList;

        private GameObject[] entry;
        
        private void Awake()
        {
            DoThis(true);
        }

        private void DoThis(bool firstTime)
        {
            if (firstTime)
            {
                entryContainer = transform.Find("HighScoreEntryContainer");
                entryTemplate = entryContainer.Find("HighScoreEntryTemplate");
                
                entryTemplate.gameObject.SetActive(false);

                //PlayerPrefs.DeleteAll();
            }
            string jsonString = PlayerPrefs.GetString("HighScoreTable");
            Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

            if (highscores == null)
            {
                // There's no stored table, initialize
                Debug.Log("Initializing table with default values...");
                AddHighscoreEntry(1, "TED");
                // Reload
                jsonString = PlayerPrefs.GetString("HighScoreTable");
                highscores = JsonUtility.FromJson<Highscores>(jsonString);
            }

            if (firstTime)
            {
                // Sort entry list by Score
                for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
                {
                    for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
                    {
                        if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                        {
                            // Swap
                            HighscoreEntry tmp = highscores.highscoreEntryList[i];
                            highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                            highscores.highscoreEntryList[j] = tmp;
                        }
                    }
                }
            }
            
        }

        private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
        {
            float templateHeight = 35f;
            Transform entryTransform = Instantiate(entryTemplate, container);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, 165.9f - templateHeight * transformList.Count);
            entryTransform.gameObject.SetActive(true);

            int rank = transformList.Count + 1;
            string rankString;
            switch (rank)
            {
                default:
                    rankString = rank + "TH"; break;

                case 1: rankString = "1ST"; break;
                case 2: rankString = "2ND"; break;
                case 3: rankString = "3RD"; break;
            }

            entryTransform.Find("PosText").GetComponent<Text>().text = rankString;

            int score = highscoreEntry.score;

            entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString();

            string name = highscoreEntry.name;
            entryTransform.Find("NameText").GetComponent<Text>().text = name;

            // Set background visible odds and evens, easier to read
            entryTransform.Find("Background").gameObject.SetActive(rank % 2 == 1);

            // Highlight First
            if (rank == 1)
            {
                entryTransform.Find("PosText").GetComponent<Text>().color = Color.green;
                entryTransform.Find("ScoreText").GetComponent<Text>().color = Color.green;
                entryTransform.Find("NameText").GetComponent<Text>().color = Color.green;
            }

            // Set trophy
            switch (rank)
            {
                default:
                    entryTransform.Find("Trophy").gameObject.SetActive(false);
                    break;
                case 1:
                    entryTransform.Find("Trophy").GetComponent<Image>().color = Color.yellow;
                    break;
                case 2:
                    entryTransform.Find("Trophy").GetComponent<Image>().color = Color.magenta;
                    break;
                case 3:
                    entryTransform.Find("Trophy").GetComponent<Image>().color = Color.cyan;
                    break;

            }

            transformList.Add(entryTransform);
        }

        public void AddHighscoreEntry(int score, string name)
        {
            // Create HighscoreEntry
            HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

            // Load saved Highscores
            string jsonString = PlayerPrefs.GetString("HighScoreTable");
            Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

            if (highscores == null)
            {
                // There's no stored table, initialize
                highscores = new Highscores()
                {
                    highscoreEntryList = new List<HighscoreEntry>()
                };
            }
            else
            {
                for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
                {
                    if (highscores.highscoreEntryList[i].score == highscoreEntry.score)
                    {
                        highscores.highscoreEntryList.RemoveAt(i);
                    }
                }
                if (highscores.highscoreEntryList.Count == 10)
                {
                    highscores.highscoreEntryList.RemoveAt(9);
                }
            }
            // Add new entry to Highscores
            highscores.highscoreEntryList.Add(highscoreEntry);

            

            //Disable the old highScoreEntries
            entryContainer = transform.Find("HighScoreEntryContainer");
            foreach (Transform t in entryContainer)
            {
                if (t.tag == "HighScoreEntryTemplate")
                {
                    t.gameObject.SetActive(false);
                }
            }

            highscoreEntryTransformList = new List<Transform>();
            foreach (HighscoreEntry highscoreEntry3 in highscores.highscoreEntryList)
            {
                CreateHighscoreEntryTransform(highscoreEntry3, entryContainer, highscoreEntryTransformList);
            }

          

            for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
            {
                for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
                {
                    if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                    {
                        // Swap
                        HighscoreEntry tmp = highscores.highscoreEntryList[i];
                        highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                        highscores.highscoreEntryList[j] = tmp;
                    }
                }
            }
            // Save updated Highscores
            string json = JsonUtility.ToJson(highscores);
            PlayerPrefs.SetString("HighScoreTable", json);
            PlayerPrefs.Save();
            Debug.Log(json);
        }

        public void LoadHighScore()
        {
            //RUN IT AGAIN
            string jsonString = PlayerPrefs.GetString("HighScoreTable");
            Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
            Debug.Log(jsonString);
        }

        private class Highscores
        {
            public List<HighscoreEntry> highscoreEntryList;
        }

        //Represents a single High score entry
        [System.Serializable]
        private class HighscoreEntry
        {
            public int score;
            public string name;
        }

    }
}