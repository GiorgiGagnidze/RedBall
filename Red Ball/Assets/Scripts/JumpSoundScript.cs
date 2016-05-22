using UnityEngine;

public class JumpSoundScript : MonoBehaviour {
	public static JumpSoundScript Instance;
	public AudioClip jumpSound;
	void Awake()
	{
		// Register the singleton
		if (Instance != null)
		{
			Debug.LogError("Multiple instances of SoundScript!");
		}
		Instance = this;
		DontDestroyOnLoad(transform.gameObject);
	}
	
	public void MakeJumpSound()
	{
		MakeSound(jumpSound);
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
