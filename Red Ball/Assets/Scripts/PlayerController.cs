using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private bool showHint = false;
    public float maxSpeed;
    public float jumpForce;
    protected bool grounded;
    public InputController inputController;
    protected Rigidbody2D body;
    protected bool inZone;
    private String zoneTag = "";
    private bool inAction = false;

    public void InZone(Vector2 vector2,String tag)
    {
        inZone = true;
        zoneTag = tag;
        body.drag = 2f;
        body.angularDrag = 2f;
        body.AddForce(vector2, ForceMode2D.Force);
    }

    public void LeaveZone()
    {
        inZone = false;
        
        body.drag = 0.5f;
        body.angularDrag = 0.5f;
    }

    public virtual void Move(float dir)
    {
        Vector2 direction = new Vector2(dir * maxSpeed, 0);
        if (direction.x + body.velocity.x > 10){
            direction = new Vector2(10-body.velocity.x, 0);
        }
        body.AddForce(direction);
    }

    protected virtual void Start()
    {
        body = GetComponent<Rigidbody2D>();
        inputController.OnMovementX += InputController_OnMovementX;
        inputController.OnMovementY += InputController_OnMovementY;
    }

    private void InputController_OnMovementY(float dir)
    {
        if (!inAction)
        {
            if (dir > 0) Jump();
            if (dir < 0)
            {
                if (inZone)
                    Dive();
                else if (grounded)
                    Stop();
            }   
        }
    }

    private void Dive()
    {
        //body.velocity = body.velocity * 0.9f;
        body.AddForce(new Vector2(body.velocity.x, -(body.velocity.y+13)));
    }

    private void Stop()
    {
        body.velocity = new Vector2(body.velocity.x/2, body.velocity.y);
    }

    protected virtual void Jump()
    {
        if (inZone && zoneTag != "ZoneGround" && zoneTag != "Surface")
        {
            body.velocity = new Vector2(body.velocity.x, jumpForce/2);
        } else if (grounded) {
            JumpSoundScript.Instance.MakeJumpSound();
            body.velocity = new Vector2(body.velocity.x, jumpForce);
        }
    }

    private void InputController_OnMovementX(float dir)
    {
        if (!inAction)
            Move(dir);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" ) grounded = true;
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") grounded = false;
    }
    
    void OnTriggerEnter2D(Collider2D otherCollider)
	{
		if (otherCollider.gameObject.tag == "Star" ) {
            PlaySoundScript.Instance.MakeStarSound();
            int index = Int32.Parse( otherCollider.gameObject.name.Substring(4));
            AppData.CurrentScore += 100;
			AppData.Stars[index].Show = false;
            if (AppData.CurrentLevel != 3)
            {
                if (AppData.Stars[index].X < AppData.ListFlags[AppData.CurrentLevel][AppData.CurrentCheckPoint].x)
                {
                    AppData.CurrentVerifiedScore += 100;
                    AppData.VerifiedStars[index].Show = false;
                }  
            } else 
            {
                if (AppData.Stars[index].Y < AppData.ListFlags[AppData.CurrentLevel][AppData.CurrentCheckPoint].y)
                {
                    AppData.CurrentVerifiedScore += 100;
                    AppData.VerifiedStars[index].Show = false;
                } 
            }
			
			Destroy(otherCollider.gameObject);
		} else if (otherCollider.gameObject.tag == "Finish" ) {
            int count = 0;
			foreach (Star item in AppData.Stars)
			{
				if (!item.Show)
					count++;
			}
            if (AppData.Stars.Count > count && !inAction)
                showHint = true;
            
			if (AppData.CurrentLevel != AppData.NUMBER_OF_LEVELS - 1 && AppData.Stars.Count == count && !inAction)
			{
                inAction = true;
                body.isKinematic = true;
				AppData.CurrentLevel++;
				if (AppData.CurrentLevel > AppData.LastLevel)
					AppData.LastLevel = AppData.CurrentLevel;
				AppData.CurrentVerifiedScore = AppData.CurrentScore;
				AppData.CurrentCheckPoint = 0;
				List<Star> stars = AppData.ListStars[AppData.CurrentLevel];
				AppData.VerifiedStars = new List<Star>();
				AppData.Stars = new List<Star>();
				foreach (Star item in stars)
				{
					AppData.VerifiedStars.Add(new Star(item.X,item.Y,item.Show));
					AppData.Stars.Add(new Star(item.X,item.Y,item.Show));
				}
				
                PlaySoundScript.Instance.MakeFinishSound();
                StartCoroutine(WaitAndLoad(2.5F,AppData.LEVEL_NAME+AppData.CurrentLevel));
			}
            if (AppData.CurrentLevel == AppData.NUMBER_OF_LEVELS - 1 && AppData.Stars.Count == count && !inAction)
			{
                inAction = true;
                body.isKinematic = true;
                PlaySoundScript.Instance.MakeFinishSound();
                StartCoroutine(WaitAndLoad(2.5F,"Menu")); 
            }
        } else if (otherCollider.gameObject.tag == "CheckPoint" ) 
        {
            int index = Int32.Parse( otherCollider.gameObject.name.Substring(5));
            if (AppData.CurrentCheckPoint < index) 
            {
                PlaySoundScript.Instance.MakeCheckPointSound();
                int count = 0;
                foreach (Star item in AppData.Stars)
                {
                    if (AppData.CurrentLevel != 3)
                    {
                        if (!item.Show && item.X < AppData.ListFlags[AppData.CurrentLevel][index].x) 
                        {
                            AppData.VerifiedStars[count].Show = false;
                            AppData.CurrentVerifiedScore += 100; 
                        }
                    } else 
                    {
                        if (!item.Show && item.Y < AppData.ListFlags[AppData.CurrentLevel][index].y) 
                        {
                            AppData.VerifiedStars[count].Show = false;
                            AppData.CurrentVerifiedScore += 100; 
                        }
                    }
                    
                    count++;   
                }
                AppData.CurrentCheckPoint = index;
            }
        }
	}
    
    IEnumerator WaitAndLoad(float waitTime, String destination) {
        yield return new WaitForSeconds(waitTime);
        if (destination == "Menu")
        {
            Destroy(GameObject.Find("Music"));
			Destroy(GameObject.Find("Sound"));
			Destroy(GameObject.Find("JumpSound"));
			Destroy(GameObject.Find("PlaySound"));
        }
        Application.LoadLevel(destination);
    }
    
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            showHint = false;
        }
    }
    
    void OnGUI()
	{
        if (showHint)
        {
            int count = 0;
			foreach (Star item in AppData.Stars)
			{
				if (!item.Show)
					count++;
			}
            GUIStyle myStyle = new GUIStyle();
            myStyle.fontSize = 15;
            myStyle.normal.textColor = Color.yellow;
            
            const int height = 20;
            const int width = 40;
            Rect rect = new Rect(
                Screen.width/2,
                Screen.height/2,
                width,
                height
                );
            GUI.Label(rect,"You need to collect "+(AppData.Stars.Count-count)+" more stars.",myStyle);
        }
    }
}
