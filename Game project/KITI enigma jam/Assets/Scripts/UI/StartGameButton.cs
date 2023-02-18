using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class StartGameButton : MonoBehaviour
    {
        [SerializeField] private GameObject loadingScreen;
        [SerializeField] private Button button;
        [SerializeField] private int gameSceneIndex;

        private void Start()
        {
            loadingScreen.SetActive(false);
            button.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            loadingScreen.SetActive(true);
            SceneManager.LoadSceneAsync(gameSceneIndex);
        }
    }
}