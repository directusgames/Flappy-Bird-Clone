using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleManager : MonoBehaviour {
	public float m_speed;
	public List<GameObject> m_obstacleObjects;
	public GameObject m_obstaclePrefab;
	public BoxCollider2D m_createCollider;
	public BoxCollider2D m_destroyCollider;
    public float maxVertChange, minVertChange;

    public Vector3 m_spawnOrigin;

	// Poor form, but quick.
	private Vector3 m_pairOne = new Vector3(0f, 0f, 1f);
	private Vector3 m_pairTwo = new Vector3(300f, 0f, 1f);
	private Vector3 m_pairThree = new Vector3(600f, 0f, 1f);
    
    private int obstNum = 0;

	// Use this for initialization
	public void Start () {
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
        //.Log ("Creating obstacle. Time:" + Time.time);
		GameObject newObs = (GameObject) Instantiate (
			m_obstaclePrefab,
			m_spawnOrigin,
			Quaternion.identity
		);
        
        //Randomise obstacle height
        AdjustObstacleHeight(newObs, Random.Range (minVertChange, maxVertChange));
		newObs.transform.parent = this.transform;
        newObs.name = "Pair " + obstNum;
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
        Transform top = obs.GetComponent<ObstacleMovement>().topObs.transform;
        Transform bot = obs.GetComponent<ObstacleMovement>().botObs.transform;
        
        //Change the scale of the obstacle as well as the position which changes when unity scales the object. This position change
        //moves it back to its original placement.
        if(deltaHeight >= 0)
        {            
            top.localScale = new Vector3(top.localScale.x, top.localScale.y + deltaHeight, top.localScale.z);
            top.position = new Vector3(top.position.x, top.position.y - deltaHeight/2, top.position.z);
            
            bot.localScale = new Vector3(bot.localScale.x, bot.localScale.y - deltaHeight, bot.localScale.z);            
            bot.position = new Vector3(bot.position.x, bot.position.y - deltaHeight/2, bot.position.z);
        }
        else
        {
            deltaHeight = Mathf.Abs(deltaHeight);
            top.localScale = new Vector3(top.localScale.x, top.localScale.y - deltaHeight, top.localScale.z);            
            top.position = new Vector3(top.position.x, top.position.y + deltaHeight/2, top.position.z);
            
            bot.localScale = new Vector3(bot.localScale.x, bot.localScale.y + deltaHeight, bot.localScale.z);
            bot.position = new Vector3(bot.position.x, bot.position.y + deltaHeight/2, bot.position.z);
        }
    }
}
