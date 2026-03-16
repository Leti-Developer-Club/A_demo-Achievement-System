using System;

namespace LetiArts.Systems
{
    public static class GameEvents
    {
        public static Action<AchievementActionType> OnButtonClick;  // Event variable

        public static void TriggerAchievementAction(AchievementActionType action) // Container function for Event Activator
        {
            OnButtonClick?.Invoke(action);   // Event Activator
        }
    }
}
