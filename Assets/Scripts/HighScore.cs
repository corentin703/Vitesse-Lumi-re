using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : Singleton<HighScore>
{

    [SerializeField]
    Text highscore;

    [SerializeField]
    Transform highscoreTable;

    [SerializeField]
    GameObject highscorePosPrefab;

    [SerializeField]
    Text highscoreName;

    [SerializeField]
    Button saveNameButton;

    [SerializeField]
    Button saveScoreButton;

    bool hasSaved = false;

    List<GameObject> listHighscorePos = new List<GameObject>();


    private List<Transform> highScoreEntriesTransformList;
    void Start()
    {
        highscore.text = PlayerPrefs.GetInt("time").ToString();
        saveScoreButton.enabled = true;
        if (PlayerPrefs.HasKey("highScores") == false)
        {
            HighScores hs = new HighScores();
            string json = JsonUtility.ToJson(hs);
            PlayerPrefs.SetString("highScores", json);
            PlayerPrefs.Save();
        };
    }

    public void OpenHighs()
    {
        string jsonString = PlayerPrefs.GetString("highScores");
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);

        if(highScores.highScoreEntryList.Count > 1) highScores = sortHighScores(highScores);

        if (highScores.highScoreEntryList.Count > 0)
        {
            highScoreEntriesTransformList = new List<Transform>();
            foreach (HighScoreEntry highscoreEntry in highScores.highScoreEntryList)
            {
                NewHighscoreEntry(highscoreEntry, highScoreEntriesTransformList);
            }
        }
    }

    public void CloseHighs()
    {
        foreach (GameObject highPos in listHighscorePos)
        {
            Destroy(highPos);
        }
    }

    void Update()
    {
        if (hasSaved == true) saveScoreButton.enabled = false;
        if (highscoreName.text == "") saveNameButton.enabled = false;
        else saveNameButton.enabled = true;
    }

    private HighScores sortHighScores(HighScores highScores)
    {
        for(int i = 0; i < highScores.highScoreEntryList.Count; i++)
        {
            for(int j = i + 1; j < highScores.highScoreEntryList.Count; j++)
            {
                if (highScores.highScoreEntryList[j].time < highScores.highScoreEntryList[i].time)
                {
                    HighScoreEntry tempo = highScores.highScoreEntryList[i];
                    highScores.highScoreEntryList[i] = highScores.highScoreEntryList[j];
                    highScores.highScoreEntryList[j] = tempo;
                }
            }
        }

        return highScores;
    }

    private void NewHighscoreEntry(HighScoreEntry newhighscore, List<Transform> transformList)
    {
        Transform highscorePos = Instantiate(highscorePosPrefab, highscoreTable).transform;
        listHighscorePos.Add(highscorePos.gameObject);
        highscorePos.gameObject.SetActive(true);
        int rank = transformList.Count + 1;
        string rankText;
        if (PlayerPrefs.GetString("language") == "FR")
        {
            switch (rank)
            {
                case 1:
                    rankText = rank + "ER";
                    break;
                default:
                    rankText = rank + "EME";
                    break;
            }
        }
        else
        {
            switch (rank)
            {
                case 1:
                    rankText = rank + "ST";
                    break;
                case 2:
                    rankText = rank + "ND";
                    break;
                case 3:
                    rankText = rank + "RD";
                    break;
                default:
                    rankText = rank + "TH";
                    break;
            }
        }
        highscorePos.Find("rankText").GetComponent<Text>().text = rankText;

        highscorePos.Find("scoreText").GetComponent<Text>().text = newhighscore.time.ToString();

        highscorePos.Find("nameText").GetComponent<Text>().text = newhighscore.playerName;

        transformList.Add(highscorePos);
    }

    public void enterLetter(string letter)
    {
        if (letter == "<" && highscoreName.text.Length > 0) highscoreName.text = highscoreName.text.Remove(highscoreName.text.Length-1);
        if(highscoreName.text.Length < 3 && letter != "<") highscoreName.text = highscoreName.text + letter;
    }

    public void saveHighScore()
    {
        addHighScore(PlayerPrefs.GetInt("time"), highscoreName.text);
        hasSaved = true;
        OpenHighs();
    }

    public void addHighScore(float time, string name)
    {
        HighScoreEntry highScoreEntry = new HighScoreEntry();
        highScoreEntry.time = time;
        highScoreEntry.playerName = name;

        string jsonString = PlayerPrefs.GetString("highScores");
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);
    
        highScores.highScoreEntryList.Add(highScoreEntry);

        string json = JsonUtility.ToJson(highScores);
        PlayerPrefs.SetString("highScores", json);
        PlayerPrefs.Save();

    }
    
    private class HighScores
    {
        public List<HighScoreEntry> highScoreEntryList;
    }

    [System.Serializable]
    private class HighScoreEntry
    {
        public float time; //score en secondes
        public string playerName;
    }
}
