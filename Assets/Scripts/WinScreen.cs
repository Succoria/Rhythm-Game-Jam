using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    public GameObject redWin;
    public GameObject pinkWin;
    public GameObject greenWin;
    public GameObject blueWin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other) {
        {
            if(other.collider.tag == "Red")
            {
                redWin.SetActive(true);
            }

            if(other.collider.tag == "Pink")
            {
                pinkWin.SetActive(true);
            }

            if(other.collider.tag == "Green")
            {
                greenWin.SetActive(true);
            }

            if(other.collider.tag == "Blue")
            {
                blueWin.SetActive(true);
            }
        }
    }
}
