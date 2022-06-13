using System.Collections.Generic;
using UnityEngine;
using TheFishMaster.Game;
using TheFishMaster.Infrastructure;
using System;

namespace TheFishMaster.Service
{
    public class GameplayController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private SpriteRenderer _seaBackground;

        [Header("Controllers")]
        [SerializeField] private HookController _hookController;

        [SerializeField] private FishDatabaseSO _fishDatabase;
        [SerializeField] private Pond _pond;

        private void Awake()
        {
            _hookController.Initialize(_camera);

            _hookController.Hook.FishHooked += FishHooked;

            _seaBackground.transform.SetParent(_camera.transform);

            _pond.PopulateFishes(_fishDatabase.Fishes);
        }

        private void Start()
        {
            StartFishing();
        }

        private void OnDestroy() 
        {
            _hookController.Hook.FishHooked -= FishHooked;
        }

        private void FishHooked(object sender, EventArgs e)
        {
            var fishCount = _hookController.Hook.FishCount;

            if (fishCount >= _hookController.Hook.Capacity)
            {
                StopFishing();
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
            _hookController.AbortHook(() => 
            {
                _pond.ReleaseFishFromHook();
            });
        }
    }
}
