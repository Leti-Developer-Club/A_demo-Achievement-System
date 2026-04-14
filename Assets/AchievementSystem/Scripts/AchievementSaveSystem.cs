using UnityEngine;
// using System.Collections.Generic;
using System.IO;
// using LetiArts.Systems;

namespace LetiArts.Systems.Achievements
{
    public class AchievementSaveSystem : MonoBehaviour
    {
        [SerializeField] private AchievementLibrary ach_Library; 
        
        public void SaveAchievements()
        {
            AchievementSaveData achDataToSave = new AchievementSaveData();  // ref to the class containing the list to store the ach data in

            if (ach_Library == null){
                Debug.LogWarning("Achievement Library reference is missing. Cannot save achievements.");
                return;
            }

            foreach (AchievementEntry ach in ach_Library.allAchievements)  // save achievement entry data into variables to store in json
            {
                achDataToSave.savedEntries.Add(new AchievementSaveEntry
                {
                    achievementID = ach.id,
                    progress = ach.currentProgress,
                    unlocked = ach.isUnlocked
                });
            }
            
            string json = JsonUtility.ToJson(achDataToSave);

            // string path = Application.persistentDataPath + "/AchievementSave.json";
            // File.WriteAllText(path, json);
            // Debug.Log("Achievements Saved to " + path);

            PlayerPrefs.SetString("AchievementSave", json);
            PlayerPrefs.Save();
            Debug.Log("Achievements Saved");
        }

        public void LoadAchievements()
        {
            if(!PlayerPrefs.HasKey("AchievementSave")) return;  // null check to see if achievement data dey there

            string json = PlayerPrefs.GetString("AchievementSave");
            AchievementSaveData loadedData = JsonUtility.FromJson<AchievementSaveData>(json);  // 

            foreach (var savedEntry in loadedData.savedEntries)
            {
                var libraryEntry = ach_Library.allAchievements.Find(a => a.id == savedEntry.achievementID);
                if(libraryEntry != null)
                {
                    libraryEntry.currentProgress = savedEntry.progress;
                    libraryEntry.isUnlocked = savedEntry.unlocked;
                }
            }

            Debug.Log("Achievements Loaded");
        }

    }
}