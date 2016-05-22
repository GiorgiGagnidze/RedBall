
using UnityEditor;
using UnityEngine;

public static class Tools
{
    [MenuItem("Tools/Clear Prefs")]
    public static void ClearPrefabs()
    {
        PlayerPrefs.DeleteAll();
    }
}
