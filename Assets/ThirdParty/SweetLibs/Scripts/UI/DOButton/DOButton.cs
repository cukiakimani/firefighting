using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using static UnityEngine.UI.Button;

namespace SweetLibs.UI
{
    public class DOButton : UIBehaviour, IPointerDownHandler, IPointerUpHandler,
        IPointerClickHandler
    {
        public bool Interactable;
        public Image TargetGraphic;

        [Space] [SerializeField] private ButtonClickedEvent onClick = new ButtonClickedEvent();

        [Space] public DOButtonStateTransition Normal;
        public DOButtonStateTransition Pressed;
        public DOButtonStateTransition Disabled;
        public DOButtonStateTransition PressedDisabled;
        public DOButtonStateTransition ReleasedDisabled;

        public ButtonClickedEvent OnClick
        {
            get { return onClick; }
            set { onClick = value; }
        }

        private Tweener stateTransitionTween;
        private bool _wasInteractable;
        private RectTransform rectTransform;
        private Vector2 baseAnchoredPosition;

        protected override void Awake()
        {
            base.Awake();
            _wasInteractable = Interactable;
            rectTransform = GetComponent<RectTransform>();
            baseAnchoredPosition = rectTransform.anchoredPosition;
        }

        private void Update()
        {
            if (_wasInteractable != Interactable)
            {
                SetState(Interactable ? Normal : Disabled);
                _wasInteractable = Interactable;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left && Interactable)
                return;

            OnClick.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!Interactable)
            {
                SetState(PressedDisabled);
                return;
            }

            SetState(Pressed);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!Interactable)
            {
                SetState(ReleasedDisabled);
                return;
            }

            SetState(Normal);
        }

        private void SetState(DOButtonStateTransition stateTransition)
        {
            if (TargetGraphic != null && stateTransition.Sprite != null)
            {
                TargetGraphic.sprite = stateTransition.Sprite;
            }

            SetTweener(stateTransition);
        }

        private void SetTweener(DOButtonStateTransition stateTransition)
        {
            stateTransitionTween?.Kill();

            foreach (var tweenSetting in stateTransition.TweenSettings)
            {
                switch (tweenSetting.TweenPropety)
                {
                    case DOButtonProperty.Scale:
                        stateTransitionTween = transform.DOScale(tweenSetting.TweenValue, tweenSetting.TweenDuration);
                        if (tweenSetting.UseTweenCurve)
                        {
                            stateTransitionTween.SetEase(tweenSetting.TweenCurve);
                        }
                        else
                        {
                            stateTransitionTween.SetEase(tweenSetting.Ease);
                        }

                        break;

                    case DOButtonProperty.AnchoredPosition:
                        stateTransitionTween = rectTransform.DOAnchorPos3D(
                            baseAnchoredPosition + (Vector2) tweenSetting.TweenValue, tweenSetting.TweenDuration);
                        if (tweenSetting.UseTweenCurve)
                        {
                            stateTransitionTween.SetEase(tweenSetting.TweenCurve);
                        }
                        else
                        {
                            stateTransitionTween.SetEase(tweenSetting.Ease);
                        }

                        break;
                }
            }

            if (stateTransition.Sound != null)
            {
                SFX.PlayAt(stateTransition.Sound);
            }
        }
    }
}