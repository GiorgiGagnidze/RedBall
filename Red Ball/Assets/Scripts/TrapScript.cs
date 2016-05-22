using UnityEngine;
using System.Collections;

public class TrapScript : MonoBehaviour {
    public ParticleSystem smokeEffect;

	public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" ) 
		{
            instantiate(smokeEffect, collision.transform.position);
			if (AppData.CurrentVerifiedScore > 0){
                AppData.CurrentVerifiedScore -= 100;
            }
            AppData.CurrentScore = AppData.CurrentVerifiedScore;
            for (int i = 0; i < AppData.VerifiedStars.Count; i++)
            {
                AppData.Stars[i].Show = AppData.VerifiedStars[i].Show;
            }
            Destroy(collision.gameObject);
            PlaySoundScript.Instance.MakeTrapSound();
            StartCoroutine(WaitAndLoad(0.5F));
		}
    }
    
    IEnumerator WaitAndLoad(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        Application.LoadLevel(AppData.LEVEL_NAME+AppData.CurrentLevel);
    }
    
    private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position)
    {
        ParticleSystem newParticleSystem = Instantiate(
        prefab,
        position,
        Quaternion.identity
        ) as ParticleSystem;
    
        // Make sure it will be destroyed
        Destroy(
        newParticleSystem.gameObject,
        newParticleSystem.startLifetime
        );
    
        return newParticleSystem;
    }
}
