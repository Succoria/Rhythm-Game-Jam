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

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public GameObject allLoss;
    public GameObject background;

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

        if(!player1.activeSelf && !player2.activeSelf && !player3.activeSelf && !player4.activeSelf)
        {
            allLoss.SetActive(true);
            WinScreen();
        }
    }
    
    public void RandomSong()
    {
	    BeatManager.SetAudioSettings(_audioSettings[Random.Range(0, _audioSettings.Count)]);
    }

    public void WinScreen()
    {
        startBtn.gameObject.SetActive(true);
        quitBtn.gameObject.SetActive(true);
        background.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

}
