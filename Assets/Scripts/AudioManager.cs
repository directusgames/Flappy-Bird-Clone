using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	public void setVolume(float volume) { AudioListener.volume = volume; }
}