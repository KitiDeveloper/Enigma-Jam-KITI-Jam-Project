using UnityEngine;

namespace UI
{
    public class PauseInput : MonoBehaviour
    {
        [SerializeField] private PauseMenu pauseMenu;

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                PauseMenu();
            }
        }

        private void PauseMenu()
        {
            pauseMenu.ShowPauseMenu();
        }
    }
}