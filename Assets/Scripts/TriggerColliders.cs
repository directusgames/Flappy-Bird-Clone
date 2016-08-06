using UnityEngine;
using System.Collections;

public class TriggerColliders : MonoBehaviour {
	public ObstacleManager m_obstacleManager;
	public bool m_create = false;
	public bool m_destroy = false;

	void start() {
    
	}

	/**
	 * A bit hacky, but basically this is triggering twice,
	 * beacuse our 'obstacle' is composed of 2 different colliders (top & bottom).
	 */
	void OnTriggerExit2D(Collider2D other) {
		switch (this.tag) {
			case "createCollide":
				if (m_create) {
					Debug.Log ("Creating new obstacle on createCollide");
					m_obstacleManager.createObstacle ();
					m_create = false;
				} else {
					m_create = true;
				}
				break;	

			case "destroyCollide":
				if (m_destroy) {
					Debug.Log ("Destroying new obstacle on createCollide");
					m_obstacleManager.destroyObstacle (other);
					m_destroy = false;
				} else {
					m_destroy = true;
				}
				break;
		}
	}
}
