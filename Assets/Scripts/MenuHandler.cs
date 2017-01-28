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
    
	public Text deathTitle;
	public int deathTitlePtr = 0;
	public Text txtScore;

	public string[] deathPhrases = {
		"Shaved!", "RIP Beard", "Clean shave = early grave",
		"De-bearded :'(", "rekt"
	};

	// Var names could be better:
	public Text m_deathScore; // Score title.
	public Text m_deathScoreValue; // Score value.
	public Text m_deathHighScore; // High score title.
	public Text m_deathHighScoreValue; // High score value;
    
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
		m_mainMenu.SetActive (true);
		txtScore.enabled = false;
		m_deathScoreValue.enabled = false;
		m_deathHighScoreValue.enabled = false;
		m_settingsMenu.SetActive (false);
		m_deathMenu.SetActive (false);
	}

	public void settingsMenu() {
		m_mainMenu.SetActive (false);
		m_settingsMenu.SetActive (true);
	}

	public void deathMenu() {
		int priorPtr = deathTitlePtr;
		// Randomly change death message.
		deathTitlePtr = Random.Range (0, deathPhrases.Length);
		// Hack for preventing randomly receiving the exact same death message.
		if (deathPhrases.Length > 1 && priorPtr == deathTitlePtr) { 
			if ((deathTitlePtr + 1) > deathPhrases.Length) {
				deathTitlePtr = 0;
			} else {
				deathTitlePtr++;
			}
		}
		deathTitle.text = deathPhrases [deathTitlePtr];

		m_deathMenu.SetActive (true);
		txtScore.enabled = false;

		m_deathScoreValue.text = txtScore.text;
		m_deathScoreValue.enabled = true;

		m_deathHighScoreValue.text = PlayerPrefs.GetInt ("highScore").ToString();
		m_deathHighScoreValue.enabled = true;
	}
    
    public void CloseApplication()
    {
        Application.Quit ();
    }
}
