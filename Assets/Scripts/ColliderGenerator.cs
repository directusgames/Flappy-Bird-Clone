using UnityEngine;
using System.Collections;

public class ColliderGenerator : MonoBehaviour {
    
    public ObstacleManager obstacleManager;
    
    public bool create;
    
	// Use this for initialization
	void Start () {
        
        create = false;
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "ObstaclePair")
        {            
            if(create)
            {   
                create = false;
                obstacleManager.createObstacle ();
            }
        }           
    }
    
    void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "ObstaclePair")
        {
            create = true;
        }
    }
}
