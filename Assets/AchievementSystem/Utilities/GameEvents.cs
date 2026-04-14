using System;
using LetiArts.Systems.Achievements;

namespace LetiArts.Systems
{
    public static class GameEvents
    {
        public static Action<AchievementActionType> OnButtonClick;  // Event variable

        public static Action<AchievementEntry> OnAchievementUnlocked; // Event variable to notify ui
    }
}
