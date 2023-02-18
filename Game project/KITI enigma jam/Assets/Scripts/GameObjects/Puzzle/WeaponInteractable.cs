using UnityEngine;

namespace GameObjects.Puzzle
{
    public class WeaponInteractable : Interactable
    {
        [SerializeField] private ClockPuzzleCounter clockPuzzleCounter;
        [SerializeField] private ClockPuzzle clockPuzzle;
        [SerializeField] private int finishTimeState;
        [SerializeField] private int finishProgressState;
        public override void OnInteract()
        {
            clockPuzzle.SetTime(finishTimeState);
            clockPuzzle.SetPuzzleProgress(finishProgressState);
            clockPuzzle.UpdateObjects();
            clockPuzzleCounter.PuzzleSolved();
        }

        public override void OnFocus()
        {
        }

        public override void OnLoseFocus()
        {
        }
    }
}