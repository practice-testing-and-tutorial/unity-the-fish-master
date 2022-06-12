using TheFishMaster.Game;
using UnityEngine;

namespace TheFishMaster.Infrastructure
{
    [CreateAssetMenu(fileName = "New FishDatabase", menuName = "Fish Database")]
    public class FishDatabaseSO : ScriptableObject
    {
        public FishData[] Fishes;
    }
}
