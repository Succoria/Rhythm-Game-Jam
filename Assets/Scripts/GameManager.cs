using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int gameLevel;
    
    public BeatManager BeatManager;

    public List<AudioSettings> _audioSettings;

    // Start is called before the first frame update
    void Start()
    {
            RandomSong();
           
            
            BeatManager.OnBeat += BeatManagerOnOnBeat;
    }

    private void BeatManagerOnOnBeat(long beatIndex)
    {
	    //Only change on odd beats
	    if (beatIndex % 2 == 0)
	    {
		    return;
	    }

	    RandomSong();
    }
    
    private void Update()
    {
	    if (Input.GetKeyDown(KeyCode.Escape))
	    {
		    Application.Quit();
	    }
    }
    
    public void RandomSong()
    {
	    BeatManager.SetAudioSettings(_audioSettings[Random.Range(0, _audioSettings.Count)]);
    }
}
