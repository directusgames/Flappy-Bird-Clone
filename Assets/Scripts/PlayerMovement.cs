using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public ObstacleManager m_obstacleManager;

	public GameObject m_canvas;

    public float jumpForce;
    public float maxVelocityMag;
    public float maxVelocity;

	public Vector3 m_spawnPos = new Vector3(-183f, 3f, -1f);
    
	private Rigidbody2D rigid;
    
	public bool alive = false;
	public float m_gravityScale = 100f;

	public void Start () {
		this.transform.position = m_spawnPos;
		alive = false; // Don't start until user has elected to start.
		rigid = GetComponent<Rigidbody2D>();
		Freeze ();
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
		m_obstacleManager.m_paused = true;
		alive = false;
		// Death animation.
		// Sound effect trigger - if sound enabled.
		// UI score display?
		// Other stuff?
	}
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.transform.parent.gameObject.tag == "Obstacle")
        {
			// Player has hit a randomly generated obstacle.
			Death();
			m_canvas.SetActive (true);
        }
    }
}
