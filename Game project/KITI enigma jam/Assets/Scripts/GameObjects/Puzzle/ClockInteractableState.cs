using UnityEngine;

namespace GameObjects.Puzzle
{
    public class ClockInteractableState : Interactable
    {
        private ClockPuzzle clockPuzzle;

        public void SetParent(ClockPuzzle puzzle)
        {
            clockPuzzle = puzzle;
        }
        
        public override void OnInteract()
        {
            clockPuzzle.OnInteract();
        }

        public override void OnFocus()
        {
            clockPuzzle.OnFocus();
        }

        public override void OnLoseFocus()
        {
            clockPuzzle.OnLoseFocus();
        }
    }
}