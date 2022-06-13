using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFishMaster.Game
{
    public class Pond : MonoBehaviour
    {
        public List<Fish> Fishes;

        [SerializeField] private Fish _fishPrefab;
        [SerializeField] private Transform _fishesContainer;

        public void PopulateFishes(FishData[] fishDatas)
        {
            Fishes ??= new List<Fish>();

            var count = fishDatas.Length;

            for (var i = 0; i < count; i++)
            {
                var multiplier = fishDatas[i].FishCount;
                for (var j = 0; j < multiplier; j++)
                {
                    var fish = Instantiate(_fishPrefab, _fishesContainer);
                    fish.FishData = fishDatas[i];
                    fish.Setup();
                    Fishes.Add(fish);
                }
            }
        }

        public void Refresh()
        {
            var count = Fishes.Count;

            for (var i = 0; i < count; i++)
            {
                if (!Fishes[i].IsHooked)
                {
                    Fishes[i].Setup();
                }
            }
        }

        public void ReleaseFishFromHook()
        {
            var count = Fishes.Count;

            for (var i = 0; i < count; i++)
            {
                if (!Fishes[i].IsHooked) continue;
                Fishes[i].ReleaseFromHook();
            }
        }

        public int GetHookedFishPrice()
        {
            var count = Fishes.Count;

            var price = 0;

            for (var i = 0; i < count; i++)
            {
                if (Fishes[i].IsHooked)
                    price += Fishes[i].FishData.Price;
            }

            return price;
        }
    }
}
