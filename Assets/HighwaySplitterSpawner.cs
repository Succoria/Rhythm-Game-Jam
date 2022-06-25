using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighwaySplitterSpawner : MonoBehaviour
{
    public GameObject ToSpawn;

    public Vector3 SpawnRotation = new Vector3(0, -90, 90);
    
    private BeatManager beatManager;

    private void Start()
    {
        beatManager = FindObjectOfType<BeatManager>();
        beatManager.OnBeat += (long i) =>
        {
            if (i % 4 == 0)
            {
                Instantiate(ToSpawn, transform.position, Quaternion.Euler(SpawnRotation));
            }
        };
    }
}
