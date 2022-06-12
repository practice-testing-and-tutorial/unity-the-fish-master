using System.Collections.Generic;
using UnityEngine;

namespace TheFishMaster.Game
{
    public class Hook : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider;

        [SerializeField] private float _length = 1f;
        [SerializeField] private float _strength = 1f;

        private List<Fish> _fishes;

        private bool _isMovable;

        public Collider2D Collider => _collider;

        public List<Fish> Fishes => _fishes ??= new List<Fish>();

        public float Length => _length;

        public float Strength => _strength;

        public bool IsMovable => _isMovable;

        private void Awake()
        {
            _isMovable = true;
        }
    }
}
