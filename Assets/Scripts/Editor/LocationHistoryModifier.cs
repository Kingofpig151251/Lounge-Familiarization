using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class LocationHistoryModifier
    {
        [MenuItem("Tools/Clear Location History")]
        private static void ClearLocationHistory()
        {
            PlayerPrefs.SetInt(Lounge.DeckBusinessLounge.ToString(), 0);
            PlayerPrefs.SetInt(Lounge.WingFristClassLounge.ToString(), 0);
            PlayerPrefs.SetInt(Lounge.WingBusinessLounge.ToString(), 0);
            PlayerPrefs.SetInt(Lounge.PierFirstClassLounge.ToString(), 0);
            PlayerPrefs.SetInt(Lounge.PierBusinessLounge.ToString(), 0);
        }
    }
}