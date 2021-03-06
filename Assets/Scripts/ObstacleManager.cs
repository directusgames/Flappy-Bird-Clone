﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic; // List<>.

public class ObstacleManager : MonoBehaviour {
	public float m_speed;
	public List<GameObject> m_obstacleObjects;
	public GameObject m_obstaclePrefab;
	public BoxCollider2D m_createCollider;
	public BoxCollider2D m_destroyCollider;
    public float maxVertChange, minVertChange;
    
    public float extraPairSpacing;
    public Vector3 m_spawnOrigin;
    
	// Poor form, but quick.
	private Vector3 m_pairOne = new Vector3(50f, 50f, 1f);
   
    private int obstNum = 0;
    
    //Obstacle movement increase rate
    public float speedIncrease = 0.05f;

	// Use this for initialization
	public void Start () {
		GameObject pairOne = this.createObstacle ();
		pairOne.transform.position = m_pairOne;
        PauseObstacles();
        StopObstacles();
    }

	public void Reset () {
        Time.timeScale = 1f;
        obstNum = 0;
		extraPairSpacing = 25f;
        destroyAll ();
		Start ();
	}
	
	// Update is called once per frame
	void Update () {
		//if (m_started && !m_paused) {
//			foreach (GameObject obstacle in m_obstacleObjects) {
//				// Update all game objects positions
//				for (int i = 0; i < 3; i++) {
//					Rigidbody2D obstacleBody = obstacle.transform.GetChild (i).GetComponent<Rigidbody2D> ();
//					Vector3 curPos = obstacleBody.position;
//					curPos.x -= m_speed * Time.deltaTime;
//					obstacleBody.position = curPos;
//				}
//			}
		//}
	}

	public void destroyAll () {
        
		foreach (GameObject obstacle in m_obstacleObjects) 
        {
			Destroy (obstacle);
		}
		m_obstacleObjects.Clear ();
	}

	/**
	 * Pair
	 * 	- Top
	 * 		- Collider2D
	 *  - Bottom
	 * 		- Collider2D
	 * 
	 * Given the top or bottom Collider2D, delete the parent GameObject 'pair'.
	 */
	public void destroyObstacle(GameObject obstacle) {
		if(obstacle != null)
        {
            m_obstacleObjects.Remove (obstacle);
    		Destroy (obstacle);
        }
	}

	/**
	 * Create a new collider at Vector3 m_spawnOrigin.
	 */
	public GameObject createObstacle() {
		GameObject newObs = (GameObject) Instantiate (
			m_obstaclePrefab,
			m_spawnOrigin,
			Quaternion.identity
		);
        
        //Randomise obstacle height
        AdjustObstacleHeight(newObs, Random.Range (minVertChange, maxVertChange));
        AdjustObstacleSpacing(newObs);
		newObs.transform.parent = this.transform;
        newObs.name = "Pair " + obstNum;
        if(obstNum > 5)
        {
            Time.timeScale += 0.05f;
        }
        
        obstNum++;
		m_obstacleObjects.Add (newObs);
		return newObs;
	}
    
    //Pause mvoement of all current obstacles in scene
    public void PauseObstacles()
    {
        //Debug.Log ("Pausing obstacles");
        foreach (GameObject obs in m_obstacleObjects)
        {
            ObstacleMovement om = obs.GetComponent<ObstacleMovement>();
            om.paused = true;
        }
    }
    
    public void UnpauseObstacles()
    {   
        //Debug.Log ("Unpausing obstacles");
        foreach (GameObject obs in m_obstacleObjects)
        {
            ObstacleMovement om = obs.GetComponent<ObstacleMovement>();
            om.paused = false;
		}
    }
    
    public void StartObstacles()
    {
        //Debug.Log ("Starting obstacles");
        foreach (GameObject obs in m_obstacleObjects)
        {
            ObstacleMovement om = obs.GetComponent<ObstacleMovement>();
            om.started = true;
        }
    }
   
    
    public void StopObstacles()
    {
        //Debug.Log ("Stopping obstacles");
        foreach (GameObject obs in m_obstacleObjects)
        {
            ObstacleMovement om = obs.GetComponent<ObstacleMovement>();
            om.started = false;
        }
    }
    
    private void AdjustObstacleHeight(GameObject obs, float deltaHeight)
    {
        //Change the scale of the obstacle as well as the position which changes when unity scales the object. This position change
        //moves it back to its original placement.
        
        obs.transform.position += new Vector3(0f, deltaHeight, 0f);
    }
    
    private void AdjustObstacleSpacing(GameObject obs)
    {
        GameObject topObs = obs.transform.Find("Top").gameObject;
        GameObject botObs = obs.transform.Find("Bottom").gameObject;
        topObs.transform.position = new Vector3(topObs.transform.position.x, topObs.transform.position.y + extraPairSpacing, topObs.transform.position.z);
        botObs.transform.position = new Vector3(botObs.transform.position.x, botObs.transform.position.y - extraPairSpacing, botObs.transform.position.z);
        if(extraPairSpacing > 0)
        {
            extraPairSpacing -= 5;
        }
    }
}
