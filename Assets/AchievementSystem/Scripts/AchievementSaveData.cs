using System;
using System.Collections.Generic;

namespace LetiArts.Systems.Achievements
{
    [Serializable]
    public class AchievementSaveEntry  // the properties I need from the achievement that will be saved
    {
        public string achievementID;
        public int progress;
        public bool unlocked;
    }

    [Serializable]
    public class AchievementSaveData 
    {
        public List<AchievementSaveEntry> savedEntries = new List<AchievementSaveEntry>();
    }
}