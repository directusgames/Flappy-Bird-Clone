using UnityEngine;
using System.Collections;

public class ScrollBackground : MonoBehaviour {
    
    public float scrollSpeed;
    private float scrollSpeedBase;
    public Renderer rend;
    public bool stopped;
    
	// Use this for initialization
	void Start () {
        
        //scrollSpeedBase = scrollSpeed;
        rend = GetComponent<Renderer>();
	
	}
	
	// Update is called once per frame
	void Update () {	    
        
        if(!stopped)
        {
            //scrollSpeed += 0.00001f; //increase speed to keep up with razors
            float offset = Time.time * scrollSpeed;
            rend.material.SetTextureOffset("_MainTex", new Vector2(offset,0));
	    }
//        else
//        {
//            scrollSpeed = scrollSpeedBase;
//        }
    }
}
