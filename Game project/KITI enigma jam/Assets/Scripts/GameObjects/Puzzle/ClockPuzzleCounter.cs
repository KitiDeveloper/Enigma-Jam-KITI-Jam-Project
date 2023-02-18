using UnityEngine;

namespace GameObjects.Puzzle
{
    public class ClockPuzzleCounter : MonoBehaviour
    {
        private int numPuzzlesSolved;
        
        public void PuzzleSolved()
        {
            numPuzzlesSolved++;
            Debug.Log($"Total number of puzzles solved {numPuzzlesSolved}");
        }
    }
}