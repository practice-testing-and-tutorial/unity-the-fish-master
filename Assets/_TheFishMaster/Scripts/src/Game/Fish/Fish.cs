using DG.Tweening;
using UnityEngine;

namespace TheFishMaster.Game
{
    public class Fish : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D _collider;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Tween _tween;

        private FishData _fishData;

        private float _screenLeft;

        public FishData FishData
        {
            get => _fishData;
            set
            {
                _fishData = value;
                _collider.radius = _fishData.ColliderRadius;
                _spriteRenderer.sprite = _fishData.Sprite;
            }
        }

        private void Awake()
        {
            _screenLeft = Camera.main.ScreenToWorldPoint(Vector3.zero).x;
        }

        public void Setup()
        {
            _collider.enabled = true;

            var randomYPosition = Random.Range(_fishData.MinLength, _fishData.MaxLength);

            var position = transform.position;
            position.y = randomYPosition;
            position.x = _screenLeft;
            transform.position = position;

            var randomYTarget = Random.Range(randomYPosition - 1, randomYPosition + 1);
            var destination = new Vector2(-position.x, randomYTarget);

            var duration = 3f;
            var delay = Random.Range(0, duration * 2);

            _tween?.Kill();

            _tween = transform.DOMove(destination, duration).SetLoops(-1, LoopType.Yoyo)
                .SetDelay(delay)
                .OnStepComplete(() =>
                {
                    // Flip the fish
                    var localScale = transform.localScale;
                    localScale.x = -localScale.x;
                    transform.localScale = localScale;
                });
        }

        public void EatBait()
        {
            _collider.enabled = false;
            _tween?.Kill();
        }
    }
}
