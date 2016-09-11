﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
    public ObstacleManager m_obstacleManager;
    public ColliderGenerator collGen;
    public ColliderDestroyer collDes;
    
	public GameObject m_canvas, deathExplosion;

	public delegate void DeathAction();
	public static event DeathAction OnDeath;

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
        }
        //Debug.Log (rigid.velocity);
	}

	void Death() {
		// Pause obstacle movement
        if(alive)
        {
            alive = false;
            //Create death explosion effect
            // GameObject deathEx = (GameObject) Instantiate(deathExplosion, transform.position, Quaternion.identity);
			Instantiate(deathExplosion, transform.position, Quaternion.identity);
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
                        // Debug.Log ("Top " + rig.velocity);
                        
                    }
                    else if(rig.gameObject.name == "Bottom")
                    {
                        rig.velocity = new Vector2(1,1) * 500f;
                        // Debug.Log ("Bot " + rig.velocity);
                    }
                    // Debug.Log (rigid.name + ": " + rigid.velocity);
                    rig.angularVelocity = Random.Range(150f, 300f);
                }
            }
            
            //turn off player sprite for now
            //GetComponent<SpriteRenderer>().enabled = false;
           
            //Turn off creation collider as flying obstacles could drigger it           
            collGen.create = false;
            collGen.GetComponent<BoxCollider2D>().enabled = false;
            collDes.GetComponent<BoxCollider2D>().enabled = false;
            
            //pause horizontal obstacle movement
    		m_obstacleManager.PauseObstacles();

			/**
			 * Called by the player on player's death.
			 * Calls a group of methods that match the delegate 'void DeathAction()' method.
			 * This allows the menu system to define a method that is only called upon PlayerDeath.
			 * 
			 * PlayerMovement (-> Refactor? PlayerController, PlayerAI, Player).
			 *   -> i.e. does more than just movement.
			 */
			if (OnDeath == null) {
				Debug.Log ("Error, no subscribed events to Player Death.");
			} else {
				Invoke("OnDeath", 1.5f);
			}
         
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
        if(coll.gameObject.tag == "PointsCollider")
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

    public void ShowMainMenu()
    {
        m_canvas.SetActive (true);
        txtScore.enabled = false;
    }
    
}
