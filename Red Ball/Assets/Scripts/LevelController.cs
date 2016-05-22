using UnityEngine;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {
	public GameObject[] Levels;
	public GameObject Locked;
	public GameObject Current;
	private int LevelIndex;
	private bool IsOpen;
	
	public void Init(int levelIndex,bool isOpen) 
	{
		LevelIndex = levelIndex;
		IsOpen = isOpen;
		Locked.SetActive(false);
		foreach (GameObject item in Levels)
		{
			item.SetActive(false);
		}
		if (isOpen)
			Current = Levels[levelIndex];
		else
			Current = Locked;
		Current.SetActive(true);
	}

	public void OnClick() 
	{
		if (IsOpen)
		{
			SoundScript.Instance.MakeSelectSound();
			AppData.CurrentLevel = LevelIndex;
			AppData.CurrentScore = 0;
			AppData.CurrentVerifiedScore = 0;
			AppData.CurrentCheckPoint = 0;
			List<Star> stars = AppData.ListStars[LevelIndex];
			AppData.VerifiedStars = new List<Star>();
			AppData.Stars = new List<Star>();
			foreach (Star item in stars)
			{
				AppData.VerifiedStars.Add(new Star(item.X,item.Y,item.Show));
				AppData.Stars.Add(new Star(item.X,item.Y,item.Show));
			}
			
			Application.LoadLevel(AppData.LEVEL_NAME+LevelIndex);
		}
	}
	
	
}
