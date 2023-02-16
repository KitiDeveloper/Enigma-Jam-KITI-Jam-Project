using UnityEngine;

namespace GameObjects.Puzzle
{
    public class PuzzleProgressInteractable : Interactable
    {
        [SerializeField] private ClockPuzzle clockPuzzle;
        public override void OnInteract()
        {
            clockPuzzle.IncreasePuzzleProgress();
        }

        public override void OnFocus()
        {
        }

        public override void OnLoseFocus()
        {
        }
    }
}