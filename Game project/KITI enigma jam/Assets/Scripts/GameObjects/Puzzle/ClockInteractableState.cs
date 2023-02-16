using UnityEngine;

namespace GameObjects.Puzzle
{
    public class ClockInteractableState : Interactable
    {
        private ClockInteractable clockInteractable;

        public void SetParent(ClockInteractable interactable)
        {
            clockInteractable = interactable;
        }
        
        public override void OnInteract()
        {
            clockInteractable.OnInteract();
        }

        public override void OnFocus()
        {
            clockInteractable.OnFocus();
        }

        public override void OnLoseFocus()
        {
            clockInteractable.OnLoseFocus();
        }
    }
}