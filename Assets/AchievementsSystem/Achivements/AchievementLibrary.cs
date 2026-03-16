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

        public void Unlock()
        {
            reward?.Grant();
        }


        public void ProcessAchievementProgress(int amount)
        {
            if (isUnlocked) return;
            currentProgress += amount;
            if (currentProgress >= goalValue) isUnlocked = true;
        }
    }

    [CreateAssetMenu(fileName = "AchievementLibrary", menuName = "LetiArts/Achievement Library")]

    public class AchievementLibrary : ScriptableObject
    {
        public List<AchievementEntry> allAchievements =  new List<AchievementEntry>();
    }
}