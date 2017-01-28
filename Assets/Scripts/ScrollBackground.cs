using UnityEngine;
using System.Collections;

public class ScrollBackground : MonoBehaviour {
    
    public float scrollSpeed;
    public Renderer rend;
    public bool stopped;
    
	// Use this for initialization
	void Start () {
        
        rend = GetComponent<Renderer>();
	
	}
	
	// Update is called once per frame
	void Update () {
	    
        if(!stopped)
        {
            float offset = Time.time * scrollSpeed;
            rend.material.SetTextureOffset("_MainTex", new Vector2(offset,0));
	    }
    }
}
