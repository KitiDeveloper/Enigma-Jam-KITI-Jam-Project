using UnityEngine;

namespace GameObjects.Puzzle
{
    public class ClockInteractable : Interactable
    {
        [SerializeField] private ClockPuzzle clockPuzzle;
        
        public override void OnInteract()
        {
            if(clockPuzzle.SwitchTime())
                clockPuzzle.UpdateObjects();
        }

        public override void OnFocus()
        {
        }

        public override void OnLoseFocus()
        {
        }
    }
}