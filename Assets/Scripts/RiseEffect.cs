using System;
using System.Collections;
using ElRaccoone.Tweens;
using ElRaccoone.Tweens.Core;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class RiseEffect : IToggleEffect
    {
        [SerializeField] private Transform offPoint;
        [SerializeField] private Transform onPoint;
        [SerializeField] private float duration;
        [SerializeField] private EaseType onEase;
        [SerializeField] private EaseType offEase;

        public GameObject Target { get; set; }

        public IEnumerator ToggleOn()
        {
            // yield return Target.TweenPosition(onPoint.position, duration).SetEase(onEase).Yield();
            yield break;
        }

        public IEnumerator ToggleOff()
        {
            // yield return Target.TweenPosition(offPoint.position, duration).SetEase(offEase).Yield();
            yield break;
        }
    }
}
