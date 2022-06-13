using System;
using UnityEngine;

namespace TheFishMaster.Game
{
    public class Hook : MonoBehaviour
    {
        public event EventHandler FishHooked;

        [HideInInspector] public float FishCount;

        [SerializeField] private Transform _biteTransform;
        
        [SerializeField] private Collider2D _collider;

        [SerializeField] private float _length = 1f;
        [SerializeField] private float _capacity = 1f;

        private bool _isMovable;

        public Collider2D Collider => _collider;

        public bool IsMovable => _isMovable;

        public float Length => _length;

        public float Capacity => _capacity;

        private void Awake()
        {
            FishCount = 0;
            _isMovable = true;
        }

        private void OnTriggerEnter2D(Collider2D other) 
        {
            Debug.Log("Hooked");

            var fish = other.GetComponent<Fish>();

            if (fish == null) return;

            fish.EatBait(_biteTransform);

            FishCount++;

            FishHooked?.Invoke(this, EventArgs.Empty);
        }
    }
}
