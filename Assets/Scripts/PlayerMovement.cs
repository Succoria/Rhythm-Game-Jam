using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class PlayerMovement : MonoBehaviour
{
    public float minTimeBetweenMovements = 0.375f + 0.075f;

    private BeatManager beatManager;
    private Vector3 last;
    private Vector3 target;
    private float t;
    public float Distance = 3;

    public AnimationCurve RotationCurve;

    private float _lastBeatTime;

    public KeyCode playerButton;
    // Start is called before the first frame update
    void Start()
    {
        beatManager = FindObjectOfType<BeatManager>();
        target = transform.position;
        last = target;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(playerButton))
        {
            if (Time.time - _lastBeatTime < (minTimeBetweenMovements)) 
            {
                Debug.LogWarning("FAILNERD");
                //FAIL! NERD
            
                last = target;
                target -= Vector3.right * Distance * 0.5f;
                t = 0;
                _lastBeatTime = Time.time;
                
                
            }
            else
            {
                
                last = target;
                target += Vector3.right * Distance;
                t = 0;
                _lastBeatTime = Time.time;

            }
        }

        t += Time.deltaTime * 4;
        float tEase = Easing.OutBounce(t);
        transform.position = Vector3.Lerp(last,target, tEase);
        transform.right = Vector3.Lerp(Vector3.right, Vector3.down, RotationCurve.Evaluate(t));
    }
}
