using UnityEngine;

namespace TheFishMaster.Game
{
    [System.Serializable]
    public class FishData
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private float _fishCount;
        [SerializeField] private float _minLength;
        [SerializeField] private float _maxLength;
        [SerializeField] private float _colliderRadius;

        public Sprite Sprite => _sprite;
        public float FishCount => _fishCount;
        public float MinLength => _minLength;
        public float MaxLength => _maxLength;
        public float ColliderRadius => _colliderRadius;
    }
}
