using System.Collections;
using ElRaccoone.Tweens;
using ElRaccoone.Tweens.Core;
using UnityEngine;

namespace DefaultNamespace
{
    public class ScaleEffect : ToggleEffect
    {
        [SerializeField] private float duration;
        [SerializeField] private EaseType onEase = EaseType.CubicInOut;
        [SerializeField] private EaseType offEase = EaseType.CubicInOut;

        public override IEnumerator ToggleOn()
        {
            Target.TweenCancelAll();
            Target.TweenLocalScaleX(1, duration).SetEase(onEase);
            Target.TweenLocalScaleZ(1, duration).SetEase(onEase);
            yield return new WaitForSeconds(duration);
        }

        public override IEnumerator ToggleOff()
        {
            Target.TweenCancelAll();
            Target.TweenLocalScaleX(0, duration).SetEase(offEase);
            Target.TweenLocalScaleZ(0, duration).SetEase(offEase);
            yield return new WaitForSeconds(duration);
        }
    }
}
