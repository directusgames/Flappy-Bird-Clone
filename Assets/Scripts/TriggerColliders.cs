using UnityEngine;
using System.Collections;

public class TriggerColliders : MonoBehaviour {
	public ObstacleManager m_obstacleManager;

	void OnTriggerEnter2D(Collider2D other) {
		switch (this.tag) {
			case "createCollide":
				break;
		}
		Debug.Log ("collision start");
	}

	void OnTriggerExit2D(Collider2D other) {
		switch (this.tag) {
			case "destroyCollide":
				break;
		}
		Debug.Log ("collision exit");
	}
}
