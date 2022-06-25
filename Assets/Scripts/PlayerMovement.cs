using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class PlayerMovement : MonoBehaviour
{
    private float minTimeBetweenMovements = 0.375f - 0.075f;
    private BeatManager beatManager;
    private Vector3 last;
    private Vector3 target;
    private float t;
    public float Distance = 3;
    
    private float _lastBeatTime;

    public AnimationCurve RotationCurve;

    public float RotationMultiplier;

    public KeyCode playerButton;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        beatManager = FindObjectOfType<BeatManager>();
        gameManager = FindObjectOfType<GameManager>();
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
        transform.right = Vector3.Lerp(Vector3.right, Vector3.down, RotationCurve.Evaluate(t)*RotationMultiplier);
    }

    void OnCollisionEnter(Collision other) {
        {
            if(other.collider.tag == "Death")
            {
                gameObject.SetActive(false);
                Debug.Log("Died");
            }
            
            if(other.collider.tag == "Win")
            {
                Debug.Log("Winner");
                gameManager.WinScreen();
            }
        }
    }
}
