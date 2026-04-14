using UnityEngine;
using UnityEditor;
using LetiArts.Systems.Achievements;
using LetiArts.Systems;

public class AchievementTrackerWindow : EditorWindow
{
    private AchievementLibrary ach_Library;
    private string searchString = "";
    private int currentTab = 0;
    private string[] tabNames = {"All", "Unlocked", "Locked"};

    [MenuItem("Achievements/ Achievements Tracker")]
    
    public static void ShowWindow() // Editor window shower
    {
        GetWindow<AchievementTrackerWindow>("Achievements Tracker");
    }

    // Area of concern
    private void OnEnable()
    {
        if (ach_Library == null)
        {
            string[] guids = AssetDatabase.FindAssets("t:AchievementLibrary");

            if(guids.Length > 0)
            {
                string path =  AssetDatabase.GUIDToAssetPath(guids[0]);
                ach_Library = AssetDatabase.LoadAssetAtPath<AchievementLibrary>(path);
            }
        }
        
    }

    private void OnInspectorUpdate()  // on every frame refresh the editor window to update
    {
        Repaint();
    }

    private void OnGUI()  
    {   
        if (ach_Library == null)  // null check for achievement library
        {
            EditorGUILayout.HelpBox("Achievement Library not found. Create one to use this tool.", MessageType.Warning);
            return;
        }

        DrawHeader();  
        DrawAchievementList();
    }

    private void DrawHeader()  // header area with search bar and filter tabs
    {
        GUILayout.Space(10);
        searchString = EditorGUILayout.TextField("Search: ", searchString);
        GUILayout.Space(10);
        currentTab = GUILayout.Toolbar(currentTab, tabNames);
        GUILayout.Space(10);
    }

    private void DrawAchievementList()
    {
        if (ach_Library == null)  // null check for achievement library
        {
            EditorGUILayout.HelpBox("Achievement Library not found. Create one to use this tool.", MessageType.Warning);
            return;
        }

        // spawn achievement list
        foreach (var ach in ach_Library.allAchievements)
        {   
            // If player doesn't search for anything, no condition gets fulfilled here, therefore, skip this condition and draw all achievements.
            if (!string.IsNullOrEmpty(searchString) && !ach.title.ToLower().Contains(searchString.ToLower()))
            {continue;} // if search string dey and achievement title no contain am, skip this one

            if (currentTab == 1 && !ach.isUnlocked) 
            {continue;} // if the Unlocked tab is selected but the achievement is not unlocked, skip this entry
               
            if (currentTab == 2 && ach.isUnlocked)
            {continue;} // if the Locked tab is selected but the achievement is unlocked, skip this entry

            DrawAchievementEntry(ach); // now draw the entries
        }
    }

    private void DrawAchievementEntry(AchievementEntry ach)
    {
        EditorGUILayout.BeginHorizontal("box", GUILayout.Height(30));
        EditorGUILayout.LabelField(ach.title, GUILayout.Width(140));

        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField($"Progress: {ach.currentProgress}/{ach.goalValue}", GUILayout.Width(120));

        Undo.RecordObject(ach_Library, "Force Unlock Achievement"); // record the state of the library for undo functionality

        GUILayout.FlexibleSpace();
        if(!ach.isUnlocked)
        {
            // Force Unlock Button
            if (GUILayout.Button("Unlock", GUILayout.Width(70)))
            {
                // Unlock Achievement
                ach.isUnlocked = true;
                ach.currentProgress = ach.goalValue;
                if (Application.isPlaying)
                {
                   ach.UnlockAchievement();
                }
            }
        }
        
        GUILayout.FlexibleSpace();
        if (ach.currentProgress > 0 || ach.isUnlocked)
        {
            // Reset Progress Button
            if (GUILayout.Button("Reset", GUILayout.Width(70)))
            {
                // Reset Achievement
                ach.currentProgress = 0;
                ach.isUnlocked = false; 
            }
        }

        EditorUtility.SetDirty(ach_Library); // ensure changes are saved to the asset
        EditorGUILayout.EndHorizontal();
    }
}