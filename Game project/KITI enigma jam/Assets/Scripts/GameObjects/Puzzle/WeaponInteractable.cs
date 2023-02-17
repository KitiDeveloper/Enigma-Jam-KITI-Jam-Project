using UnityEngine;

namespace GameObjects.Puzzle
{
    public class WeaponInteractable : Interactable
    {
        [SerializeField] private ClockPuzzle clockPuzzle;
        public override void OnInteract()
        {
            clockPuzzle.IncreasePuzzleProgress();
            clockPuzzle.SwitchClockStates();
            Debug.Log("Finished puzzle - spawn ghost");
        }

        public override void OnFocus()
        {
        }

        public override void OnLoseFocus()
        {
        }
    }
}