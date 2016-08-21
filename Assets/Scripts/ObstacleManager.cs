using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleManager : MonoBehaviour {
	public float m_speed;
	public List<GameObject> m_obstacleObjects;
	public GameObject m_obstaclePrefab;
	public BoxCollider2D m_createCollider;
	public BoxCollider2D m_destroyCollider;

	// Poor form, but quick.
	private Vector3 m_pairOne = new Vector3(0f, 0f, -5f);
	private Vector3 m_pairTwo = new Vector3(300f, 0f, -5f);
	private Vector3 m_pairThree = new Vector3(600f, 0f, -5f);

	private Vector3 m_spawnOrigin = new Vector3 (825f, 0f, -5f);

	// Use this for initialization
	public void Start () {
        Debug.Log ("Creating obstacles, adjusting positions");
		GameObject pairOne = this.createObstacle ();
		GameObject pairTwo = this.createObstacle ();
		GameObject pairThree = this.createObstacle ();
		pairOne.transform.position = m_pairOne;
		pairTwo.transform.position = m_pairTwo;
		pairThree.transform.position = m_pairThree;
        PauseObstacles();
        StopObstacles();
    }

	public void Reset () {
        Debug.Log ("Reset. Destroying all obstacles, starting obstacle management again");
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
        Debug.Log ("removing objects from list");
		foreach (GameObject obstacle in m_obstacleObjects) {
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
	public void destroyObstacle(Collider2D obstacle) {
		if(obstacle.transform.parent.gameObject != null)
        {
            m_obstacleObjects.Remove (obstacle.transform.parent.gameObject);
    		Destroy (obstacle.transform.parent.gameObject);
        }
	}

	/**
	 * Create a new collider at Vector3 m_spawnOrigin.
	 */
	public GameObject createObstacle() {
        Debug.Log ("Creating obstacle");
		GameObject newObs = (GameObject) Instantiate (
			m_obstaclePrefab,
			m_spawnOrigin,
			Quaternion.identity
		);
		newObs.transform.parent = this.transform;
		m_obstacleObjects.Add (newObs);
		return newObs;
	}
    
    //Pause mvoement of all current obstacles in scene
    public void PauseObstacles()
    {
        Debug.Log ("Pausing obstacles");
        foreach (GameObject obs in m_obstacleObjects)
        {
            ObstacleMovement om = obs.GetComponent<ObstacleMovement>();
            om.paused = true;
        }
    }
    
    public void UnpauseObstacles()
    {   
        Debug.Log ("Unpausing obstacles");
        foreach (GameObject obs in m_obstacleObjects)
        {
            ObstacleMovement om = obs.GetComponent<ObstacleMovement>();
            om.paused = false;
        }
    }
    
    public void StartObstacles()
    {
        Debug.Log ("Starting obstacles");
        foreach (GameObject obs in m_obstacleObjects)
        {
            ObstacleMovement om = obs.GetComponent<ObstacleMovement>();
            om.started = true;
        }
    }
    
    public void StopObstacles()
    {
        Debug.Log ("Stopping obstacles");
        foreach (GameObject obs in m_obstacleObjects)
        {
            ObstacleMovement om = obs.GetComponent<ObstacleMovement>();
            om.started = false;
        }
    }   
}
