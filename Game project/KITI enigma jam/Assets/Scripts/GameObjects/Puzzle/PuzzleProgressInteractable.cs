using UnityEngine;
using UnityEngine.Serialization;

namespace GameObjects.Puzzle
{
    public class PuzzleProgressInteractable : Interactable
    {
        [SerializeField] private ClockPuzzle clockPuzzle;
        [SerializeField] private int progressState;
        public override void OnInteract()
        {
            clockPuzzle.SetPuzzleProgress(progressState);
            gameObject.SetActive(false);
        }

        public override void OnFocus()
        {
        }

        public override void OnLoseFocus()
        {
        }
    }
}