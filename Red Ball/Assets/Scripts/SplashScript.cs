using UnityEngine;

public class SplashScript : MonoBehaviour {

	public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player" && collision.transform.position.y+0.25 > transform.position.y)
        {
            PlaySoundScript.Instance.MakeWaterSound();
        }
    }
}
