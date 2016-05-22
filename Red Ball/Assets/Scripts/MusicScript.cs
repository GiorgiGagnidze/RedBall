using UnityEngine;

public class MusicScript : MonoBehaviour {
	public static MusicScript Instance;
	void Awake()
	{
		// Register the singleton
		if (Instance != null)
		{
			Debug.LogError("Multiple instances of MusicScript!");
		}
		Instance = this;
		if (AppData.isMusicOn)
			 GetComponent<AudioSource>().Play();
		DontDestroyOnLoad(transform.gameObject);
	}
	
	public void startMusic() 
	{
		 GetComponent<AudioSource>().Play();
	}
	
	public void stopMusic() 
	{
		 GetComponent<AudioSource>().Stop();
	}
}
