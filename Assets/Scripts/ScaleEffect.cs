using System.Collections;
using ElRaccoone.Tweens;
using ElRaccoone.Tweens.Core;
using UnityEngine;

namespace DefaultNamespace
{
    public class ScaleEffect : ToggleEffect
    {
        [SerializeField] private float duration = 1;
        [SerializeField] private EaseType onEase = EaseType.CubicInOut;
        [SerializeField] private EaseType offEase = EaseType.CubicInOut;
        [SerializeField] private bool scaleX = true;
        [SerializeField] private bool scaleY;
        [SerializeField] private bool scaleZ = true;

        public override IEnumerator ToggleOn()
        {
            foreach (var col in Target.GetComponentsInChildren<Collider>())
                col.enabled = true;
            Target.TweenCancelAll();
            if (scaleX)
                Target.TweenLocalScaleX(1, duration).SetEase(onEase);
            if (scaleY)
                Target.TweenLocalScaleY(1, duration).SetEase(onEase);
            if (scaleZ)
                Target.TweenLocalScaleZ(1, duration).SetEase(onEase);
            yield return new WaitForSeconds(duration);
        }

        public override IEnumerator ToggleOff()
        {
            Target.TweenCancelAll();
            if (scaleX)
                Target.TweenLocalScaleX(0, duration).SetEase(offEase);
            if (scaleY)
                Target.TweenLocalScaleY(0, duration).SetEase(offEase);
            if (scaleZ)
                Target.TweenLocalScaleZ(0, duration).SetEase(offEase);
            yield return new WaitForSeconds(duration);

            foreach (var col in Target.GetComponentsInChildren<Collider>())
                col.enabled = false;
        }
    }
}
