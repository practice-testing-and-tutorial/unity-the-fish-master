using System.Collections.Generic;
using UnityEngine;
using TheFishMaster.Game;
using TheFishMaster.Infrastructure;

namespace TheFishMaster.Service
{
    public class GameplayController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private SpriteRenderer _seaBackground;

        [Header("Controllers")]
        [SerializeField] private HookController _hookController;

        [SerializeField] private FishDatabaseSO _fishDatabase;
        [SerializeField] private Fish _fishPrefab;
        [SerializeField] private Transform _fishesContainer;

        private List<Fish> _fishes;

        private void Awake()
        {
            _hookController.Initialize(_camera);

            _seaBackground.transform.SetParent(_camera.transform);

            PopulateFishes();
        }

        private void Start()
        {
            StartFishing();
        }

        public void PopulateFishes()
        {
            var count = _fishDatabase.Fishes.Length;

            _fishes ??= new List<Fish>();

            for (var i = 0; i < count; i++)
            {
                var multiplier = _fishDatabase.Fishes[i].FishCount;
                for (var j = 0; j < multiplier; j++)
                {
                    var fish = Instantiate(_fishPrefab, _fishesContainer);
                    fish.FishData = _fishDatabase.Fishes[i];
                    fish.Setup();
                    _fishes.Add(fish);
                }
            }
        }

        public void StartFishing()
        {
            // todo: Jump (smoothly) the camera to the bottom of display
            // todo: Show the hook (display / presentation)
            _hookController.HookDown(() =>
            {
                _hookController.HookUp();
            });
        }

        public void StopFishing()
        {
            // todo: Hide the hook
            // todo: Jump (smoothly) the camera to the main display
            // todo: Clear fish from the hook or some other mechanism
        }
    }
}
