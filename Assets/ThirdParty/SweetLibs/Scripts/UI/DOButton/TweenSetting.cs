using UnityEngine;
using DG.Tweening;

namespace SweetLibs.UI
{
    [System.Serializable]
    public class TweenSetting
    {
        public DOButtonProperty TweenPropety;
        public Vector3 TweenValue;
        public float TweenDuration = 1f;
        public Ease Ease = Ease.Linear;
        public bool UseTweenCurve;
        public AnimationCurve TweenCurve;
    }
}