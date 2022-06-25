using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public BeatManager beatManager;
    // Start is called before the first frame update
    void Start()
    {
        beatManager = GetComponent<BeatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        Debug.Log("button pressed");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
