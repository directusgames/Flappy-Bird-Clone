using UnityEngine;
using System.Collections;

public class PlayerRotation : MonoBehaviour {
	public bool m_rotate = false;
    public float rotationSpeed;
    
	// Use this for initialization
	void Start () {
		MenuHandler.StartRound += StartRound;
	}

	public void StartRound() {
		m_rotate = true;
	}

	public void EndRound() {

	}
	
	// Update is called once per frame
	void Update () {
		if (m_rotate) {	gameObject.transform.Rotate(new Vector3(0,0, rotationSpeed * Time.deltaTime)); }
	}
}
