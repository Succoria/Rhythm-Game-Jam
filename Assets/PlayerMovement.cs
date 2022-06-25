using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private BeatManager beatManager;
    private Vector3 target;
    public KeyCode playerButton;
    // Start is called before the first frame update
    void Start()
    {
        beatManager = FindObjectOfType<BeatManager>();
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(playerButton))
        {
            target += Vector3.right;
        }
        
        transform.position = Vector3.MoveTowards(transform.position,target, 4 * Time.deltaTime);
    }
}
