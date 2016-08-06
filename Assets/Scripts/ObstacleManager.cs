using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleManager : MonoBehaviour {
	public float m_speed;
	public List<GameObject> m_obstacleObjects;
	public bool m_started = true;
	public bool m_paused = false;

	public BoxCollider2D m_createCollider;
	public BoxCollider2D m_destroyCollider;

	private Vector3 m_spawnOrigin = new Vector3 (400f, 0f, 0f);

	// Use this for initialization
	void Start () {
		m_obstacleObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Obstacle"));
	}
	
	// Update is called once per frame
	void Update () {
		if (m_started && !m_paused) {
			foreach (GameObject obstacle in m_obstacleObjects) {
				// Update all game objects positions
				Vector3 curPos = obstacle.transform.position;
				curPos.x -= m_speed * Time.deltaTime;
				obstacle.transform.position = curPos;
			}
		}
		// If trigger event on create collider
			// Create new Obstacle prefab
			// Add it game object array 
		// If trigger event on destruction obstacle
			// Detect object causing trigger and delete
			// Remove from game object list
	}

	void destroyObstacle(GameObject obstacle) {

	}

	void createObstacle() {
		// Create @ spawnOrigin.
	}
}
