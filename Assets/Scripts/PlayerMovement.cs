using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public ObstacleManager m_obstacleManager;
    public ColliderGenerator collGen;
    public ColliderDestroyer collDes;
    
	public MenuHandler m_menuHandler;
	public GameObject m_mainMenu;
	public GameObject deathExplosion;
    
	// Don't think the player object should set the score. *shrug*.
    public Text txtScore;
    public int score;

    public float jumpForce;

	public Vector3 m_spawnPos = new Vector3(-183f, 3f, -1f);
    
	private Rigidbody2D rigid;
    
	public bool alive = false;
	public float m_gravityScale = 100f;

	public void Start () {
        score = 0;        
        txtScore.text = "" + score;
		this.transform.position = m_spawnPos;
        
		alive = false; // Don't start until user has elected to start.
   
        rigid = GetComponent<Rigidbody2D>();
        Freeze ();
        rigid.velocity = Vector2.zero; //set velocity to 0 otherwise results in unexpected behaviour unpon reset.
	}

	public void Freeze() {
		rigid.gravityScale = 0;
	}

	public void Unfreeze() {
		rigid.gravityScale = m_gravityScale;
	}
	
	void Update () {
        if (alive)
        {
            if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
               rigid.velocity = Vector2.zero;
               rigid.AddForce(new Vector2(0f, 1f) *  jumpForce);
            }
        }        //Debug.Log (rigid.velocity);
	}

	/**
	 * I think rather than the player having knowledge of ObstacleManager, etc.
	 * A global event could be triggered.
	 * Then the logic for handling player death can be inside of ObstacleManager.
	 * Or, perhaps we'd have a separate class/script that does this.
	 */
	void Death() {
		// Pause obstacle movement
        if (alive)
        {
            alive = false;

            //Create death explosion effect
            GameObject deathEx = (GameObject) Instantiate(deathExplosion, transform.position, Quaternion.identity);
            
            //Slow time and lower delta time step so we don't get low fps.
            Time.timeScale = 0.1f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            
            Rigidbody2D[] rigids = null;
            
            //Iterate through all obstacle pairs in the list, get their rigid bodies and apply an explosion force to them            
            foreach(GameObject obstaclePair in m_obstacleManager.m_obstacleObjects)
            {
                rigids = obstaclePair.GetComponentsInChildren<Rigidbody2D>();
                foreach(Rigidbody2D rig in rigids)
                {
                    //rig.transform.parent = null;
                    rig.isKinematic = false;                
                    //AddExplosionForce(rig, 1000f, transform.position, 5000f);
                    if(rig.gameObject.name == "Top")
                    {
                        rig.velocity = new Vector2(1,1) * 500f;
                        Debug.Log ("Top " + rig.velocity);
                        
                    }
                    else if(rig.gameObject.name == "Bottom")
                    {
                        rig.velocity = new Vector2(1,1) * 500f;
                        Debug.Log ("Bot " + rig.velocity);
                    }
                    Debug.Log (rigid.name + ": " + rigid.velocity);
                    rig.angularVelocity = Random.Range(150f, 300f);
                }
            }
            
            // turn off player sprite for now
            // GetComponent<SpriteRenderer>().enabled = false;
           
            // Turn off creation/destruction collider as flying obstacles could drigger it.
            collGen.create = false;
            collGen.GetComponent<BoxCollider2D>().enabled = false;
            collDes.GetComponent<BoxCollider2D>().enabled = false;
            
            // Stop the obstacles from perpetually moving left.
            m_obstacleManager.PauseObstacles();
            
			// Disable the scoring collider, so that it doesn't fly into the user.
			GameObject[] pointColliders = GameObject.FindGameObjectsWithTag("PointsCollider");
			foreach (GameObject pointCollider in pointColliders) {
				pointCollider.SetActive (false);
			}

			if (score > PlayerPrefs.GetInt ("highScore")) {
				PlayerPrefs.SetInt ("highScore", score);
			}

			m_menuHandler.deathMenu ();
         
            // Death animation.
    		// Sound effect trigger - if sound enabled.
    		// UI score display?
    		// Other stuff?
    	}
    }
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Obstacle")
        {
			// Player has hit a randomly generated obstacle.
			Death();
        }
    }
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "PointsCollider")
        {
            score++;
            txtScore.text = "" + score;
        }
    }
    
    public static void AddExplosionForce (Rigidbody2D body, float expForce, Vector3 expPosition, float expRadius)
    {
        var dir = (body.transform.position - expPosition);
        float calc = 1 - (dir.magnitude / expRadius);
        if (calc <= 0) {
            calc = 0;       
        }
        
        body.AddForce (dir.normalized * expForce * calc);
    }
}