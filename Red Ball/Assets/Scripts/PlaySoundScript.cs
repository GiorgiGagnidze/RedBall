using UnityEngine;


public class PlaySoundScript : MonoBehaviour {
	public static PlaySoundScript Instance;

	public AudioClip starSound;
	public AudioClip checkPointSound;
	public AudioClip trapSound;
	public AudioClip waterSound;
	public AudioClip fallSound;
	public AudioClip finishSound;
	void Awake()
	{
		// Register the singleton
		if (Instance != null)
		{
			Debug.LogError("Multiple instances of PlaySoundScript!");
		}
		Instance = this;
		DontDestroyOnLoad(transform.gameObject);
	}
	
	public void MakeFinishSound()
	{
		MakeSound(finishSound);
	}
	
	public void MakeFallSound()
	{
		MakeSound(fallSound);
	}
	
	public void MakeWaterSound()
	{
		MakeSound(waterSound);
	}
	
	public void MakeTrapSound()
	{
		MakeSound(trapSound);
	}
	
	public void MakeStarSound()
	{
		MakeSound(starSound);
	}
	
	public void MakeCheckPointSound()
	{
		MakeSound(checkPointSound);
	}
	
	private void MakeSound(AudioClip originalClip)
	{
		// As it is not 3D audio clip, position doesn't matter.
		if (AppData.isSoundOn){
			AudioSource source = GetComponent<AudioSource>();
			source.clip = originalClip;
			source.loop = false;
			source.Play();
		}
	}
}
