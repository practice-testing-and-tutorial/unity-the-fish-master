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

        private bool _isHooked;

        public bool IsHooked => _isHooked;

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

            _tween = transform.DOMove(destination, duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear)
                .SetDelay(delay)
                .OnStepComplete(FlipSprite);
        }

        public void EatBait(Transform baitTransform)
        {
            _isHooked = true;
            _collider.enabled = false;
            _tween?.Kill();

            transform.SetParent(baitTransform);
            transform.localScale = Vector3.one;
            transform.position = baitTransform.position;
            transform.rotation = Quaternion.identity;

            transform.DOShakeRotation(5f, 45f, 15).SetLoops(1, LoopType.Yoyo)
                .OnComplete(() => 
                {
                    transform.rotation = Quaternion.identity;
                });
        }

        public void ReleaseFromHook()
        {
            _isHooked = false;
            Setup();
        }

        private void FlipSprite()
        {
            var localScale = transform.localScale;
            localScale.x = -localScale.x;
            transform.localScale = localScale;
        }
    }
}
