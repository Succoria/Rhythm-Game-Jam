using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public List<Transform> Targets;
    
    void Update()
    {
        float averageX = 0;

        foreach (Transform target in Targets)
        {
            averageX += target.position.x;
        }
        averageX /= Targets.Count;
        
        transform.position = new Vector3(averageX, transform.position.y, transform.position.z);
    }
}
