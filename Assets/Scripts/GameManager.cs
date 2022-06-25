using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int gameLevel;
    
    public BeatManager BeatManager;
    public Button startBtn;
    public Button quitBtn;

    public List<AudioSettings> _audioSettings;
    
    void Start()
    {
	    BeatManager.OnBeat += BeatManagerOnOnBeat;
    }

    private void BeatManagerOnOnBeat(long beatIndex)
    {
	    //Only change on odd beats
	    if (beatIndex % 2 == 0)
	    {
		    return;
	    }
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
