#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

public class MyUtility : Editor
{
    [UnityEditor.MenuItem("Edit/SavePrefab %&s")]
    static void SavePrefab()
    {
        AssetDatabase.SaveAssets();
    }
}

#endif