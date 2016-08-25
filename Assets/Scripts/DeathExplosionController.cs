using UnityEngine;
using System.Collections;

public class DeathExplosionController : MonoBehaviour {
    
    public float lifetime;
    
    private float curTime;
    
	// Use this for initialization
	void Start () {
    
        curTime = 0f;	
	}
	
	// Update is called once per frame
	void Update () {
        
        curTime += Time.deltaTime;
        
        if(curTime > lifetime)
        {
            GetComponent<ParticleSystem>().Stop();
        }
        
	}
}
