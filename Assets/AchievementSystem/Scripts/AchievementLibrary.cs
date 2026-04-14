using UnityEngine;
using System.Collections.Generic;
using System;

namespace LetiArts.Systems.Achievements
{
    [System.Serializable]
    public class AchievementEntry
    {
        public AchievementActionType AchTargetAction;
        public string id;
        public string title;
        [TextArea] public string description;
        public Sprite icon;
        public int goalValue;
        public int currentProgress;
        public bool isUnlocked;

        [SerializeReference] public AchievementRewards reward;
        
         // unlock achievement and invoke reward and event
        public void UnlockAchievement() 
        {
            isUnlocked = true;
            reward?.Grant();
            GameEvents.OnAchievementUnlocked?.Invoke(this);
        }

        // update achievement progres and check for unlock condition
        public void UpdateAchievementProgress(int amount)  
        {
            if (isUnlocked) return;
            currentProgress += amount;
            if (currentProgress >= goalValue)
            {
                UnlockAchievement();
            }
            
        }
    }

    [CreateAssetMenu(fileName = "AchievementLibrary", menuName = "Achievements/Achievement Library")]

    public class AchievementLibrary : ScriptableObject
    {
        public List<AchievementEntry> allAchievements =  new List<AchievementEntry>();
    }
}