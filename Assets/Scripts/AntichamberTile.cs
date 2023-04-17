using System;
using ElRaccoone.Tweens;
using poetools.player.Player.Interaction;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class AntiChamberTile : MonoBehaviour, IInteractable
    {
        public CanvasGroup imageGroup;
        public CanvasGroup textGroup;
        public Sprite sprite;
        public Image spriteImage;
        [TextArea]
        public string message;
        public float duration = 0.5f;

        private bool _showingImage = true;

        private void Awake()
        {
            textGroup.alpha = 0;
            imageGroup.alpha = 1;
            GetComponentInChildren<TMP_Text>().text = message;
            spriteImage.sprite = sprite;
        }

        public void HandleInteractStart(GameObject grabber)
        {
            if (_showingImage)
                ShowText();

            else ShowImage();
        }

        private void ShowImage()
        {
            _showingImage = true;
            textGroup.TweenCancelAll();
            imageGroup.TweenCancelAll();
            textGroup.TweenCanvasGroupAlpha(0, duration).SetOnComplete(() => imageGroup.TweenCanvasGroupAlpha(1, duration));
        }

        private void ShowText()
        {
            _showingImage = false;
            textGroup.TweenCancelAll();
            imageGroup.TweenCancelAll();
            imageGroup.TweenCanvasGroupAlpha(0, duration).SetOnComplete(() => textGroup.TweenCanvasGroupAlpha(1, duration));
        }

        public void HandleInteractStop(GameObject grabber)
        {
            // Not needed.
        }
    }
}
