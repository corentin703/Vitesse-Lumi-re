using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public enum EndWay
    {
        Win,
        Loose
    }

    [System.Serializable]
    public struct SEndScene
    {
        public EndWay Case;
        public string SceneName;
    }
    
    [SerializeField] private string[] Scenes;
    [SerializeField] private SEndScene[] EndScenes;

    private int m_currentSceneIndex;
    private List<string> m_listScenes;
    private Dictionary<EndWay, string> m_dictEndScenes;


    protected override void Awake()
    {
        base.Awake();

        m_listScenes = new List<string>();
        
        foreach (string scene in Scenes)
        {
            if (true)
            {
                m_listScenes.Add(scene);
            }
            else
                throw new Exception("[GameManager] Scene \"" + scene + "\" doen't exists");
        }

        m_dictEndScenes = new Dictionary<EndWay, string>();
        foreach (SEndScene scene in EndScenes)
        {
            if (SceneManager.GetSceneByName(scene.SceneName).IsValid())
            {
                if (m_dictEndScenes.ContainsKey(scene.Case))
                    throw new Exception("[GameManager] You can't assign more than one scene to a end game event");
                    
                m_dictEndScenes.Add(scene.Case, scene.SceneName);
            }
            else
                throw new Exception("[GameManager] Scene \"" + scene + "\" doen't exists");
        }
        
        DontDestroyOnLoad(this);
    }

    public void LoadScene(int sceneNum)
    {
        if (sceneNum >= m_listScenes.Count)
            throw new Exception("[GameManager] Scene reference error");
        
        LoadScene(m_listScenes[sceneNum]);
        
        NotifyManagers(EManagerNotif.SceneChanged);
    }
    
    public void LoadScene(string sceneName)
    {
        if (m_listScenes.Contains(sceneName))
        {
            SceneManager.LoadScene(sceneName);
            NotifyManagers(EManagerNotif.SceneChanged);
        }
        else if (m_dictEndScenes.ContainsValue(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
            throw new Exception("[GameManager] Scene reference error");
    }

    public void LoadNextScene()
    {
        if ((m_currentSceneIndex + 1) >= (m_listScenes.Count - 1))
            LoadScene(m_currentSceneIndex + 1);
        else
            EndGame(EndWay.Win);
    }

    public void GamePause()
    {
//        MGR_TimeLine.Instance.ChronoPause();
        NotifyManagers(EManagerNotif.GamePaused);
    }

    public void GameResume()
    {
//        MGR_TimeLine.Instance.ChronoResume();
        NotifyManagers(EManagerNotif.GameResumed);
    }

    public void GameStart()
    {
        SceneManager.LoadScene(m_listScenes[0]);

        MGR_TimeLine.Instance.ChronoStart();
    }

    public void EndGame(EndWay endWay)
    {
        MGR_TimeLine.Instance.ChronoStop();

        if (m_dictEndScenes.ContainsKey(endWay))
            LoadScene(m_dictEndScenes[endWay]);
        else
        {
//            throw new Exception("That endway isn't defined");
            Debug.LogError("That endway isn't defined");
            Quit();
        }
            
    }

    public void Quit()
    {
        Application.Quit();
    }

    public enum EManagerNotif
    {
        SceneChanged,
        GamePaused,
        GameResumed,
    }
    
    public void NotifyManagers(EManagerNotif managerNotif)
    {
        MGR_Gameplay.Instance.Notify(managerNotif);
        MGR_Ressource.Instance.Notify(managerNotif);
        MGR_Song.Instance.Notify(managerNotif);
        MGR_TimeLine.Instance.Notify(managerNotif);
        MGR_UI.Instance.Notify(managerNotif);
    }
}
