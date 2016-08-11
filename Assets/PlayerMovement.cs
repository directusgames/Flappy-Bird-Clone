using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    
	public GameObject m_obstacleParent;
	private ObstacleManager m_obstacleManager;
    public float jumpForce;
    public float maxVelocityMag;
    public float maxVelocity;
    Rigidbody2D rigid;
    bool alive;
    
	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        alive = true;
		m_obstacleManager = m_obstacleParent.GetComponent<ObstacleManager> ();
	}
	
	// Update is called once per frame
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
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.transform.parent.gameObject.tag == "Obstacle")
        {
			// Player has hit a randomly generated obstacle.
            alive = false;
			m_obstacleManager.m_paused = true;
        }
    }
}
