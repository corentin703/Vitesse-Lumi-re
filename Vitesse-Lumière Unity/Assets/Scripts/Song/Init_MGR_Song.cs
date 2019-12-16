using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init_MGR_Song : MonoBehaviour
{
    public AudioClip[] BackgroundSongs;
    public MGR_Song.SSong[] Songs;
    
    void Awake()
    {
        if (MGR_Song.Instance)
            MGR_Song.Instance.SetUp(Songs, BackgroundSongs);
        
        Destroy(this);
    }
}
