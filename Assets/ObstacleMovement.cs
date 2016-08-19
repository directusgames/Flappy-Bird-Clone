using UnityEngine;
using System.Collections;

public class ObstacleMovement : MonoBehaviour {
    
    public GameObject topObs, botObs; //top and bottom obstacles
    public float speed;
    public bool started, paused;
    
	// Use this for initialization
	void Start () {
	    started = true;
        paused = false;
	}
	
	// Update is called once per frame
	void Update () {
        
        if(started && !paused)
        {
            Vector3 curPos = transform.position;
            curPos.x -= speed * Time.deltaTime;
            transform.position = curPos; 
        }      
    }
}
