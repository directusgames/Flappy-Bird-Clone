using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FontJitter : MonoBehaviour {

	public Text[] jitterText; // Text objects to manipulate transform of.
	public Vector2[] jitterOrigPos; // Array of the original (x,y,z) of text objects transforms.

	public float textLength = 11; // f1 l2 a3 p4 p5 y6 b7 e8 a9 r10 d11

	public float randScale = 0.25f; // Used to lessen impact of random jitter.

	public int[] randRange = {-15, 15}; // Used to generate random x and y.

	public Vector2 clampRange = new Vector2(-15f, 15f); // probably float[] makes more sense?

	// Use this for initialization
	void Start () {
		textLength = jitterText.Length;

		for (int i = 0; i < textLength; i++) {
			// Store orig pos. - deprecate?
			jitterOrigPos[i].x = jitterText[i].transform.position.x;
			jitterOrigPos[i].y = jitterText[i].transform.position.y;
		}
	}

	/*
	 * Randomly bump the x/y value up or down.
	 * Clamping to within a certain range of its original position.
	 */
	void Update () {
		for (int i = 0; i < textLength; i++) {
			Vector3 pos = jitterText[i].transform.position;

			// Determine random x and y.
			// float xRand = randScale * Random.Range (randRange[0], randRange[1]);
			float yRand = Time.deltaTime * (Random.Range (randRange[0], randRange[1]));
			// Debug.Log ("xRand: " + xRand + " yRand: " + yRand);

			// Clamp random x and y.
			// float xPos = Mathf.Clamp(pos.x + xRand, jitterOrigPos[i].x + clampRange[0], jitterOrigPos[i].x + clampRange[1]); 
			float yPos = Mathf.Clamp(pos.y + yRand, jitterOrigPos[i].y + clampRange[0], jitterOrigPos[i].y + clampRange[1]);

			// Debug.Log ("xPos: " + xPos + " yPos: " + yPos);

			// Assign random jitter +/- on x/y.
			jitterText[i].transform.position = new Vector3 (pos.x, yPos, pos.z);
		}
	}
}
