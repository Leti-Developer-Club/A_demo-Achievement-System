using UnityEngine;
using LetiArts.Systems;
using LetiArts.Systems.Achievements;

public class AchievementTestTrigger : MonoBehaviour
{
   
    public void SendHeartAchievementSignal()  // button click achievement signal
    {
        Debug.Log("Button Clicked: Sending signal...");
        GameEvents.TriggerAchievementAction(AchievementActionType.Ach_CollectHeart);  // Using the created Event, calling its activator function to send a signal 
    }

    public void LevelCompleteSignal() // Level complete achievement signal
    {
        Debug.Log("Button Clicked: Sending signal...");
        GameEvents.TriggerAchievementAction(AchievementActionType.Ach_LevelComplete);  // Using the created Event, calling its activator function to send a signal 
    }
}