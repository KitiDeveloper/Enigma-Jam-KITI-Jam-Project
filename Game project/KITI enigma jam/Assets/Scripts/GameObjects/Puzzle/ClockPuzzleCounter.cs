using TMPro;
using UnityEngine;

namespace GameObjects.Puzzle
{
    public class ClockPuzzleCounter : MonoBehaviour
    {
        private int numPuzzlesSolved;
        [SerializeField] private int puzzlesToSolveToWin = 3;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private WinSequence winSequence;
        
        public void PuzzleSolved()
        {
            numPuzzlesSolved++;
            text.text = $"Weapons discovered: {numPuzzlesSolved}";
            if (numPuzzlesSolved == puzzlesToSolveToWin)
            {
                winSequence.Play();
            }
        }
    }
}