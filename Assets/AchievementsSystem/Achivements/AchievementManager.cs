using UnityEngine;
using System.Collections.Generic;
using LetiArts.Systems;
using System.IO;

namespace LetiArts.Systems.Achievements 
{
    public class AchievementManager : MonoBehaviour
    {
        public static AchievementManager Instance {get; private set;}  // singleton instance

        [SerializeField] private AchievementLibrary library; // link to the achievements list
        [SerializeField] private AchievementNotification achNotificationUI; // ref to achievement ui panel

        void Start()
        {
            LoadAchievements();
        }

        private void OnEnable() // Unity function called when the script is enabled
        {
            GameEvents.OnButtonClick += OnAchievementActionTriggered; // entering the game event class to access the event variable and subscribe our action function to it
        }

        private void OnDisable() // Unity function called when the script is disabled
        {
            GameEvents.OnButtonClick -= OnAchievementActionTriggered; // unsubscribing the action function from the Event variable
        }

        private void OnAchievementActionTriggered(AchievementActionType achievementAction) // Action function that will be called when the Event is triggered
        {
            // bool didChange = false;

            foreach (AchievementEntry ach in library.allAchievements) // iterating through the database of achievements
            {
                if(ach.AchTargetAction == achievementAction && !ach.isUnlocked) // checking if the achievement enum matches the action enum
                {
                    ach.ProcessAchievementProgress(1); // adding progress to the achievement
                    SaveAchievements();

                    if(ach.isUnlocked)
                    {
                        achNotificationUI.ShowAchievement(ach); // use ui script function to display ach ui panel
                    }
                }
            }
        }

        public void SaveAchievements()
        {
            AchievementSaveData achDataToSave = new AchievementSaveData();

            foreach (AchievementEntry ach in library.allAchievements)
            {
                achDataToSave.savedEntries.Add(new AchievementSaveEntry{
                    achievementID = ach.id,
                    progress = ach.currentProgress,
                    unlocked = ach.isUnlocked
                });
            }

            string json = JsonUtility.ToJson(achDataToSave);
            PlayerPrefs.SetString("AchievementSave", json);
            PlayerPrefs.Save();
            Debug.Log("Achievements Saved");
        }

        public void LoadAchievements()
        {
            if(!PlayerPrefs.HasKey("AchievementSave")) return;

            string json = PlayerPrefs.GetString("AchievementSave");
            AchievementSaveData loadedData = JsonUtility.FromJson<AchievementSaveData>(json);

            foreach (var savedEntry in loadedData.savedEntries)
            {
                var libraryEntry = library.allAchievements.Find(a => a.id == savedEntry.achievementID);
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