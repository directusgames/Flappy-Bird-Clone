using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {
	public AudioSource m_mainTrack;
	public Toggle m_toggle;
	public Image m_background;
	public Text m_copy;

	public void Start() {
		m_toggle = this.GetComponent<Toggle> ();
	}

	public void setVolume(float volume) { AudioListener.volume = volume; }

	public void muteTrack() {
		if (m_mainTrack.isPlaying) { 
			m_copy.text = "play";
			m_mainTrack.Pause ();
		} else {
			m_copy.text = "mute";
			m_mainTrack.Play (); 
		}
		ColorBlock tempBlock = new ColorBlock();
		tempBlock = this.GetComponent<Toggle> ().colors;
		tempBlock.normalColor = this.GetComponent<Toggle> ().colors.pressedColor;
		tempBlock.pressedColor = this.GetComponent<Toggle> ().colors.normalColor;
		this.GetComponent<Toggle> ().colors = tempBlock;
	}
}