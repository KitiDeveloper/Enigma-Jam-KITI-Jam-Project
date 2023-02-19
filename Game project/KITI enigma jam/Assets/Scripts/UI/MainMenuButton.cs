using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuButton: MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private int mainMenuIndex;

        private void Start()
        {
            button.onClick.AddListener(MainMenu);
        }

        private void MainMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadSceneAsync(mainMenuIndex);
        }
    }
}