using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuHandler : MonoBehaviour {
	public PlayerMovement m_playerMovement;
	public ObstacleManager m_obstacleManager;
    public ColliderGenerator colliderGenerator;
    public ColliderDestroyer colliderDestroyer;

	public GameObject m_mainMenu;
	public GameObject m_settingsMenu;
	public GameObject m_deathMenu;
    
	public Text txtScore;
	public Text m_deathScore;
	public Text m_deathHighScore;
    
    bool firstRun;

	// Use this for initialization
	void Start () {
    	firstRun = true;
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
        
        
		if(!firstRun)
        {
            colliderGenerator.create = false;
            m_obstacleManager.Reset();
        }
        else
        {
            firstRun = false;
            colliderGenerator.create = true;
        }
        
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
		m_mainMenu.SetActive (true);
		txtScore.enabled = false;
		m_settingsMenu.SetActive (false);
		m_deathMenu.SetActive (false);
	}

	public void settingsMenu() {
		m_mainMenu.SetActive (false);
		m_settingsMenu.SetActive (true);
	}

	public void deathMenu() {
		m_deathMenu.SetActive (true);
		m_deathScore.text = "Score: " + txtScore.text;
		m_deathHighScore.text = "High Score: " + PlayerPrefs.GetInt ("highScore");
	}
    
    public void CloseApplication()
    {
        Application.Quit ();
    }
}
