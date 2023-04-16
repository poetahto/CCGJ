using System;
using System.Collections;
using ElRaccoone.Tweens;
using ElRaccoone.Tweens.Core;
using TriInspector;
using UnityEngine;

namespace DefaultNamespace
{
    public class RiseEffect : ToggleEffect
    {
        [SerializeField] private Transform offPoint;
        [SerializeField] private Transform onPoint;
        [SerializeField] private float duration;
        [SerializeField] private EaseType onEase;
        [SerializeField] private EaseType offEase;

        private Vector3 _onPosition;
        private Vector3 _offPosition;

        private void Awake()
        {
            _onPosition = onPoint.transform.position;
            _offPosition = offPoint.transform.position;
        }

        public override IEnumerator ToggleOn()
        {
            Target.TweenCancelAll();
            yield return Target.TweenPosition(_onPosition, duration).SetEase(onEase).Yield();
        }

        public override IEnumerator ToggleOff()
        {
            Target.TweenCancelAll();
            yield return Target.TweenPosition(_offPosition, duration).SetEase(offEase).Yield();
        }

        [Button]
        private void GenerateOnAndOffPoint()
        {
            onPoint = new GameObject("On Point").transform;
            offPoint = new GameObject("Off Point").transform;
            onPoint.SetParent(transform, false);
            offPoint.SetParent(transform, false);
        }
    }
}
