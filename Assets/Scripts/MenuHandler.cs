﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuHandler : MonoBehaviour {
	public PlayerMovement m_playerMovement;
	public ObstacleManager m_obstacleManager;
	public GameObject m_canvas;
    public Text txtScore;

	// Use this for initialization
	void Start () {
	}

	public void startGame() {
		m_canvas.SetActive (false);
		m_playerMovement.Start ();
		m_obstacleManager.Reset();
		m_playerMovement.alive = true;
		m_playerMovement.Unfreeze ();
		m_playerMovement.enabled = true;
		m_obstacleManager.m_started = true;
		m_obstacleManager.m_paused = false;
        txtScore.enabled = true;
		// SceneManager.LoadScene(1);
	}
		
	// Update is called once per frame
	void Update () {
	
	}
}
