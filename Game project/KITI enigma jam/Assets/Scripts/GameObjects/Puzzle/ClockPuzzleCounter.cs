using TMPro;
using UnityEngine;

namespace GameObjects.Puzzle
{
    public class ClockPuzzleCounter : MonoBehaviour
    {
        private int numPuzzlesSolved;
        [SerializeField] private TextMeshProUGUI text;
        
        public void PuzzleSolved()
        {
            numPuzzlesSolved++;
            text.text = $"Weapons discovered: {numPuzzlesSolved}";
        }
    }
}