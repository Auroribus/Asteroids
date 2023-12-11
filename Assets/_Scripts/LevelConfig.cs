using _Scripts.Components.Pickups;
using UnityEngine;

namespace _Scripts
{
    [CreateAssetMenu(fileName = nameof(LevelConfig), menuName = "Configs/" + nameof(LevelConfig))]
    public class LevelConfig : ScriptableObject
    {
        public int UfoScore;
        public int UfoLevelThreshold;
        public int PickupLevelThreshold;
        
        public int AsteroidScore;
        public int AsteroidSplitAmount;
        public int AsteroidIncrementAmount;

        public PickupComponent[] Pickups;
    }
}