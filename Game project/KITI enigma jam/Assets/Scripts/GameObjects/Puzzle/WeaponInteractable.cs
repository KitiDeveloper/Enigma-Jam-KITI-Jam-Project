using UnityEngine;

namespace GameObjects.Puzzle
{
    public class WeaponInteractable : Interactable
    {
        public override void OnInteract()
        {
            Debug.Log("Puzzle Finished!!!");
        }

        public override void OnFocus()
        {
        }

        public override void OnLoseFocus()
        {
        }
    }
}