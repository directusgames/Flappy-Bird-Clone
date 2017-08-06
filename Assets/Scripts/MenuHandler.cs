using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuHandler : MonoBehaviour {
	public PlayerMovement m_playerMovement;
	public ObstacleManager m_obstacleManager;
    public ColliderGenerator colliderGenerator;
    public ColliderDestroyer colliderDestroyer;
    public ScrollBackground scrollBG;

	public GameObject m_mainMenu;
	public GameObject m_deathMenu;
    
	public Text txtScore;

	public Text m_deathMsg;
	public string[] m_deathMsgs;
	public int m_deathPtr;

	public Text m_score;
	public Text m_highScore;
    
	// Use this for initialization
	void Start () {
	}

	public void startGame() {
		// Disable all menus when the game commences.
		foreach (GameObject menu in GameObject.FindGameObjectsWithTag("canvas")) {
		  menu.SetActive(false);
		}
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;
        
		m_mainMenu.SetActive (false);
		m_playerMovement.Start ();
        
        colliderGenerator.GetComponent<BoxCollider2D>().enabled = true;
        colliderDestroyer.GetComponent<BoxCollider2D>().enabled = true;
                
		m_obstacleManager.Reset();
		colliderGenerator.create = true;
        
		m_playerMovement.alive = true;
		m_playerMovement.Unfreeze ();
		m_playerMovement.enabled = true;
		m_obstacleManager.StartObstacles();
		m_obstacleManager.UnpauseObstacles();
        txtScore.enabled = true;
		// SceneManager.LoadScene(1);
	}

	// These menu methods are bad form.
	// They just toggle one another.
	// Doesn't scale very well.
	public void mainMenu() {
		GameObject.Find ("Player").GetComponent<PlayerMovement> ().Start ();
		m_mainMenu.SetActive (true);
		txtScore.enabled = false;
		m_deathMenu.SetActive (false);
	}

	public void settingsMenu() {
		m_mainMenu.SetActive (false);
	}

	public void deathMenu() {
		// Hack for ensuring different message to last death.
		int prevDeathPtr = m_deathPtr;
		m_deathPtr = Random.Range (0, m_deathMsgs.Length);
		if (prevDeathPtr == m_deathPtr) {
			if (m_deathPtr == m_deathMsgs.Length) {
				m_deathPtr = 0;
			} else {
				m_deathPtr++;
			}
		}
		m_deathMsg.text = m_deathMsgs [m_deathPtr];
        scrollBG.stopped = true;
		m_deathMenu.SetActive (true);
		txtScore.enabled = false;
		m_score.text = txtScore.text;
		m_highScore.text = PlayerPrefs.GetInt("highScore").ToString();
	}
    
    public void CloseApplication()
    {
        Application.Quit ();
    }
}
