using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleManager : MonoBehaviour {
	public float m_obstacleMovement;
	public List<GameObject> m_obstacleObjects;
	public bool m_started = true;

	// Use this for initialization
	void Start () {
		m_obstacleObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Obstacle"));
	}
	
	// Update is called once per frame
	void Update () {
		// If game has started && !paused
			// Update all game objects positions
				// all game objects + m_obstacleMovement
		// If trigger event on create collider
			// Create new Obstacle prefab
			// Add it game object array 
		// If trigger event on destruction obstacle
			// Detect object causing trigger and delete
			// Remove from game object list
	}
}
