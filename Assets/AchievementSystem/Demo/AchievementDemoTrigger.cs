using UnityEngine;
using LetiArts.Systems;
using LetiArts.Systems.Achievements;

public class AchievementDemoTrigger : MonoBehaviour
{
    public void CupidVolleySignal()  
    {
        GameEvents.OnButtonClick?.Invoke(AchievementActionType.Ach_CollectHeart); 
        Debug.Log("Cupid in triple action: Sending signal...");
    }

    public void LevelsCompleteSignal() 
    {
        GameEvents.OnButtonClick?.Invoke(AchievementActionType.Ach_LevelComplete); 
        Debug.Log("5 levels completed: Sending signal...");
    }

    public void FirstTrySignal()
    {
        GameEvents.OnButtonClick?.Invoke(AchievementActionType.Ach_FirstTry);
        Debug.Log("Level Completed with three stars: Sending signal...");
    }

    public void SpeedRunnerSignal()
    {
        GameEvents.OnButtonClick?.Invoke(AchievementActionType.Ach_SpeedRunner);
        Debug.Log("Level Completed under 1 minute: Sending signal...");
    }

    public void TreasureHunterSignal()
    {
        GameEvents.OnButtonClick?.Invoke(AchievementActionType.Ach_TreasureHunter);
        Debug.Log("Collected all treasures: Sending signal...");
    }

    public void PatternMasterSignal()
    {
        GameEvents.OnButtonClick?.Invoke(AchievementActionType.Ach_PatternMaster);
        Debug.Log("Completed the pattern puzzle: Sending signal...");
    }

    public void BookWormSignal()
    {
        GameEvents.OnButtonClick?.Invoke(AchievementActionType.Ach_BookWorm);
        Debug.Log("Read all the lore entries: Sending signal...");
    }    
}