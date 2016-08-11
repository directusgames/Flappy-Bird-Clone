using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleManager : MonoBehaviour {
	public float m_speed;
	public List<GameObject> m_obstacleObjects;
	public bool m_started = true;
	public bool m_paused = false;

	public GameObject m_obstaclePrefab;

	public BoxCollider2D m_createCollider;
	public BoxCollider2D m_destroyCollider;

	// Poor form, but quick.
	private Vector3 m_pairOne = new Vector3(0f, 0f, -5f);
	private Vector3 m_pairTwo = new Vector3(300f, 0f, -5f);
	private Vector3 m_pairThree = new Vector3(600f, 0f, -5f);

	private Vector3 m_spawnOrigin = new Vector3 (825f, 0f, -5f);

	// Use this for initialization
	void Start () {
		m_obstacleObjects = new List<GameObject>(
			GameObject.FindGameObjectsWithTag("Obstacle")
		);

		GameObject pairOne = this.createObstacle ();
		GameObject pairTwo = this.createObstacle ();
		GameObject pairThree = this.createObstacle ();
		pairOne.transform.position = m_pairOne;
		pairTwo.transform.position = m_pairTwo;
		pairThree.transform.position = m_pairThree;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_started && !m_paused) {
			foreach (GameObject obstacle in m_obstacleObjects) {
                // Update all game objects positions
                for (int i = 0; i < 2; i++) {
                    Rigidbody2D obstacleBody = obstacle.transform.GetChild (i).GetComponent<Rigidbody2D>();
                    Vector3 curPos = obstacleBody.position;
                    curPos.x -= m_speed * Time.deltaTime;
                    obstacleBody.position = curPos;
                }
			}
		}
	}

	void Reset () {
		foreach (GameObject obstacle in m_obstacleObjects) {
			m_obstacleObjects.Remove (obstacle);
			destroyObstacle (obstacle);
		}
	}

	public void destroyObstacle(Collider2D obstacle) {
		GameObject obstacleContainer = obstacle.transform.parent.gameObject;
		m_obstacleObjects.Remove (obstacleContainer);
		Destroy (obstacleContainer);
	}

	public GameObject createObstacle() {
		GameObject newObs = (GameObject) Instantiate (
			m_obstaclePrefab,
			m_spawnOrigin,
			Quaternion.identity
		);
		newObs.transform.parent = this.transform;
		m_obstacleObjects.Add (newObs);
		return newObs;
	}
}
