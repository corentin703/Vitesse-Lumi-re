using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    
    public enum EManagerNotif
    {
        SceneChanged,
        GameStart,
        GamePaused,
        GameResumed,
        GameEnded,
    }
    
    [SerializeField] private string[] Scenes;
    [SerializeField] private SEndScene[] EndScenes;

    private int m_currentSceneIndex;
    private List<string> m_listScenes;
    private Dictionary<EndWay, string> m_dictEndScenes;


    protected override void Awake()
    {
        base.Awake();

        bool error = false;
        
        m_listScenes = new List<string>();
        
        foreach (string scene in Scenes)
        {
            m_listScenes.Add(scene);
        }

        m_dictEndScenes = new Dictionary<EndWay, string>();
        foreach (SEndScene scene in EndScenes)
        {
            if (m_dictEndScenes.ContainsKey(scene.Case))
            {
                Debug.LogError("[" + GetType().Name + "] You can't assign more than one scene to a end game event");
                error = true;
            }

            m_dictEndScenes.Add(scene.Case, scene.SceneName);
        }
        
        if (error)
            Application.Quit();

        DontDestroyOnLoad(this);
    }

    public void LoadScene(int sceneNum)
    {
        if (sceneNum >= m_listScenes.Count)
            throw new Exception("[" + GetType().Name + "] Scene reference error");
        
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
            throw new Exception("[" + GetType().Name + "] Scene reference error");
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
        NotifyManagers(EManagerNotif.GamePaused);
    }

    public void GameResume()
    {
        NotifyManagers(EManagerNotif.GameResumed);
    }
    
    public void GameStart()
    {
        SceneManager.LoadScene(m_listScenes[0]);

        NotifyManagers(EManagerNotif.GameStart);
    }

    public void EndGame(EndWay endWay)
    {
        NotifyManagers(EManagerNotif.GameEnded);

        if (m_dictEndScenes.ContainsKey(endWay))
            SceneManager.LoadScene(m_dictEndScenes[endWay]);
        else
            throw new Exception("[" + GetType().Name + "] That endway isn't defined");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void NotifyManagers(EManagerNotif managerNotif)
    {
        MGR_Gameplay.Instance.Notify(managerNotif);
        MGR_Resource.Instance.Notify(managerNotif);
        MGR_Song.Instance.Notify(managerNotif);
        MGR_TimeLine.Instance.Notify(managerNotif);
        MGR_UI.Instance.Notify(managerNotif);
    }
}
