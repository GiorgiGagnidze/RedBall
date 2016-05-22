using UnityEngine;

public class GameScript : MonoBehaviour {
	public Texture StarTexture;
	public Texture CrownTexture;
	public GameObject Camera;
	public Transform Ball;
	public Transform Star;
	private bool isPaused = false;
	
	private string lastTooltip = "";
	private string beforeLastTooltip = "";

	// Use this for initialization
	void Start () {
		FollowController follow = Camera.GetComponent<FollowController>();
		Transform transform = (Transform)Instantiate(Ball, AppData.ListFlags[AppData.CurrentLevel][AppData.CurrentCheckPoint],
			 Quaternion.identity);
		follow.target = transform;
		for (int i=0; i<AppData.Stars.Count; i++)
		{
			Star item = AppData.Stars[i];
			if (item.Show)
			{
				Transform form = (Transform)Instantiate(Star, new Vector2(item.X,item.Y), Quaternion.identity);
				form.gameObject.name = "Star"+i;
			}
		}
	}
	
	void OnGUI()
	{
		const int buttonWidth = 100;
		const int buttonHeight = 35;
		const int menuButtonWidth = 220;
	
		// Determine the button's place on screen
		// Center in X, 2/3 of the height in Y
		GUIStyle myStyle = new GUIStyle();
		myStyle.fontSize = 30;
		myStyle.normal.textColor = Color.yellow;
     	myStyle.hover.textColor = Color.magenta;
		
		Rect buttonRect = new Rect(
			Screen.width - buttonWidth,
			(24 * Screen.height / 25) - (buttonHeight / 2),
			buttonWidth,
			buttonHeight
			);
		Rect levelRect = new Rect(
			0,
			(24 * Screen.height / 25) - (buttonHeight / 2),
			buttonWidth,
			buttonHeight
			);
		Rect starRect = new Rect(
			buttonWidth,
			(24 * Screen.height / 25) - (buttonHeight / 2),
			buttonWidth/2,
			buttonHeight
			);
		Rect ratio = new Rect(
			(float)(buttonWidth*1.6),
			(24 * Screen.height / 25) - (buttonHeight / 2),
			buttonWidth/2,
			buttonHeight
			);
		Rect crown = new Rect(
			(float)(buttonWidth*2.2),
			(24 * Screen.height / 25) - (buttonHeight / 2),
			buttonWidth/2,
			buttonHeight
			);
		Rect scoreRect = new Rect(
			(float)(buttonWidth*2.8),
			(24 * Screen.height / 25) - (buttonHeight / 2),
			buttonWidth/2,
			buttonHeight
			);
		Rect restartRect = new Rect(
			Screen.width - (float)(buttonWidth*2.5),
			(24 * Screen.height / 25) - (buttonHeight / 2),
			buttonWidth+10,
			buttonHeight
			);
		Rect resumeRect = new Rect(
			Screen.width/2 - menuButtonWidth/2,
			Screen.height/2 - 2*buttonHeight,
			menuButtonWidth,
			buttonHeight
			);
		Rect soundRect = new Rect(
			Screen.width/2 - menuButtonWidth/2,
			Screen.height/2 - buttonHeight,
			menuButtonWidth,
			buttonHeight
			);
		Rect musicRect = new Rect(
			Screen.width/2 - menuButtonWidth/2,
			Screen.height/2,
			menuButtonWidth,
			buttonHeight
			);
		Rect menuRect = new Rect(
			Screen.width/2 - menuButtonWidth/2,
			Screen.height/2 + buttonHeight,
			menuButtonWidth,
			buttonHeight
			);
		
			
		GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
		myButtonStyle.fontSize = 30;
			
		// Set color for selected and unselected buttons
		myButtonStyle.normal.textColor = Color.yellow;
		myButtonStyle.hover.textColor = Color.blue;
		
		
		
		if (isPaused)
		{
			if(GUI.Button(buttonRect,new GUIContent("Menu", "kbutton"),myButtonStyle))
			{
				Time.timeScale = 1;
				SoundScript.Instance.MakePauseSound();
				isPaused = false;
			}
			if(GUI.Button(resumeRect,new GUIContent("Resume Game", "fbutton"),myButtonStyle))
			{
				Time.timeScale = 1;
				SoundScript.Instance.MakePauseSound();
				isPaused = false;
			}
			if(GUI.Button(menuRect,new GUIContent("Main Menu", "ebutton"),myButtonStyle))
			{
				Time.timeScale = 1;
				SoundScript.Instance.MakeSelectSound();
				isPaused = false;
				Destroy(GameObject.Find("Music"));
				Destroy(GameObject.Find("Sound"));
				Destroy(GameObject.Find("JumpSound"));
				Destroy(GameObject.Find("PlaySound"));
				Application.LoadLevel("Menu");
			}
			if (AppData.isMusicOn)
			{
				if(GUI.Button(musicRect,new GUIContent("Music: Off", "dbutton"),myButtonStyle))
				{
					AppData.isMusicOn = false;
					MusicScript.Instance.stopMusic();
				}
					
			} else {
				if(GUI.Button(musicRect,new GUIContent("Music: On", "cbutton"),myButtonStyle)) 
				{
					SoundScript.Instance.MakeSelectSound();
					AppData.isMusicOn = true;
					MusicScript.Instance.startMusic();
				}
					
			}
			if (AppData.isSoundOn)
			{
				if(GUI.Button(soundRect,new GUIContent("Sound: Off", "bbutton"),myButtonStyle))
					AppData.isSoundOn = false;
			} else {
				if(GUI.Button(soundRect,new GUIContent("Sound: On", "abutton"),myButtonStyle)) {
					AppData.isSoundOn = true;
					SoundScript.Instance.MakeSelectSound();
				}
			}
		} else 
		{
			if(GUI.Button(buttonRect,new GUIContent("Menu", "kbutton"),myButtonStyle))
			{
				SoundScript.Instance.MakePauseSound();
				isPaused = true;
				Time.timeScale = 0;
			}
		}
			
		
		
		if(GUI.Button(restartRect,new GUIContent("Restart", "mbutton"),myButtonStyle))
		{
			SoundScript.Instance.MakeSelectSound();
			isPaused = false;
			Time.timeScale = 1;
			AppData.CurrentScore = AppData.CurrentVerifiedScore;
            for (int i = 0; i < AppData.VerifiedStars.Count; i++)
            {
                AppData.Stars[i].Show = AppData.VerifiedStars[i].Show;
            }
            Application.LoadLevel(AppData.LEVEL_NAME+AppData.CurrentLevel);
		}
		
		string tool = GUI.tooltip;
		if(tool.Contains("button") && tool != lastTooltip && tool != beforeLastTooltip){
			SoundScript.Instance.MakeHoverSound();
		}
		beforeLastTooltip = lastTooltip;
		lastTooltip =tool;     
		
		int count = 0;
		foreach (Star item in AppData.Stars)
		{
			if (!item.Show)
				count++;
		}
		
		string score = ""+AppData.CurrentScore;
		string zeros = "";
		for (int i = 0; i < 5-score.Length; i++)
		{
			zeros += "0";
		}
		GUI.DrawTexture(starRect,StarTexture);
		GUI.DrawTexture(crown,CrownTexture);
		GUI.Label(levelRect,"Level "+(AppData.CurrentLevel+1),myStyle);
		GUI.Label(ratio,count+"/"+AppData.Stars.Count,myStyle);
		GUI.Label(scoreRect,zeros+score,myStyle);
	}
}
