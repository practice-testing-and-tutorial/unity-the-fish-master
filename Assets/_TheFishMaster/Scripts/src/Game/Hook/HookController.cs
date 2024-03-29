using System;
using UnityEngine;
using DG.Tweening;

namespace TheFishMaster.Game
{
    public class HookController : MonoBehaviour
    {
        [SerializeField] private Hook _hook;

        private Camera _camera;

        public float Duration => _hook.Length * .1f;

        public Hook Hook => _hook;

        private void Update()
        {
            if (_hook.IsMovable && Input.GetMouseButton(0))
            {
                MoveToMouseXPosition();
            }
        }

        public void Initialize(Camera camera)
        {
            _hook.FishCount = 0;

            _camera = camera;

            _hook.transform.SetParent(_camera.transform);
        }

        public void HookDown(Action onComplete = null)
        {
            _hook.FishCount = 0;

            var yPos = transform.position.y;
            _camera.transform.DOMoveY(yPos - _hook.Length, 1 + Duration * .25f).From(yPos)
                .OnStart(() => { _hook.Collider.enabled = false; })
                .OnComplete(() => { onComplete?.Invoke(); });
        }

        public void HookUp(Action onComplete = null)
        {
            var yPos = transform.position.y;
            _camera.transform.DOMoveY(yPos, Duration * 5f)
                .OnStart(() => { _hook.Collider.enabled = true; })
                .OnComplete(() => { onComplete?.Invoke(); });
        }

        public void AbortHook(Action onComplete = null)
        {
            var yPos = transform.position.y;
            _camera.transform.DOMoveY(yPos, Duration * 4f)
                .OnStart(() => { _hook.Collider.enabled = false; })
                .OnComplete(() => { onComplete?.Invoke(); });
        }

        private void MoveToMouseXPosition()
        {
            var screenToWorld = _camera.ScreenToWorldPoint(Input.mousePosition);

            var hookPosition = _hook.transform.position;
            hookPosition.x = screenToWorld.x;
            _hook.transform.position = hookPosition;
        }
    }
}
