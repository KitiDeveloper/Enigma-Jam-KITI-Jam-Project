using Units.RayCaster;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private RectTransform pauseMenu;
        [SerializeField] private FirstPersonController controller;
        [SerializeField] private ObjectInteracter interacter;
        [SerializeField] private AudioClip pauseSound;
        [SerializeField] private AudioSource audioSource;
        private bool paused;

        private void Start()
        {
            resumeButton.onClick.AddListener(Resume);
            restartButton.onClick.AddListener(Restart);
            pauseMenu.gameObject.SetActive(false);
        }

        public void ShowPauseMenu()
        {
            if (paused) return;
            paused = true;
            audioSource.PlayOneShot(pauseSound);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            controller.canMove = false;
            pauseMenu.gameObject.SetActive(true);
            interacter.CanInteract = false;
            Time.timeScale = 0;
        }

        private void Restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void Resume()
        {
            paused = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            controller.canMove = true;
            interacter.CanInteract = true;
            Time.timeScale = 1;
            pauseMenu.gameObject.SetActive(false);
        }
    }
}