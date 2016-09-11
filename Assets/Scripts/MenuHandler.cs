using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuHandler : MonoBehaviour {
	public PlayerMovement m_playerMovement;
	public ObstacleManager m_obstacleManager;
    public ColliderGenerator colliderGenerator;
    public ColliderDestroyer colliderDestroyer;

	public GameObject m_canvas;
	public GameObject m_tauntCanvas;

    public Text txtScore;
    
    bool firstRun;

	public delegate void StartRoundEvent();
	public static event StartRoundEvent StartRound;

	// Use this for initialization
	void Start () {
		PlayerMovement.PostPlayerExplode += ShowMainMenu;
		PlayerMovement.PlayerDeath += ShowDeathMenu;
		m_tauntCanvas.SetActive (false);
        firstRun = true;
	}

	public void ShowDeathMenu() {
		m_tauntCanvas.SetActive (true);
	}

	/**
	 * Registered as a callback for PostPlayerExplode.
	 */
	public void ShowMainMenu() {
		m_tauntCanvas.SetActive (false);
		m_canvas.SetActive (true);
		txtScore.enabled = false;
	}
			
	// Called by On Click() of "Start Button" game object.
	public void startRound() {
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;
		txtScore.text = "0";
		txtScore.enabled = true;
		m_canvas.SetActive (false);
        colliderGenerator.GetComponent<BoxCollider2D>().enabled = true;
        colliderDestroyer.GetComponent<BoxCollider2D>().enabled = true;
        
		if(!firstRun) {
            colliderGenerator.create = false;
            m_obstacleManager.Reset();
        } else {
            firstRun = false;
            colliderGenerator.create = true;
        }
        
		// Run all StartRoundEvent method callbacks.
		if (StartRound != null) {
			StartRound ();
		}
	}
		
	// Update is called once per frame
	void Update () {}
}
