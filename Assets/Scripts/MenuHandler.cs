﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuHandler : MonoBehaviour {
	public PlayerMovement m_playerMovement;
	public ObstacleManager m_obstacleManager;
    public ColliderGenerator colliderGenerator;
	public GameObject m_canvas;
    public Text txtScore;
    
    bool firstRun;

	// Use this for initialization
	void Start () {
    
        firstRun = true;
	}

	public void startGame() {
		m_canvas.SetActive (false);
		m_playerMovement.Start ();
		if(!firstRun)
        {
            m_obstacleManager.Reset();
        }
        else
        {
            firstRun = false;
        }
		m_playerMovement.alive = true;
		m_playerMovement.Unfreeze ();
		m_playerMovement.enabled = true;
        colliderGenerator.create = true;
		m_obstacleManager.StartObstacles();
		m_obstacleManager.UnpauseObstacles();
        txtScore.enabled = true;
		// SceneManager.LoadScene(1);
	}
		
	// Update is called once per frame
	void Update () {
	
	}
}