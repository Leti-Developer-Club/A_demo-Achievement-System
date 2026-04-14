using UnityEngine;
using System.Collections.Generic;
using LetiArts.Systems;
using System.IO;

namespace LetiArts.Systems.Achievements 
{
    public class AchievementManager : MonoBehaviour
    {
        public static AchievementManager Instance {get; private set;}  // singleton instance

        [SerializeField] private AchievementLibrary ach_Library; // link to the achievements list
        [SerializeField] private AchievementSaveSystem saveSystem; // ref to the achievement save system

        void Start()
        {
            saveSystem.LoadAchievements();
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
            if (ach_Library == null || saveSystem == null) return; // safety check to avoid null reference errors
            foreach (var ach in ach_Library.allAchievements) // iterating through the database of achievements
            {
                if (ach.AchTargetAction == achievementAction && !ach.isUnlocked) // checking if the achievement enum matches the action enum
                {
                    ach.UpdateAchievementProgress(1); // adding progress to the achievement
                    saveSystem.SaveAchievements();
                }
            }
        } 
    }
}