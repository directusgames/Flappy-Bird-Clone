using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    
    public float jumpForce;
    public float maxVelocityMag;
    public float maxVelocity;
    Rigidbody2D rigid;
    
	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
    
        if(Input.GetKeyDown (KeyCode.Space))
        {
           rigid.velocity = Vector2.zero;
           rigid.AddForce(new Vector2(0f,1f) *  jumpForce);
        }
        
        //Debug.Log (rigid.velocity);
	}
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log (coll.gameObject.tag);
        if(coll.gameObject.tag == "Obstacle")
        {
            //reset game
        }
    }
}
