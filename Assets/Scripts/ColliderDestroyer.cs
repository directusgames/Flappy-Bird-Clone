﻿using UnityEngine;
using System.Collections;

public class ColliderDestroyer : MonoBehaviour {

    public ObstacleManager obstacleManager;
    
    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        
    }
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "ObstaclePair")
        {
            obstacleManager.destroyObstacle(coll.gameObject);
        }
    }
}