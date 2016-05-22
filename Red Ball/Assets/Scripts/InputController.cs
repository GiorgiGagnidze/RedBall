using UnityEngine;
using System;
using System.Collections;

public class InputController : MonoBehaviour
{
    public event Action<float> OnMovementX;
    public event Action<float> OnMovementY;
    public float bottom;
    private bool inAction = false;

    // Update is called once per frame
    protected virtual void Update()
    {
        float directionX = Input.GetAxis("Horizontal");
        if (OnMovementX != null) OnMovementX(directionX);
        float directionY= Input.GetAxis("Vertical");
        if (OnMovementY != null) OnMovementY(directionY);
        
        if (transform.position.y < bottom && !inAction) {
            inAction = true;
            if (AppData.CurrentVerifiedScore > 0){
                AppData.CurrentVerifiedScore -= 100;
            }
            AppData.CurrentScore = AppData.CurrentVerifiedScore;
            for (int i = 0; i < AppData.VerifiedStars.Count; i++)
            {
                AppData.Stars[i].Show = AppData.VerifiedStars[i].Show;
            }
            
            PlaySoundScript.Instance.MakeFallSound();
            StartCoroutine(WaitAndLoad(1.9F));
        }
    }
    
    IEnumerator WaitAndLoad(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        Application.LoadLevel(AppData.LEVEL_NAME+AppData.CurrentLevel);
    }
}
