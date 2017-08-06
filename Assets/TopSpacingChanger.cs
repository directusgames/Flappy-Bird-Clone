using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopSpacingChanger : MonoBehaviour {
    
    public ObstacleManager om;
    
	// Use this for initialization
	void Start () {
        
        om = GameObject.Find ("Obstacle Manager").GetComponent<ObstacleManager>();
        
        if(om.extraPairSpacing <= 0)
        {
            //do nothing
        }
        else
        {
            Vector3 newPos = new Vector3(0, om.extraPairSpacing, 0);
            transform.position += newPos ;
            om.extraPairSpacing -= 5;
        }
        		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
