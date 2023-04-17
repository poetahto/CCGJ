using ElRaccoone.Tweens;
using poetools.player.Player.Interaction;
using UnityEngine;

namespace DefaultNamespace
{
    public class AntiChamberTile : MonoBehaviour, IInteractable
    {
        public CanvasGroup imageGroup;
        public CanvasGroup textGroup;
        public float duration = 0.5f;

        private bool _showingImage;

        public void HandleInteractStart(GameObject grabber)
        {
            if (_showingImage)
                ShowText();

            else ShowImage();
        }

        private void ShowImage()
        {

        }

        private void ShowText()
        {
            textGroup.TweenCancelAll();
            textGroup.TweenCanvasGroupAlpha(1, duration);
        }

        public void HandleInteractStop(GameObject grabber)
        {
            // Not needed.
        }
    }
}
