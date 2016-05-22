using UnityEngine;
using System.Collections.Generic;

public static class AppData {
	public static int CurrentLevel {get; set;}
	public static int CurrentScore {get; set;}
	public static int CurrentVerifiedScore {get; set;}
	public static int CurrentCheckPoint {get; set;}
	public static bool isSoundOn = true;
	public static bool isMusicOn = true;
	
	public static List<Star> Stars {get; set;}
	public static List<Star> VerifiedStars {get; set;}
	
	public static List<List<Star>> ListStars = new List<List<Star>>(){
		new List<Star>(){
			new Star((float)8.19,1,true),
			new Star((float)9.19,1,true),
			new Star((float)10.19,1,true)
		},
		new List<Star>(){
			new Star((float)11.77,(float)4.33,true),
			new Star((float)12.77,(float)4.33,true),
			new Star((float)13.77,(float)4.33,true),
			new Star((float)14.77,(float)4.33,true),
			new Star((float)15.77,(float)4.33,true),
			new Star((float)31.28,(float)2.16,true)
		},
		new List<Star>(){
			new Star((float)14,(float)4.51,true),
			new Star((float)15,(float)4.51,true),
			new Star((float)16,(float)4.51,true),
			new Star((float)28.19,(float)4.51,true),
			new Star((float)34,(float)4.51,true),
			new Star((float)55.18,(float)2.46,true),
			new Star((float)56.18,(float)2.46,true)
		},
		new List<Star>(){
			new Star((float)2.17,(float)4.33,true),
			new Star((float)-6.39,(float)6.98,true),
			new Star((float)-2.12,(float)10.22,true),
			new Star((float)3.75,(float)12.28,true),
			new Star((float)-1.06,(float)17.52,true),
			new Star((float)-5.25,(float)21.46,true),
			new Star((float)2.88,(float)24.67,true),
			new Star((float)-2.72,(float)27.6,true),
			new Star((float)4.04,(float)30.88,true)
		}
	};
	
	public static List<List<Vector2>> ListFlags = new List<List<Vector2>>(){
		new List<Vector2>(){
			new Vector2(1,(float)0.2)
		},
		new List<Vector2>(){
			new Vector2(-1,(float)0.53),
			new Vector2(21,(float)1.82)
		},
		new List<Vector2>(){
			new Vector2((float)4.7,(float)0.75),
			new Vector2(19,(float)4.4),
			new Vector2((float)39.8,(float)4.4)
		},
		new List<Vector2>(){
			new Vector2((float)2,(float)0),
			new Vector2((float)-7.25,(float)14.5)
		}
	};
	
	private static int lastLevel = 0;
	public static int LastLevel
	{
		get 
		{
			if (lastLevel == 0 && PlayerPrefs.HasKey(PREFS_LEVEL_KEY))
			{
					lastLevel = PlayerPrefs.GetInt(PREFS_LEVEL_KEY);
			}
			return lastLevel;
		}
		set 
		{
			lastLevel = value;
			PlayerPrefs.SetInt(AppData.PREFS_LEVEL_KEY, value);
		}
	}

	public const string PREFS_LEVEL_KEY = "lvl";
	public const int NUMBER_OF_LEVELS = 4;
	public const int STAR_SCORE = 100;
	public const string LEVEL_NAME = "level";
}
