using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void StartGame()
    {
		// Start menu.

        SceneManager.LoadScene(1);
    }
}
