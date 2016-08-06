using UnityEngine;
using System.Collections;

public class TriggerColliders : MonoBehaviour {
	public ObstacleManager m_obstacleManager;

	void OnTriggerEnter2D(Collider2D other) {
		switch (this.tag) {
			case "createCollide":	
				Debug.Log ("Creating new obstacle on createCollide");
				m_obstacleManager.createObstacle ();
				break;
		}
		Debug.Log ("collision start");
	}

	void OnTriggerExit2D(Collider2D other) {
		switch (this.tag) {
			case "destroyCollide":
				Debug.Log ("Creating new obstacle on createCollide");
				m_obstacleManager.destroyObstacle (other);
				break;
		}
		Debug.Log ("collision exit");
	}
}
