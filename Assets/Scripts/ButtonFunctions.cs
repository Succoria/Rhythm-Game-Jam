using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public GameObject redWin;
    public GameObject pinkWin;
    public GameObject greenWin;
    public GameObject blueWin;
    public GameObject allLoss;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        
        if(redWin != null || pinkWin != null || greenWin != null || blueWin != null)
        {
            redWin.SetActive(false);
            pinkWin.SetActive(false);
            greenWin.SetActive(false);
            blueWin.SetActive(false);
        }

        if(allLoss != null)
        {
            allLoss.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
