using UnityEngine;

namespace GameObjects.Puzzle
{
    public class ClockInteractable : Interactable
    {
        [SerializeField] private ClockPuzzle clockPuzzle;

        public void SetParent(ClockPuzzle puzzle)
        {
            clockPuzzle = puzzle;
        }
        
        public override void OnInteract()
        {
            clockPuzzle.SwitchClockStates();
        }

        public override void OnFocus()
        {
        }

        public override void OnLoseFocus()
        {
        }
    }
}