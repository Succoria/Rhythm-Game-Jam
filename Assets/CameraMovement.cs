using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float unitsPerSecond = 3;
    public float BPM;
    public List<Transform> Targets;
    
    void Update()
    {
        float averageX = 0;

        foreach (Transform target in Targets)
        {
            averageX += target.position.x;
        }
        averageX /= Targets.Count;
        
        //transform.position = new Vector3(averageX, transform.position.y, transform.position.z);
        
        float bps = BPM / 60;
        float x =  bps * unitsPerSecond * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z);
    }
}
