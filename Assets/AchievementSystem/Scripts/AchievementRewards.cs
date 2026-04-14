using UnityEngine;

namespace LetiArts.Systems.Achievements
{
    public abstract class AchievementRewards : ScriptableObject
    {
        public abstract void Grant();
    }

    [CreateAssetMenu(fileName = "NewGoldReward", menuName = "Achievements/Rewards/Gold")]
    public class GoldReward : AchievementRewards
    {
        public int goldAmount;

        public override void Grant()
        {
            Debug.Log($"Added {goldAmount} Gold!");
        }
    }

    [CreateAssetMenu(fileName = "NewHeartReward", menuName = "Achievements/Rewards/Heart")]
    public class HeartReward : AchievementRewards
    {
        public int heartAmount;

        public override void Grant()
        {
            Debug.Log($"Added {heartAmount} Hearts!");
        } 
    }

    [CreateAssetMenu(fileName = "NewCompassReward", menuName = "Achievements/Rewards/Compass")]
    public class CompassReward : AchievementRewards
    {
        public int compassAmount;
        
        public override void Grant()
        {
            Debug.Log($"Added {compassAmount} Compasses!");
        }
    }
}